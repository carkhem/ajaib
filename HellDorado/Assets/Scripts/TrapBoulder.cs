using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBoulder : MonoBehaviour {
    public float fallDelay;
    private bool active;
    private bool consumed;
    private Vector3 startPosition;
    private float timer;
    Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        timer = fallDelay;
        startPosition = transform.position;

        active = false;
        rigid = this.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (active == true && consumed == false)
            Fall();

    }
		

    public void Fall()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
			rigid.isKinematic = false;
            rigid.velocity = new Vector3(0f, -100f, 0f);
            consumed = true;

        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.transform.GetComponent<PlayerStats>().health = -1;
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Reset() {
        transform.position = startPosition;
        active = false;
        consumed = false;
        timer = fallDelay;
        rigid.isKinematic = true;
    }
}
