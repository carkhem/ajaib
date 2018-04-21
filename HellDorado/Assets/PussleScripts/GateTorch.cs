using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTorch : MonoBehaviour {
	public bool lit;

	public void Light(){
		lit = true;
	}

	public void PutOut(){
		lit = false;
	}
}
