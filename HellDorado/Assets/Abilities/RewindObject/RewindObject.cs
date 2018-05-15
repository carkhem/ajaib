using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	private GameObject activeGameobject;
	public GameObject interactionIcon;
	private GameObject currentIcon;
    private GameObject clone;
    private GameObject objectToCloneFrom;

    private List<PointInTime> clonePointInTime;
    private int index = 0;
    private Material ghostMaterial;
    private Material trailMaterial;
    public GameObject trailPrefab;
    private GameObject trail;
    private float timer = 0.7f;

	void Update(){
		//Det här är en ful lösning men vet inte hur jag annars ska kunna avaktiver spökObjektet
		//när man byter ability
		if (GetComponent<AbilityManager> ().selectedAbility != AbilityManager.Ability.ObjectRewind)
			if (clone != null) {
				Destroy (clone);
				objectToCloneFrom = null;
			}	 
	}

    public void UseRewindObject(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
			activeGameobject = hit.collider.gameObject;
			hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StartRewind ();
		}
	}

	public void UpdateFeedback(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
            if (!Input.GetButton("Fire2"))
            {
                if (objectToCloneFrom == null)
                {
					InstantiateClone(hit.collider.gameObject);
                }
                else {
					UpdateClonePosition();
                }
				if (currentIcon == null) {
				currentIcon = GameObject.Instantiate (interactionIcon, new Vector3 (hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.LookRotation (Camera.main.transform.position - hit.transform.position));
				} else {
					currentIcon.GetComponent<InteractionIcon> ().KeepAlive ();
				}
			}
		} else {
			Destroy (currentIcon);
            Destroy(clone);
            objectToCloneFrom = null;
            index = 0;
            timer = 0.7f;
		}
	}


	public GameObject HitInfo(){
		return activeGameobject;
	}

	private void ChangeAlpha(Material original, Material newM, float alpha, float metallic) {
        newM = original;
		newM.SetFloat ("_Metallic", metallic);
		Color changeAlpha = newM.GetColor("_Color");
        changeAlpha.a = alpha;
		newM.color = changeAlpha;
        original = newM;

    }

	private void InstantiateClone(GameObject _gameObject){
		objectToCloneFrom = _gameObject;
		clone = GameObject.Instantiate(objectToCloneFrom, objectToCloneFrom.transform.position, Quaternion.identity);
		clone.GetComponent<BoxCollider>().enabled = false;
		clone.GetComponent<Rigidbody>().isKinematic = true;
		clonePointInTime = objectToCloneFrom.GetComponent<ObjectTimeBody>().GetPointsInTime();
		// Destroy(clone.GetComponent<ObjectTimeBody>());
		if(clone.GetComponent<PushableObject>() != null)
			Destroy(clone.GetComponent<PushableObject>());
		if(clone.GetComponent<ObjectTimeBody>() != null)
			Destroy(clone.GetComponent<FreezeTime>());
		if(clone.GetComponent<Animator>() != null)
			Destroy(clone.GetComponent<Animator>());
		clone.GetComponent<ObjectTimeBody>().SetPointsInTime(clonePointInTime);
		
		ChangeAlpha(clone.GetComponent<Renderer>().material, ghostMaterial, 0.3f,0f);
		
		timer -= Time.deltaTime;
		trail = GameObject.Instantiate(trailPrefab, clone.transform.position, Quaternion.identity);
		trail.transform.parent = clone.transform;
		clone.SetActive(false);
	}

	private void UpdateClonePosition(){
		
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			clone.SetActive(true);
			
			if (clonePointInTime.Count > index)
			{
				PointInTime pointInTime = clonePointInTime[index];
				clone.transform.position = pointInTime.position;
				clone.transform.rotation = pointInTime.rotation;
				index++;
			}
			
		}
	}

}
