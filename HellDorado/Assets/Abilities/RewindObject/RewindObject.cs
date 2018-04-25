using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	public bool isRewinding = false;
	public LayerMask ObjectLayer;
	public float recordTime = 5f;
	private float objectGravity;
	public Material shadowMaterial;

	List<PointInTime> pointsInTime;
	List<PointInTime> shadowRewind;
	Transform shadowObject;

	public static bool createShadowObject = false;
	public bool activateShadowObject = false;
	private Rigidbody rb;
	//Rigidbody rb;

	void Start () {
		pointsInTime = new List<PointInTime>();
		shadowRewind = new List<PointInTime> ();
		rb = GetComponent<Rigidbody>();
		shadowObject = gameObject.transform;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (pointsInTime.Count);
	}

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
			Record();

		if (gameObject.GetComponent<Rigidbody> ().velocity == Vector3.zero && createShadowObject == true) {
			activateShadowObject = true;
		}
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

	public void CreateShadowObject(){
		if (pointsInTime.Count <= 0)
			return;

	

		if (createShadowObject == false) {
			Instantiate (shadowObject, transform.position, Quaternion.identity);
			shadowObject.GetComponent<MeshRenderer> ().material = shadowMaterial;
			shadowObject.GetComponent<BoxCollider> ().isTrigger = true;
			shadowObject.gameObject.SetActive (false);
			createShadowObject = true;
			shadowRewind = pointsInTime;
		}


	}

	public void ShadowRewind(){
		shadowObject.gameObject.SetActive (true);

		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			shadowObject.position = pointInTime.position;
			shadowObject.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			Destroy (shadowObject);
			createShadowObject = false;
			activateShadowObject = false;
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
	}

	public void StartRewind ()
	{
		isRewinding = true;
			rb.isKinematic = true;

	}

	public void StopRewind ()
	{
		isRewinding = false;
			rb.isKinematic = false;
	
	}
}
