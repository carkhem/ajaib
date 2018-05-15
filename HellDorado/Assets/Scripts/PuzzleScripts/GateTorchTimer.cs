using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTorchTimer : MonoBehaviour {

	public bool lit;
	public ParticleSystem fire;
	public Light fireLight;
	public float timer = 3f;
	private float originalTime;
	private float lightIntensity;
	public float fireHight = 2f;

	void Start(){
		lightIntensity = fireLight.intensity;
		fireLight.intensity = 0;
		originalTime = timer;
	}

	void Update(){

		if (!TimeBody.isRewinding && lit) {
			timer -= Time.deltaTime;
		}
		if (timer <= 0f)
			PutOut ();

	
		if (lit && !fire.isPlaying)
			Light ();
		else if (!lit && fire.isPlaying)
			PutOut ();

	}

	public void Light(){
		lit = true;
		fire.Play ();
		fire.startLifetime = fireHight;
		foreach (ParticleSystem system in fire.transform.GetComponentsInChildren<ParticleSystem>()) {
			system.startLifetime = fireHight;
		}
		fireLight.intensity = lightIntensity;
	}

	public void PutOut(){
		lit = false;
		fire.Stop ();
		fire.startLifetime = 1f;
		foreach (ParticleSystem system in fire.transform.GetComponentsInChildren<ParticleSystem>()) {
			system.startLifetime = 1f;
		}
		fireLight.intensity = 0;
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "FireBall") {
			lit = true;
			timer = originalTime;
		}
			
	}
}
