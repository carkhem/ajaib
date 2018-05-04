using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    private Text interactText;
    public float rayRange;

    // Use this for initialization
    void Start(){
		interactText = CanvasManager.instance.interactText;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastObject();
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
}
