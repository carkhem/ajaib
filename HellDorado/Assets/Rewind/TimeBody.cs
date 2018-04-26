using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public bool isRewinding = false;

	public float recordTime = 5f;
    private float playerGravity;
	List<PointInTime> pointsInTime;
    private PlayerController _controller;
	//Rigidbody rb;

	void Start () {
		pointsInTime = new List<PointInTime>();
        //	rb = GetComponent<Rigidbody>();
        _controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame


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

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind ()
	{
		isRewinding = true;
        //	rb.isKinematic = true;
        playerGravity = _controller.Gravity;
        _controller.Gravity = 0;
	}

	public void StopRewind ()
	{
		isRewinding = false;
        //	rb.isKinematic = false;
        _controller.Gravity = playerGravity;
	}
}
