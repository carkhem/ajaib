﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProximity : MonoBehaviour {

	private EnemyController _controller;

	public bool activateRewind = false;

	// Use this for initialization
	void Start () {
		_controller = GetComponent<EnemyController> ();
	
	}

	void Update(){
		ActivateRewind ();
	}
	
	void ActivateRewind(){
		
		if (activateRewind) {
	
			if (AbilityManager.WorldRewind) {
				_controller.TransitionTo<EnemyRewindState> ();
				Debug.Log ("enter");
			}
		
			activateRewind = false;
		}
	}
}
