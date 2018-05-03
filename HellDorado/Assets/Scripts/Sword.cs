using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public Animator anim;
//	public static bool extended;

	private void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Enemy")) {
			col.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
		}
	}

//	public void SetExtended(bool ex){
//		extended = ex;
//	}
}
