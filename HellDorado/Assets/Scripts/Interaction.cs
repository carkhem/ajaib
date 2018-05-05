using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private Text interactText;
    public float range;
	private InteractableObject interactableObject;

	private Material originalMaterial;
	public Material highlightMaterial;

    void Start(){
		interactText = CanvasManager.instance.interactText;
    }

    void Update(){
		Interact ();
    }

	public void Interact (){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, range) && (hit.collider.gameObject.CompareTag("Interactable"))){
			if (interactableObject == null || interactableObject != hit.transform.GetComponent<InteractableObject> ()) {
				interactableObject = hit.transform.GetComponent<InteractableObject> ();
				originalMaterial = interactableObject.GetComponent<Renderer> ().material;
				interactableObject.GetComponent<Renderer> ().material = highlightMaterial;
			}
			interactText.text = interactableObject.GetInteractionText ();
			interactText.gameObject.SetActive (true);
			if (Input.GetButtonDown ("Interact"))
				interactableObject.Interact ();
		} else {
			interactText.gameObject.SetActive (false);
			if (interactableObject != null) {
				interactableObject.GetComponent<Renderer> ().material = originalMaterial;
				interactableObject = null;
			}
		}
	}
}
