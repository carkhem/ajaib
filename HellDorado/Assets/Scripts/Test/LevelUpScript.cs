using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpScript : MonoBehaviour {
    private float exp = 100;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
//			GameManager.instance.ChangePlayerLevel (GameManager.instance.playerLevel + 1);
			GameManager.instance.LevelUp();
			Destroy (gameObject);
		}
    }
}
