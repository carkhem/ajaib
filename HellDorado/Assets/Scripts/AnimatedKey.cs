using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimatedKey : MonoBehaviour {

	public PlayableDirector director;
	public Transform hand;
	private Vector3 endPos = new Vector3 (-13f, 1.6f, -1.7f);
	public Vector3 dropPos = new Vector3 (-10.8f, 1.9f, 1.1f);
    public GameObject magicalBox;
    private bool dropped = false;
	public void Update(){
		if (director == null)
			return;

		if (director.time > 0 && director.time < 6.52f) {
			Camera.main.GetComponent<Headbobber> ().enabled = false;
			PlayerStats.instance.transform.GetComponent<PlayerController> ().enabled = false;
			transform.localPosition = new Vector3 (0.0016f, -0.0017f, 0);
		} else  if (director.time >= 6.52 && director.time < 7) {
			if (!dropped) {
				transform.position = dropPos;
				dropped = true;
				GetComponent<AudioSource> ().PlayScheduled (0.5);
			}
			transform.SetParent (null);
			transform.GetComponent<BoxCollider> ().isTrigger = false;
		} else if (director.time >= 11.05 && director.time < 15.49) {
			transform.SetParent (hand, true);
			transform.GetComponent<BoxCollider> ().isTrigger = true;
			transform.localPosition = new Vector3 (0, 0.002f, 0.0006f);
			transform.localEulerAngles = new Vector3 (90, 90, 90);
		} else if (director.time >= 15.49 &&  director.time <= 16) {
			transform.SetParent (null);
			if (transform.position != endPos)
				transform.position = endPos;
			transform.GetComponent<BoxCollider> ().isTrigger = false;
			PlayerStats.instance.transform.GetComponent<PlayerController> ().enabled = true;
			Camera.main.GetComponent<Headbobber> ().enabled = true;
            magicalBox.GetComponent<AudioSource>().Play();
		}
	}


}
