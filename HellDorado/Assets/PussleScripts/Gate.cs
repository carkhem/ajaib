using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public bool open = false;
	private Animator anim;

	private void Start(){
		anim = transform.GetChild (0).GetComponent<Animator> ();
		animOpen (open);
	}

	public void OpenGate(){
		open = true;
		animOpen (open);
	}

	public void CloseGate(){
		open = false;
		animOpen (open);
	}

	public void ToggleGate(){
		open = !open;
		animOpen (open);
	}

	private void animOpen(bool isOpen){
		anim.SetBool ("open", isOpen);
	}
	
}
