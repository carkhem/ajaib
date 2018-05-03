using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {


	ObjectTimeBody activeObject;
	private GameObject activeGameobject;

	void Update(){
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			if (activeObject != null && activeObject.isRewinding)
				activeObject.StopRewind ();
		}
	}

	public void UseRewindObject(float cost){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		//Är medveten om att tagen är lite missvisande för tillfället, ska ändra till något mer passande 
		//eller använda lager
		if ((Physics.Raycast (ray, out hit, 50f) && (hit.collider.gameObject.tag == "Object"))) {
			activeObject = hit.collider.gameObject.GetComponent<ObjectTimeBody> ();
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				hit.collider.gameObject.GetComponent<ObjectTimeBody>().StartRewind ();
				GetComponent<PlayerStats> ().ChangeHealth (-cost);
				hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StartRewind ();
				GetComponent<PlayerStats> ().DamagePlayer (cost);
			} else if (Input.GetKeyUp (KeyCode.Mouse1)) {
				hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StopRewind ();
			}
		}
	}



}
