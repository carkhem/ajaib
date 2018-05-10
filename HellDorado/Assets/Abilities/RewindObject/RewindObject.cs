using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {

	private GameObject activeGameobject;
	public GameObject interactionIcon;
	private GameObject currentIcon;
    private GameObject clone;
    private GameObject objectToCloneFrom;

    private List<PointInTime> clonePointInTime;
    private List<PointInTime> originalPointInTime;

    public void UseRewindObject(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
			activeGameobject = hit.collider.gameObject;
			hit.collider.gameObject.GetComponent<ObjectTimeBody> ().StartRewind ();
		}
	}

	public void UpdateFeedback(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50) && (hit.collider.gameObject.tag == "Interactable")) && hit.collider.GetComponent<ObjectTimeBody> () != null) {
            if (!Input.GetButton("Fire2"))
            {
                if (objectToCloneFrom == null)
                {
                    objectToCloneFrom = hit.collider.gameObject;
                    clone = GameObject.Instantiate(objectToCloneFrom, objectToCloneFrom.transform.position, Quaternion.identity);
                    clone.GetComponent<BoxCollider>().enabled = false;
                    Destroy(clone.GetComponent<Rigidbody>());
                    clonePointInTime = objectToCloneFrom.GetComponent<ObjectTimeBody>().GetPointsInTime();
                    Destroy(clone.GetComponent<ObjectTimeBody>());
                    clone.GetComponent<ObjectTimeBody>().SetPointsInTime(clonePointInTime);
                    clone.transform.position = clonePointInTime[clonePointInTime.Count - 1].position;

                    //clone.GetComponent<ObjectTimeBody>().StartRewind();
                }
               
				if (currentIcon == null) {
				currentIcon = GameObject.Instantiate (interactionIcon, new Vector3 (hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.LookRotation (Camera.main.transform.position - hit.transform.position));
				} else {
					currentIcon.GetComponent<InteractionIcon> ().KeepAlive ();
				}
			}
		} else {
			Destroy (currentIcon);
            Destroy(clone);
            objectToCloneFrom = null;
		}
	}


	public GameObject HitInfo(){
		return activeGameobject;
	}


}
