using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour {

    //private Vector3 position;
    //	private Quaternion rotation; kanske?
    public UnityEvent OnRespawn;

    public void SetCheckPoint() {
   
        GameManager.instance.SetCheckPoint(gameObject);
    }

	public Vector3 GetPosition()
	{
		return transform.position;
	}

    public void DestroyCheckPoint() {
        Destroy(this.gameObject);
    }

    public void RespawnEvent() {
        OnRespawn.Invoke();
    }

}