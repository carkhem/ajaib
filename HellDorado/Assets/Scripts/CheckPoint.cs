using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private Vector3 position;


    public void SetPosition() {
        position = transform.position;
        GameManager.instance.SetCheckPoint(position);
    }

    public Vector3 getCheckPoint()
    {
        return position;
    }

    public void DestroyCheckPoint() {
        Destroy(this.gameObject);
    }


}