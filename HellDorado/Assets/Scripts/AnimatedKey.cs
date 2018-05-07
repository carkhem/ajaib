using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimatedKey : MonoBehaviour {

	public PlayableDirector director;
	public Transform hand;

	public void Update(){
		if (director == null)
			return;
		
		if (director.time >= 10.58 && director.time <= 12) {
			transform.SetParent (hand);
		}
//		if (director.time >= 8 &&  director.time <= 9) {
//			transform.SetParent (hand);
//		}
	}

}
