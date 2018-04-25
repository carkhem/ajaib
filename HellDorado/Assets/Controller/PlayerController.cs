using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerController : Controller {

	[Header("Movement")]
	public float MaxSpeed = 10f;
	public float Gravity = 100f;
	[Range(0f, 1f)] public float SlopeTollerance = 0.2f;
	[Range(0f, 1f)] public float InputRequiredToMove = 0.5f;

	[Header("Collision")]
	public LayerMask CollisionLayers;
	public float GroundCheckDistance = 0.15f;
	public float SkinWidth = 0.03f;

	public float MaxWallAngleDelta = 5f;

	private BoxCollider _collider;
    private bool rewinding;

	private void Start() {
		_collider = GetComponent<BoxCollider>();
        rewinding = GetComponent<TimeBody>().isRewinding;
	}

	public Vector3 Input {
		get
		{
            Vector3 input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0.0f, UnityEngine.Input.GetAxisRaw("Vertical"));
            if (!rewinding)
            {
                
                float y = Camera.main.transform.rotation.eulerAngles.y;
                input = Quaternion.Euler(0f, y, 0f) * input;
            }
			return input;

		}
	}
		
	public RaycastHit[] DetectHits(bool addGroundCheck = false, Vector3 overrideRay = default(Vector3)) {
		Vector3 ray = overrideRay == default(Vector3) ? Velocity * Time.deltaTime : overrideRay;
		List<RaycastHit> hits = Physics.BoxCastAll(transform.position, _collider.size * 0.5f, ray.normalized, transform.rotation, ray.magnitude, CollisionLayers).ToList();
		if (addGroundCheck)
		{
			RaycastHit[] groundHits = Physics.BoxCastAll(transform.position, _collider.size * 0.5f, Vector3.down, transform.rotation, GroundCheckDistance, CollisionLayers);
			hits.AddRange(groundHits);
		}
		for (int i = hits.Count - 1; i >= 0; i--)
		{
			if (hits[i].point.magnitude < MathHelper.FloatEpsilon)
			{
				hits.RemoveAt(i);
				continue;
			}
			RaycastHit temp;
			Physics.Linecast(transform.position + _collider.center, hits[i].point, out temp, CollisionLayers);
			if (temp.collider != null) hits[i] = temp;
		}
		return hits.ToArray();
	}

	// Om en boxcast kallas med en ursprungspunkt som överlappar med en collider kommer
	// datan som returneras inte vara användbar, träffar för collidern kommer returneras dock
	// kommer träffpunkten alltid vara [0,0,0], distansen för träffen kommer vara 0 och normalen
	// kommer vara motsatt till riktningen som casten skedde i

	// På grund av detta beteende kommer vi också kunna hamna i situationer där vi kan gå
	// igenom väggar eller liknande. Om vi på något sätt hamnar inuti en collider kan vi inte få ut
	// några användbara träffar och spelaren kommer bete sig som att det inte finns någon collider
	// över huvud taget. Detta är ett problem som antagligen kan lösas på många olika sätt, till
	// exempel genom att använda en OverlapBox (em metod som returnerar alla colliders inom en
	// given kub) och sedan på något sätt försöka hitta en väg ut ur den collider spelaren är inut

	public void SnapToHit(RaycastHit hit)
	{
		Vector3 vectorToHit = hit.point - transform.position;

		RaycastHit playerHit;
		Physics.Linecast(hit.point + vectorToHit.normalized, transform.position, out playerHit, LayerMask.GetMask("Player"));

		if (playerHit.collider == null) return;

		vectorToHit = hit.point - playerHit.point;
		vectorToHit -= vectorToHit.normalized * SkinWidth;
		Vector3 movement = hit.normal * Vector3.Dot(vectorToHit , hit.normal);

		if (Vector3.Dot(movement.normalized, Velocity.normalized) > 0.0f)
			transform.position += movement;
	}

	// Det finns några stora problem med att lösa snapping på det här sättet, det absolut största är
	// att vi inte kan hantera skarpa vinklar

	// Det bästa sättet att lösa det skulle antagligen vara att inte enbart
	// snappa till en hit i taget, utan istället köra SnapToHit en gång med tillgång till alla träffar. I
	// den situationen kan metoden räkna ut den optimala positionen där spelaren bör hamna. Till
	// exempel kan ett medelvärde av alla träffpunkter hittas och sedan kan spelaren flyttas fram
	// mot den punkten så länge den inte är inuti en vägg.

}