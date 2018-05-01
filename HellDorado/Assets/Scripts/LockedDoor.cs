using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {


    public GameObject[] keys;
    public bool locked;
    // public int keysUsed;


    // Use this for initialization
    void Start() {
        locked = true;
       // keysUsed = 0;
    }

    // Update is called once per frame
    void Update() {
        checkKeyStatus();
        // Destroy(this.gameObject);
    }

    void checkKeyStatus()
    {
        for (int count = 0; count < keys.Length; count++)
        {
            if (keys[count].transform.GetComponent<ActivateKey>().active == false)
            {
                return;
            }
        }
        locked = false;
    }
}

/*    void checkKeysUsed()
    {
        if (keysUsed == keys.Length)
        {
            locked = false;
        }
    }

    void Open()
    {
        if(locked == false)
        {
            Destroy(this.gameObject);
        }
    }



    public void increaseKeysUsed()
    {
        keysUsed++;
    }

    
}
*/
