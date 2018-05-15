using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public static bool isRewinding = false;
    public int rewindSpeed = 2;

	public float recordTime = 5f;
    private float playerGravity;
	private List<PointInTime> pointsInTime;

    private PlayerController playerController;

	void Start () {
		pointsInTime = new List<PointInTime>();

		playerController = GetComponent<PlayerController> ();
	}

	void FixedUpdate ()
	{
		if (isRewinding) {

			Rewind ();
		} else {
		
			Record ();
		}
	}

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            if(pointsInTime.Count > 1)
             transform.rotation = Quaternion.Lerp(transform.rotation, pointsInTime[0].rotation, Time.deltaTime * 5f);
            else
              transform.rotation = pointInTime.rotation;

            for (int i = 0; i < rewindSpeed; i++)
            {
				if (pointsInTime.Count < 1)
					return;
                pointsInTime.RemoveAt(0);

            }
		} else
		{
			TimeBody.isRewinding = false;
			//StopRewind();
		}
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

        if (playerController != null)
        {
			if((playerController.InputVector.x != 0f && playerController.InputVector.z != 0f) ||
				pointsInTime.Count > 0 && transform.position != pointsInTime[0].position)
				
             	   pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        }
	}


	public void StartRewind ()
	{
		//isRewinding = true;
	

	}

	public void StopRewind ()
	{
		//isRewinding = false;
    	
	}
}
