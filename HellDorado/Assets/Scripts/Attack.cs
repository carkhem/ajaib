using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public bool canAttack = true;
	public float reach = 3f;
	public Vector3 offset;

	public void Update(){
		Debug.DrawRay (transform.position + offset, transform.TransformDirection (Vector3.forward) * reach, Color.yellow);
	}

	public void Strike(){
		if (!canAttack)
			return;
		
		RaycastHit hit;
		if (Physics.Raycast (transform.position + offset, transform.TransformDirection (Vector3.forward), out hit, reach)) {
			print ("Attack Player");
			if (hit.transform.CompareTag("Enemy")){
				hit.transform.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
			}
			if (hit.transform.CompareTag("Player")){
				PlayerStats.instance.ChangeHealth (-20);
				Camera.main.GetComponent<Animator> ().SetTrigger ("hit");
			}
		}
	}
}
