using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public static bool isRewinding = false;

	private bool enemyTransition = false;

	public float recordTime = 5f;
    private float playerGravity;
	private List<PointInTime> pointsInTime;
	private List<string> enemyStates;

	private EnemyController enemy;


	void Start () {
		pointsInTime = new List<PointInTime>();
     
		enemy = GetComponent<EnemyController> ();
		if (enemy != null)
			enemyStates = new List<string> ();
	}

	void FixedUpdate ()
	{
		if (isRewinding) {
			if (enemy != null && !enemyTransition) {
				enemy.recentHealth = enemy.health;
				enemyTransition = true;
				enemy.TransitionTo<EnemyRewindState> ();
			}
			Rewind ();
		} else {
			enemyTransition = false;
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

			if (enemy != null) {
				string enemyRecentState = enemyStates [0];
				enemy.recentState = enemyRecentState;
				enemyStates.RemoveAt (0);
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
			if(enemy != null)
				enemyStates.RemoveAt (enemyStates.Count - 1);
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}
		if(enemy != null)
			enemyStates.Insert (0, EnemyRecentState ());
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	private string EnemyRecentState(){

		enemy.recentState = enemy.CurrentState.name;
		enemy.recentState = enemy.recentState.Remove (enemy.recentState.Length-7,7);
		enemy.recentState.Trim ();
		return enemy.recentState;
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
