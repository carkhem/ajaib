using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour {

    public string SceneToLoad;
    public int currentLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if(GameObject.Find("GameManager").GetComponent<GameManager>().levelsCompleted < currentLevel)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().levelsCompleted = currentLevel;
            }
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
