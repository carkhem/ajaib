using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public Animator anim;
//	public static bool extended;

	private void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Enemy")) {
			if ((anim.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing 1") || anim.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing 1")) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
				col.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
		}
	}

//	public void SetExtended(bool ex){
//		extended = ex;
//	}
}
