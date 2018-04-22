using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTorch : MonoBehaviour {
	public bool lit;
	public ParticleSystem fire;
	public Light fireLight;
	private float lightIntensity;

	void Start(){
		lightIntensity = fireLight.intensity;
		fireLight.intensity = 0;
		print (lightIntensity);
	}

	void Update(){
		//TEST
		if (lit && !fire.isPlaying)
			Light ();
		else if (!lit && fire.isPlaying)
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
}
