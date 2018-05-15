using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHand : MonoBehaviour {

	public float reach = 3f;

	public void Strike(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, reach)) {
			if (hit.transform.CompareTag("Enemy")){
				hit.transform.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
			}
			if (hit.transform.CompareTag("Player")){
				PlayerStats.instance.ChangeHealth (-20);
			}
		}
	}
}
