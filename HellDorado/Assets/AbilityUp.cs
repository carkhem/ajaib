using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUp : MonoBehaviour {

	public void SetAbility(int ammount){
		GameManager.instance.SetAbilityCount (ammount);
	}
}
