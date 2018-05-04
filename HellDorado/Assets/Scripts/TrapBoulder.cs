using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBoulder : MonoBehaviour {
    public float fallDelay;
    private bool active;
    private bool consumed;
    Rigidbody rigid;

    // Use this for initialization
    void Start()
    {

        active = false;
        rigid = this.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (active == true && consumed == false)
            Fall();

    }

    void OnMouseDown()
    {
        rigid.useGravity = true;
    }

    public void Fall()
    {
        fallDelay -= Time.deltaTime;
        if (fallDelay < 0)
        {
            rigid.useGravity = true;
            consumed = true;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Activate()
    {
        active = true;
    }

    
}
