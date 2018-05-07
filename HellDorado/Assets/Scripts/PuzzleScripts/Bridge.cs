using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {


	[HideInInspector]
	public int counter = 0;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInParent<Animator> ();
	}

	void Update(){
		if (counter == 2) {
			animator.SetBool ("Open", true);
		}
	}
	


}
