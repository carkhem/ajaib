using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slowDownFactor = 0.05f;
    public float slowDownLenght = 2f;
	public bool resetTime = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (resetTime)
			TimeToNormal ();

	}

	public void DoSlowMotion(){
		resetTime = false;
		Time.timeScale = slowDownFactor;
		Time.timeScale = Time.timeScale * 0.02f;
	}

	public void TimeToNormal(){
		Time.timeScale += (1f / slowDownLenght) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
	}
}
