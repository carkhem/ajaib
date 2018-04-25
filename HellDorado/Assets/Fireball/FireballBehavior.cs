using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour {
    public int damage = 10;
    private bool collided = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other + " " + other.tag);
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
            return;
        if (other.CompareTag("Enemy") && collided == false)
        {
            other.GetComponent<EnemyScript>().health -= damage;
            collided = true;
        }
        this.transform.localScale = new Vector3(2,2,2);
        Destroy(this.gameObject, 0.2f);
    }
}
