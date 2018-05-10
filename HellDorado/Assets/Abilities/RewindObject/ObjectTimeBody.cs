using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeBody : MonoBehaviour {

	public bool isRewinding = false;
	public float recordTime = 5f;

	public List<PointInTime> pointsInTime;
	private Rigidbody rb;
	private PlayerController _controller;
	private AbilitySounds abilitySounds;


	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
		_controller = GetComponent<PlayerController> ();
//		abilitySounds = _controller.GetComponent<AbilitySounds>();

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
        if (transform.GetComponent<BoxCollider>().enabled == true)
            rb.isKinematic = true;

//		abilitySounds.PlayAbilitySound ("RewindObject");

	}

	public void StopRewind ()
	{
		isRewinding = false;
        if(transform.GetComponent<BoxCollider>().enabled == true)
		    rb.isKinematic = false;
//		abilitySounds.StopPlayingAudio();
	}

    public List<PointInTime> GetPointsInTime() {
        return pointsInTime;
    }

    public void SetPointsInTime(List<PointInTime> list) {
        pointsInTime = list;
    }

}
