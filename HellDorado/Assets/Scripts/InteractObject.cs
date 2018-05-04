using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
	private GameObject highLightObject;

    public GameObject player;
    public Text interactText;
    public float rayRange;
    private float DashLenght = 8;
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

	private void HighlightObject(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 50f) && (hit.collider.gameObject.tag == "Object")) {
			highLightObject = hit.collider.gameObject;
			hit.collider.gameObject.GetComponent<HighlightCube> ().distance = hit.distance;
			hit.collider.gameObject.GetComponent<HighlightCube> ().hit = true;
		
		} else {
			if (highLightObject != null)
				highLightObject.GetComponent<HighlightCube> ().hit = false;
		}
			

	}
}
