using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private Vector3 position;
//	private Quaternion rotation; kanske?

    public void SetPosition() {
        position = transform.position;
        GameManager.instance.SetCheckPoint(position);
    }

    public Vector3 GetCheckPoint()
    {
        return position;
    }

	public Vector3 GetPosition()
	{
		return position;
	}

    public void DestroyCheckPoint() {
        Destroy(this.gameObject);
    }


}