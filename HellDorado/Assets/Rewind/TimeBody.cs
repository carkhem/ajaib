using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	bool isRewinding = false;

	public float recordTime = 5f;
    private float playerGravity;
	List<PointInTime> pointsInTime;
    private PlayerController _controller;
    //Rigidbody rb;
    private int count;

	void Start () {
		pointsInTime = new List<PointInTime>();
        //	rb = GetComponent<Rigidbody>();
        _controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse1))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Mouse1))
			StopRewind();
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

            if (count >= 60)
            {
                GetComponent<PlayerScript>().changeHealth(-1);
                
                if (count == 60)
                    count = 0;
                count++;
            }
            
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
        count = 0;
	}

	public void StopRewind ()
	{
		isRewinding = false;
        //	rb.isKinematic = false;
        _controller.Gravity = playerGravity;
	}
}
