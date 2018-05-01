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


	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnObject", 0f, spawnTimer);
        spawnPosition = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //CreatePrefab();
	}

    void SpawnObject()
    {
        spawnPosition = new Vector3(Random.Range(0, x), y, Random.Range(0, z));
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
    
}
