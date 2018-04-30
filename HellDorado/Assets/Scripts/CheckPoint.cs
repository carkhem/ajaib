using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	public bool activated = false;
	public static GameObject[] checkPoints;

	public static Vector3 GetActiveCheckPointPosition ()
	{

		//Om det inte finns någon checkpoint aktiverad så är positionen "default"
		Vector3 position = new Vector3 (0, 0, 0);

		if (checkPoints != null) {
			foreach (GameObject checkpoint in checkPoints) {
				if (checkpoint.GetComponent<CheckPoint> ().activated) {
					position = checkpoint.transform.position;
					break;
				}
			}
		}

		return position;
	}

	private void ActivateCheckPoint ()
	{
		//går igenom alla checkpoints i listan och deaktiverar dem
		foreach (GameObject cp in checkPoints) {
			cp.GetComponent<CheckPoint> ().activated = false;
		}

		//aktiverar nuvarande checkpoint
		activated = true;
	}

	void Start ()
	{
		checkPoints = GameObject.FindGameObjectsWithTag ("CheckPoint");
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.tag == "Player") {
			ActivateCheckPoint ();
		}
	}
}