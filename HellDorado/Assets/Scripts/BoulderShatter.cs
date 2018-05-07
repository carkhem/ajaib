using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderShatter : MonoBehaviour {

    public string tagThatDestroys;
    public GameObject ShatterEffect;
    //private Quaternion rotation = new Quaternion(45, 45, 0, 0);

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagThatDestroys)
            {
            Instantiate(ShatterEffect, this.transform.position, Quaternion.EulerAngles(Vector3.up) );
            Destroy(this.gameObject);
            
            }
            else if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().health -= 10;
        }
        }
    }
