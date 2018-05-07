using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour {

	private float lifeTime = 0.5f;
	private float timer = 0;

	private void Update(){
		transform.LookAt (Camera.main.transform);
		timer += Time.deltaTime;
		if (timer > lifeTime) {
			Destroy (gameObject);
		}
	}

	public void KeepAlive(){
		timer = 0;
	}
}
