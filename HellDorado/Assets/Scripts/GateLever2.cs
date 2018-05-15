using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLever2 : MonoBehaviour {
    public GameObject[] GateList1;
    public GameObject[] GateList2;
    public bool activated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Activate()
    {
        foreach(GameObject g in GateList1)
        {
            g.GetComponent<Gate>().OpenGate();
        }
        foreach(GameObject g in GateList2)
        {
            g.GetComponent<Gate>().CloseGate();
        }
    }
}
