using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour {

    public Sprite noChosenRam;
    public Sprite chosenRam;
    public Sprite usingRam;
    public Image Ram;
    public GameObject Ability;
    // Use this for initialization
    void Start () {
        Ram = GetComponent<Image>();
        Ram.sprite = noChosenRam; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeRam(int switchCmd)
    {
        switch (switchCmd) {
            case 1:
                Ram.sprite = noChosenRam;
                break;
            case 2:
                Ram.sprite = chosenRam;
                break;
            case 3:
                Ram.sprite = usingRam;
                break;




        }
        
    }
}
