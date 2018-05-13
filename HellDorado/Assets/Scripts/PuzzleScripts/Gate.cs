using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public bool open = false;
	private Animator anim;
	private float timer = 0;
	[HideInInspector]
	public float delayTime = 0;

	public AudioClip gateSound;
	private AudioSource source;

	private void Start(){
		anim = transform.GetChild (0).GetComponent<Animator> ();
		animOpen (open);
		source = GetComponent<AudioSource> ();
	}

	private void Update(){
		if (delayTime > 0){
			timer += Time.deltaTime;
			if (timer > delayTime) {
				CloseGate ();
				delayTime = 0;
				timer = 0;
			}
		}
	}

	public void CloseGateDelayed(float seconds){
		delayTime = seconds;
	}

	public void OpenGate(){
        delayTime = 0;
        timer = 0;
		open = true;
		animOpen (open);
//		source.PlayOneShot (gateSound);
	}

	public void CloseGate(){
		open = false;
		animOpen (open);
//		source.PlayOneShot (gateSound);
	}

	public void ToggleGate(){
		open = !open;
		animOpen (open);
	}

	private void animOpen(bool isOpen){
		anim.SetBool ("open", isOpen);
	}

	public void Print(){
		print ("yoyo");
	}

}
