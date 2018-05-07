using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTorch : MonoBehaviour {
	public bool lit;
	public ParticleSystem fire;
	public Light fireLight;

	public AudioClip TorchSound;
	public AudioSource source;

	private float lightIntensity;

	void Start(){
		lightIntensity = fireLight.intensity;
		fireLight.intensity = 0;
		fire.Stop ();
		source = GetComponent<AudioSource> ();
		source.clip = TorchSound;
		source.loop = true;
		source.time = Random.Range (0, TorchSound.length);
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
		source.Play ();
	}

	public void PutOut(){
		lit = false;
		fire.Stop ();
		fireLight.intensity = 0;
		source.Stop ();
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "FireBall")
			lit = true;
	}
}
