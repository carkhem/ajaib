using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightCube : MonoBehaviour {

	[HideInInspector]
	public bool hit = false;
	[HideInInspector]
	public float distance = 0f;

	private Material original;
	public Material highlight;

	// Use this for initialization
	void Start () {
		original = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		HighlightObject ();
	}


	private void HighlightObject(){
		if (hit) {
			GetComponent<Renderer> ().material = highlight;
		
//			float border = (distance * 0.1f) + 0.2f;
//			Mathf.Clamp (border, 1.2f, 2f);
			GetComponent<Renderer> ().material.SetFloat ("_OutlineWidth", 1.1f);
		} else {
			GetComponent<Renderer> ().material = original;
		}
	}
}
