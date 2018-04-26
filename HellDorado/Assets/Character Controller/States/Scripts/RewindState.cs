using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Player/States/Rewind")]
public class RewindState : State {

    public bool rewinding;
    

    private PlayerController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
        
    }

	public override void Enter(){
		_controller.GetComponent<TimeBody> ().StartRewind ();
	}
	
	// Update is called once per frame
	public override void Update () {
        rewinding = _controller.GetComponent<TimeBody>().isRewinding; 

		if (!rewinding) {
			Debug.Log ("stop rewind");
			_controller.TransitionTo<GroundState> ();
		} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
			_controller.GetComponent<TimeBody> ().StopRewind ();
			_controller.TransitionTo<GroundState> ();
		}
	}
}
