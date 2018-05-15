using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPodium : MonoBehaviour {
    public GameObject blood;
	public bool filled;

    private void Start()
    {
        filled = false;
        blood.SetActive(false);
    }

    private void Update()
    {
        if(filled == true) {
            blood.SetActive(true);
        }
    }

}
