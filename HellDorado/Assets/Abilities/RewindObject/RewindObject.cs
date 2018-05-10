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
    private int index = 0;
    Material ghostMaterial;

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
                    //----------------Ska flytta till metod pga spagetti
                    objectToCloneFrom = hit.collider.gameObject;
                    clone = GameObject.Instantiate(objectToCloneFrom, objectToCloneFrom.transform.position, Quaternion.identity);
                    clone.GetComponent<BoxCollider>().enabled = false;
                    clone.GetComponent<Rigidbody>().isKinematic = true;
                    clonePointInTime = objectToCloneFrom.GetComponent<ObjectTimeBody>().GetPointsInTime();
                    // Destroy(clone.GetComponent<ObjectTimeBody>());
                    Destroy(clone.GetComponent<PushableObject>());
                    Destroy(clone.GetComponent<FreezeTime>());
                    clone.GetComponent<ObjectTimeBody>().SetPointsInTime(clonePointInTime);
                  
                    //  clone.transform.position = clonePointInTime[clonePointInTime.Count - 1].position;


                }
                else {

                    if (clonePointInTime.Count > index)
                    {
                        PointInTime pointInTime = clonePointInTime[index];
                        clone.transform.position = pointInTime.position;
                        clone.transform.rotation = pointInTime.rotation;
                        index++;
                      //  clonePointInTime.RemoveAt(0);
                    }
                    
                }
               //--------------------
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
            index = 0;
		}
	}


	public GameObject HitInfo(){
		return activeGameobject;
	}


}
