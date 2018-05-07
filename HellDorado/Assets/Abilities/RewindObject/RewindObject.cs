using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	private GameObject activeGameobject;
	public GameObject interactionIcon;
	private GameObject currentIcon;

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
			if (!Input.GetButton ("Fire2")){
				if (currentIcon == null) {
				currentIcon = GameObject.Instantiate (interactionIcon, new Vector3 (hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.LookRotation (Camera.main.transform.position - hit.transform.position));
				} else {
					currentIcon.GetComponent<InteractionIcon> ().KeepAlive ();
				}
			}
		} else {
			Destroy (currentIcon);
		}
	}


	public GameObject HitInfo(){
		return activeGameobject;
	}



}
