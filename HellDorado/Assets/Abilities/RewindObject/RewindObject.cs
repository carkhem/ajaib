using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	public bool isRewinding = false;
	public LayerMask ObjectLayer;
	public float recordTime = 5f;
	public Material shadowMaterial;

	public Transform shadowObject;
	public bool shadowObjectCreated = false;
	List<PointInTime> pointsInTime;

	private Rigidbody rb;
	//Rigidbody rb;

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
