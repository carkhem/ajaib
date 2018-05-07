using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsTrap : MonoBehaviour {

    public GameObject[] objects;
    public bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter()
    {

        activated = true;
        ActivateTrap();
    }

    public void ActivateTrap()
    {
        for (int count = 0; count < objects.Length; count++)
        {
			Debug.Log ("boldertrap");
            objects[count].transform.GetComponent<TrapBoulder>().Activate();
         //   if (object[count].transform.GetComponent<ActivateKey>().active == false)
           // {
           //     return;
          //  }
        }
    }
}
