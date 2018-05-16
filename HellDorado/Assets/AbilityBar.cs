using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour {

    public Sprite RewindIcon;
    public Sprite RewindObject;
    public Sprite FireballIcon;
    public Sprite forcePushIcon;
    public Sprite macuahuitl;
    public Image Icon;
    public Image weaponImage;
    public GameObject Ability;
    public GameObject Weapon;
    // Use this for initialization
    void Start () {
        Icon = Ability.GetComponent<Image>();
        weaponImage = Weapon.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Icon.sprite == null)
        {
            Ability.SetActive(false);
        }
        else
            Ability.SetActive(true);

        if (weaponImage.sprite == null)
            Weapon.SetActive(false);
        else
            Weapon.SetActive(true);
    }

    public void ChangeIcon(int switchCmd)
    {
        switch (switchCmd) {
            case 1:
                Icon.sprite = RewindIcon;
                break;
            case 2:
                Icon.sprite = RewindObject;
                break;
            case 3:
                Icon.sprite = FireballIcon;
                break;

            case 4:
                Icon.sprite = forcePushIcon;
                break;

        }
    }

    public void ChangeWeapon(int switchCommand)
    {
        switch (switchCommand)
        {
            case 1:
                weaponImage.sprite = macuahuitl;
                break;
        }
    }
}
