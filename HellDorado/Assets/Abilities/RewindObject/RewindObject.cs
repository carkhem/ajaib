using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	private GameObject activeGameobject;
	public GameObject interactionIcon;

	public void UseRewindObject(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
			activeGameobject = hit.collider.gameObject;
			hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StartRewind ();
		}
	}

	public void UpdateFeedback(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
			GameObject.Instantiate (interactionIcon, new Vector3 (transform.position.x, transform.position.y + 5), transform.rotation);
		}
	}


	public GameObject HitInfo(){
		return activeGameobject;
	}



}
