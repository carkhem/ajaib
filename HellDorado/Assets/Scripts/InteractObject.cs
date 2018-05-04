using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    private Text interactText;
    public float rayRange;

	private GameObject objectToHighlight;

    // Use this for initialization
    void Start(){
		interactText = CanvasManager.instance.interactText;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastObject();

		HighlightObject ();
    }

    public void RaycastObject()
    {

		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayRange) && (hit.collider.gameObject.tag == "Lever"))
        {
            GameObject Lever = hit.collider.gameObject;
            interactText.text = "Press [F] to interact with " + hit.collider.gameObject.tag;
            if (Input.GetKeyDown(KeyCode.F))
                Lever.GetComponent<GateLever>().open = !Lever.GetComponent<GateLever>().open;
        }
        else
        {
            interactText.text = "";
        }
    }

	public void HighlightObject()
	{

		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 50f) && (hit.collider.gameObject.tag == "Object"))
		{
			objectToHighlight = hit.collider.gameObject;
			//objectToHighlight.GetComponent<HighlightCube> ().hit = true;
		}
		else
		{
			//if(objectToHighlight != null)
				//objectToHighlight.GetComponent<HighlightCube> ().hit = false;
		}
	}
}
