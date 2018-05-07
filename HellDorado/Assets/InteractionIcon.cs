using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour {

	private void Start(){

	}

	private void Update(){
		transform.LookAt (Camera.main.transform);
	}
}
