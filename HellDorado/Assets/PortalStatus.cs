using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalStatus : MonoBehaviour {
    private bool active;
    public int levelsBeatenRequired;

	// Use this for initialization
	void Start () {

        if (GameObject.Find("GameManager").GetComponent<GameManager>().levelsCompleted >= levelsBeatenRequired)
        {
            active = true;
        } else
        {
            active = false;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
        if (active == false)
        {
            this.gameObject.SetActive(false);
        }
		
	}
}
