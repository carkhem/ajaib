using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeBody : MonoBehaviour {

	public bool isRewinding = false;
	public float recordTime = 5f;

	List<PointInTime> pointsInTime;
	private Rigidbody rb;
	private AbilitySounds abilitySounds;

	//Lämna tills vidare
	//	public Transform shadowObject;
	//	public Transform clone;
	//	public bool shadowObjectCreated = false;
	//	public bool deactivateObject = false;
	//	public Material shadowMaterial;

	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();

	}


	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind ()
	{

		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}

	}


	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		if(rb.velocity.sqrMagnitude > 0.01f)
			pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));


		//Lämna tills vidare
		//		if (clone != null && pointsInTime.Count > 0) {
		//			clone.position = pointsInTime [pointsInTime.Count - 1].position;
		//			clone.rotation = pointsInTime [pointsInTime.Count - 1].rotation;
		//		}
	}

	public void StartRewind ()
	{
		isRewinding = true;
		rb.isKinematic = true;
		abilitySounds.PlayAbilitySound ("RewindObject");

	}

	public void StopRewind ()
	{
		isRewinding = false;
		rb.isKinematic = false;
		abilitySounds.StopPlayingAudio();
	}


	//lämna tills vidare
	//	public void CreateShadowObject(){
	//
	//		if (shadowObject != null) {
	//			clone = Instantiate (shadowObject, transform.position, Quaternion.identity);
	//			clone.gameObject.GetComponent<MeshRenderer> ().material = shadowMaterial;
	//			Destroy (clone.gameObject.GetComponent<Rigidbody> ());
	//			Destroy(clone.gameObject.GetComponent<BoxCollider> ());
	//			Destroy (clone.gameObject.GetComponent<RewindObject> ());
	//			clone.gameObject.layer = 0;
	//			clone.gameObject.SetActive (false);
	//			if (pointsInTime.Count > 0) {
	//				clone.position = pointsInTime [pointsInTime.Count - 1].position;
	//				clone.rotation = pointsInTime [pointsInTime.Count - 1].rotation;
	//			}
	//
	//		}
	//	}
	//
	//	public void ActivateShadowObject(){
	//		if (clone != null && shadowObjectCreated == true) {
	//			if (rb.velocity.sqrMagnitude < 0.01f)
	//				if(pointsInTime.Count > 0 && pointsInTime[0].position != pointsInTime[pointsInTime.Count-1].position)
	//					clone.gameObject.SetActive (true);
	//		} else {
	//			shadowObjectCreated = true;
	//			CreateShadowObject ();
	//		}
	//	}
	//
	//	public void DestroyObject(){
	//		if (clone != null) {
	//			clone.gameObject.SetActive (false);
	//			Debug.Log ("WHYYYYY");
	//			deactivateObject = false;
	//		}	
	//	}
	//
	//	public void DeactivateObject(){
	//
	//		deactivateObject = true;
	//	}
}
