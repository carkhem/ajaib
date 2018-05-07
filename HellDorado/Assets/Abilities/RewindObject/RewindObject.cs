using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {



	private GameObject activeGameobject;



	public void UseRewindObject(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;


		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable"))) {
			if (hit.collider.GetComponent<ObjectTimeBody> () != null) {
				activeGameobject = hit.collider.gameObject;
				hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StartRewind ();
			}
		}
	}

	public GameObject HitInfo(){
		return activeGameobject;
	}



}
