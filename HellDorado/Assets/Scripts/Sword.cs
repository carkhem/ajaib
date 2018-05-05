using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public Animator anim;
	public enum SwordTypes{
		Firendly, Hostile
	}
	public SwordTypes swordType = SwordTypes.Firendly;
	public float damage = 20;
//	public static bool extended;

	private void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Enemy") && swordType == SwordTypes.Firendly) {
			if ((anim.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing 1") || anim.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing 1")) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
				col.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
		}
		if (col.CompareTag ("Player") && swordType == SwordTypes.Hostile) {
			col.GetComponent<PlayerStats> ().ChangeHealth (-damage);
		}
	}

//	public void SetExtended(bool ex){
//		extended = ex;
//	}
}
