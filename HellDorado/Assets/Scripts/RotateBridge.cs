using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBridge : MonoBehaviour {

    private bool toggleActive;
	private bool isLerping;
	private Quaternion newRotation;
	private Quaternion startRotation;
	private float direction;

	public float timeToFinishLerp = 1f;
	private float _timeStartedLerping;
	// Use this for initialization
	void Start () {
        toggleActive = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLerping) {
			float timeSinceStarted = Time.time - _timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeToFinishLerp;

			transform.rotation = Quaternion.Lerp(startRotation,newRotation,percentageComplete);

			if(percentageComplete >= 1f){
				isLerping = false;
			}
		}
	}

    public void ActivateBridge(float amountToRotate)
    {
		if(!isLerping){
		toggleActive = !toggleActive;
		direction = toggleActive == true ? 1f : -1f;

		isLerping = true;
		_timeStartedLerping = Time.time;
		
		startRotation = transform.rotation;
		
		newRotation = transform.rotation;
		newRotation *= Quaternion.Euler (0, amountToRotate*direction, 0);
		}
    }

	public void Reset(){
		toggleActive = false;
		isLerping = false;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
	}


}
