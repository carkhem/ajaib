using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTorchTimer : MonoBehaviour {

	public bool lit;
	public ParticleSystem fire;
	public Light fireLight;
	public float timer = 3f;
	public FireGateTimer gate;
	private float originalTime;
	private float lightIntensity;

	void Start(){
		lightIntensity = fireLight.intensity;
		fireLight.intensity = 0;
		originalTime = timer;
	}

	void Update(){

		if (!TimeBody.isRewinding && lit) {
			timer -= Time.deltaTime;
		}

		if (timer <= 0)
			lit = false;

	
		if (lit && !fire.isPlaying)
			Light ();
		else if (!lit && fire.isPlaying && !gate.bothTorchesLit)
			PutOut ();

	}

	public void Light(){
		lit = true;
		fire.Play ();
		fireLight.intensity = lightIntensity;
	}

	public void PutOut(){
		lit = false;
		fire.Stop ();
		fireLight.intensity = 0;
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "FireBall") {
			lit = true;
			timer = originalTime;
		}
			
	}
}
