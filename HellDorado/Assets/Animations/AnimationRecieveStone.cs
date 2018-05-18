using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationRecieveStone : MonoBehaviour {

	public PlayableDirector director;
	private Vector3 startPosition = new Vector3 (-12f, 1.598708f, 3.69f);
	private GameObject player;
	public GameObject gameHands;
	public FPSCamera fps;
	private bool stopped;

	void Start (){
		player = PlayerStats.instance.transform.gameObject;
		director.Stop ();
		director.playOnAwake = false;
	}

	public void StartAnimation(){
		gameHands.SetActive(false);
		director.transform.gameObject.SetActive (true);
		fps.enabled = false;
		director.Play ();
		stopped = false;
		player.GetComponent<CharacterController> ().enabled = false;
	}

	void Update(){
		if (director.time > 0 && !stopped) {
			player.transform.position = Vector3.Lerp (player.transform.position, startPosition, Time.deltaTime * 50);
		}

		if (director.time > 14.11f && !stopped){
			CanvasManager.instance.rewindPanel.SetActive (true);
		}

		if (director.time > director.duration - 2 && !stopped) {
			stopped = true;
			CanvasManager.instance.rewindPanel.SetActive (false);
			GetComponent<FPSCamera> ().enabled = true;
			gameHands.SetActive(true);
			director.gameObject.SetActive (false);
			fps.enabled = true;
            GameManager.instance.SetAbilityCount(2);
            player.GetComponent<CharacterController> ().enabled = true;
		}
	}
}
