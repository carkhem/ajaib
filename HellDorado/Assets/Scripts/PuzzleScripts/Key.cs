using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	Transform player;

	void Start() {
		player = PlayerStats.instance.transform;
	}

	public void PickUp(){
		player.GetComponent<PlayerSounds>().PlayKeyPickUpSound ();
		Destroy (gameObject);
	}
}
