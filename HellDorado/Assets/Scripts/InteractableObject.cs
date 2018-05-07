using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {
	public string pressFTo;
	public bool showText = true;
	public UnityEvent Interaction;

	public void Interact(){
		Interaction.Invoke ();
	}
	public string GetInteractionText (){
		return "Press [F] to " + pressFTo;
	}
}
