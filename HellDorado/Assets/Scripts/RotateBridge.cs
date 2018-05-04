using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBridge : MonoBehaviour {

    public float amountToRotate;
    public GameObject objectToRotate;
    private bool active;
    
	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (active == false)
        {
            objectToRotate.transform.Rotate(0, amountToRotate, 0, Space.Self);
            active = true;
        }
        else if( active == true)
        {
            objectToRotate.transform.Rotate(0, -amountToRotate, 0, Space.Self);
            active = false;
        }
    }
}
