using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public static bool isRewinding = false;

	public float recordTime = 5f;
    private float playerGravity;
	List<PointInTime> pointsInTime;

	private EnemyController enemy;


	void Start () {
		pointsInTime = new List<PointInTime>();
     
		enemy = GetComponent<EnemyController> ();
	}

	void FixedUpdate ()
	{
		if (isRewinding) {
			if (enemy != null) {
				enemy.recentHealth = enemy.health;

				enemy.recentState = enemy.CurrentState.name;
				Debug.Log (enemy.recentState + " recentstate");
				enemy.TransitionTo<EnemyRewindState> ();
			}
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
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
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

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
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
