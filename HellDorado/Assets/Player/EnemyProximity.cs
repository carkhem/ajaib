using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximity : MonoBehaviour {



	void OnTriggerStay(Collider coll){

		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<PlayerProximity> ().activateRewind = true;
		
		}
	}
}
