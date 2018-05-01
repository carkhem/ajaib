using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateKey : MonoBehaviour {

    public bool active;


	// Use this for initialization
	void Start () {
        active = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        //   transform.parent.GetComponent<LockedDoor>().increaseKeysUsed();
        // Destroy(this.gameObject);

        active = true;
    }
}
