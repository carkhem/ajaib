using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	public float Ystart;
	public float Yend;
	public float speed = 1f;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool upDown;
	private float startTime;
	private float journeyLenght;
	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (transform.position.x, Ystart, transform.position.z);
		endPosition = new Vector3 (transform.position.x, Yend, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (upDown) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLenght;
			transform.position = Vector3.Lerp (transform.position, endPosition, fracJourney);
		} else {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLenght;
			transform.position = Vector3.Lerp (transform.position, startPosition, fracJourney);
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
