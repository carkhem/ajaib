using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Player/States/Rewind")]
public class RewindState : State {
	// -------------- Vi behöver inget här. All Rewind görs ändå i Ability Managern. Det här statet finns bara för att vi inte ska kunna göra nåt annat när vi rewindar. --------------------------- //

//    public bool rewinding;
    
    private PlayerController _controller;
//    private AbilityManager am;

    public override void Initialize(Controller owner) {
        _controller = (PlayerController)owner;
        
    }

	public override void Enter(){
//		Debug.Log ("Rewind State");
//        _controller.GetComponent<AbilityManager>().StartRewind ();
	}
	
	// Update is called once per frame
	public override void Update () {
//        rewinding = _controller.GetComponent<AbilityManager>().isRewinding; 
//
//		if (!rewinding) {
//			Debug.Log ("stop rewind");
//			_controller.TransitionTo<GroundState> ();
//		} else if (Input.GetAxisRaw ("Fire2") == 0) {
//			_controller.GetComponent<AbilityManager> ().StopRewind ();
//			_controller.TransitionTo<GroundState> ();
//		}
	}
}
