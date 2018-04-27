using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour {
	public Sprite avalibleSprite;
//	public Sprite unavalibleSprite;
	public Sprite activeSprite;
	private Image image;

	void Awake(){
		image = GetComponent<Image> ();
	}

	void Start(){
		image.sprite = avalibleSprite;
	}

	public void SetActive(bool active){
		if (active)
			image.sprite = activeSprite;
		else
			image.sprite = avalibleSprite;
	}

}
