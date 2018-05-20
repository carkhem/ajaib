using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

	public string[] interactableTags = {"Player", "Interactable"};
	public UnityEvent OnPressureEnter;
	public UnityEvent OnPressureExit;
    public AudioClip clip;
    private int objects = 0;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects++;
                //GetComponent<Animator> ().SetBool ("activate", true);
                PlaySound();
                OnPressureEnter.Invoke ();
				break;
			}
		}
	}

	void OnTriggerExit(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects--;
				//GetComponent<Animator> ().SetBool ("activate", false);
				if (objects == 0) {
					OnPressureExit.Invoke ();
				}
				break;
			}
		}
	}

    private void PlaySound() {
        if (!source.isPlaying) {
            source.PlayOneShot(clip);
        }
    }
}
