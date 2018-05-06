using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomArea : MonoBehaviour {

    public float spawnTimer = 2f;
    public GameObject prefab;
    private Vector3 spawnPosition;
    public int x;
    public int z;
    public int y;
    private Transform parent;
    private bool instantiateInWorldSpace = false;


	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnObject", 0f, spawnTimer);
        spawnPosition = new Vector3(0, 0, 0);
        parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
     
	}

    void SpawnObject()
    {
        spawnPosition = new Vector3(Random.Range(transform.localPosition.x, transform.localPosition.x + x), transform.localPosition.y, Random.Range(transform.localPosition.z, transform.localPosition.z + z));
             Instantiate(prefab, spawnPosition, Quaternion.identity, parent);
    }
    
}
