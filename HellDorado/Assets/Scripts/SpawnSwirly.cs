using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSwirly : MonoBehaviour {
    private Vector3 spawnPosition;
    public GameObject prefab;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
       
        if (Physics.Raycast(landingRay, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Ground")
            {
                spawnPosition = hit.point;
                Instantiate(prefab, spawnPosition, Quaternion.identity);
       //         Debug.Log(hit.point);
                            }
            
        }
	}
}
