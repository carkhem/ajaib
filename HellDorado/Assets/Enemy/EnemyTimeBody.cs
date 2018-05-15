using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimeBody : MonoBehaviour {

	public float recordTime = 5f;
	public int rewindSpeed = 1;
	public bool isRewinding = false;
	private bool enemyTransition = false;
	private List<string> enemyStates;
	
	private EnemyController enemy;
	private List<PointInTime> pointsInTime;

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
			//transform.rotation = pointInTime.rotation;
			if(pointsInTime.Count > 1)
				transform.rotation = Quaternion.Lerp(transform.rotation, pointsInTime[0].rotation, Time.deltaTime * 5f);
			else
				transform.rotation = pointInTime.rotation;
			
			for (int i = 0; i < rewindSpeed; i++)
			{
				if (pointsInTime.Count < 1)
					return;
				pointsInTime.RemoveAt(0);

				string enemyRecentState = enemyStates[0];
				enemy.recentState = enemyRecentState;
				enemyStates.RemoveAt(0);

			}
		} else
		{
			isRewinding = false;
		}
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{

			enemyStates.RemoveAt (enemyStates.Count - 1);
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

			
		if (!enemy.dead) {
			enemyStates.Insert (0, EnemyState ());
			pointsInTime.Insert (0, new PointInTime (transform.position, transform.rotation));
		}

	}

	public string EnemyState(){
		
		string state = enemy.CurrentState.name;
		state = state.Remove (state.Length-7,7);
		state.Trim ();
		return state;
	}
}
