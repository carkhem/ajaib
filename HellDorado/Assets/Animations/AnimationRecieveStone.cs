using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationRecieveStone : MonoBehaviour {

	public PlayableDirector director;
	private Vector3 startPosition = new Vector3 (-11.93f, 1.51f, 3.69f);
	private GameObject player;

	void Start (){
		player = PlayerStats.instance.transform.gameObject;
	}

	public void StartAnimation(){
		GetComponent<FPSCamera> ().enabled = false;
		director.Play ();
	}

	void Update(){
		if (director.time > 0) {
			player.transform.position = Vector3.Lerp (player.transform.position, startPosition, Time.deltaTime * 50);
		}
		if (director.time >= director.duration) {
			GetComponent<FPSCamera> ().enabled = true;
		}
	}
}
