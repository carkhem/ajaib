using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	public float distance;
	public float speed = 1f;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool upDown;
	private float startTime;
	private float journeyLenght;
	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		endPosition = new Vector3 (transform.position.x, transform.position.y + distance, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (upDown) {
			if (journeyLenght != 0) {
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLenght;

				transform.position = Vector3.Lerp (transform.position, endPosition, fracJourney);

			}
		} else {
			if (journeyLenght != 0) {
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLenght;

				transform.position = Vector3.Lerp (transform.position, startPosition, fracJourney);

			}
		}

	}

	public void ElevetorUp(){
		upDown = true;
		startTime = Time.time;
		journeyLenght = Vector3.Distance (transform.position, endPosition);
	}

	public void ElevetorDown(){
		upDown = false;
		startTime = Time.time;
		journeyLenght = Vector3.Distance (transform.position, startPosition);
	}
}
