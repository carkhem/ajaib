using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceIdentifier : MonoBehaviour {

    private Vector3 direction;
    public float distance;
    private RaycastHit hit;
    public string surfaceType;

	// Use this for initialization
	void Start () {
        direction =  new Vector3(0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, direction * distance, Color.green);
        if((Physics.Raycast(transform.position, direction, out hit, distance))) {

            if (hit.collider.gameObject.tag == "Blood") {
                surfaceType = "Blood";
                return;
            }

            if (hit.collider.gameObject.tag == "Wood") {
                surfaceType = "Wood";
                return;

            }else{
                surfaceType = "Default";
            }
        }
	}

    public string GetSurfaceType()
    {
        return surfaceType;
    }
}
