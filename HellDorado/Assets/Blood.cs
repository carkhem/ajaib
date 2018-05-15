using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {
    public float despawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Despawn();
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BloodPodium")
        {
            other.GetComponent<BloodPodium>().filled = true;
            Destroy(this.gameObject);
        }
    }

    void Despawn()
    {
        despawnTimer = despawnTimer +- Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
