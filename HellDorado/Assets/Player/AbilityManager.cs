using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

	public enum Ability{
		None, Rewind, Fireball, Push, ObjectRewind
	}
	public Ability selectedAbility;

	[Header("Rewind")]
	public bool isRewinding = false;
	public float recordTime = 5f;
	public float rewindCost = 1;
	public static bool WorldRewind = false;

	[Header("Fireball")]
	public GameObject fireballPrefab;
	public Transform fireballSpawn;
	public GameObject player;
	public int fireCost;

	[Header("ForcePush")]
	public int forcePushCost;
	private ForcePush forcePush;

	[Header("ObjectRewind")]
	public int objectRewindCost;
	private RewindObject rewindObject;

	PlayerController _controller;

    void Start (){
		_controller = GetComponent<PlayerController> ();
		selectedAbility = Ability.None;
	}
	
	void Update (){
        ChangeAbility();

		switch (selectedAbility) {
		case Ability.Rewind:
			UpdateRewind ();
			break;
		case Ability.Fireball:
			FireFireball ();
			break;
		case Ability.Push:
			UseForcePush ();
			break;
		case Ability.ObjectRewind:
			UseRewindObject();
			break;
		default:
			break;
		}

	}
		


    private void ChangeAbility(){
		if (Input.GetKeyDown("1") && GameManager.instance.playerLevel >= 1){
			selectedAbility = Ability.Rewind;
			CanvasManager.instance.ChangeAbility (0);
        }


        if (Input.GetKeyDown("2") && GameManager.instance.playerLevel >= 1)
        {
            selectedAbility = Ability.ObjectRewind;
            CanvasManager.instance.ChangeAbility(1);
        }

        if (Input.GetKeyDown("3") && GameManager.instance.playerLevel >= 2)
        {
            selectedAbility = Ability.Fireball;
            CanvasManager.instance.ChangeAbility(2);
        }

        if (Input.GetKeyDown("4") && GameManager.instance.playerLevel >= 3)
        {
            selectedAbility = Ability.Push;
            CanvasManager.instance.ChangeAbility(3);
        }

    }

	private void UpdateRewind() {
		if (player.GetComponent<PlayerStats> ().health > 10) {
			if (player.GetComponent<PlayerStats> ().health <= rewindCost)
				TimeBody.isRewinding = false;
			//	StopRewind ();


			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = true;
				TimeBody.isRewinding = true;
				GetComponent<PlayerStats>().DamagePlayer(rewindCost);
				CanvasManager.instance.rewindPanel.SetActive (true);
				//StartRewind ();
				_controller.TransitionTo<RewindState> ();
			}

			if (Input.GetKeyUp (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = false;
				TimeBody.isRewinding = false;
				_controller.TransitionTo<GroundState> ();
				CanvasManager.instance.rewindPanel.SetActive (false);
				//StopRewind ();
			}
		} else {
			TimeBody.isRewinding = false;
		}
	}

	private void FireFireball() {
        if (player.GetComponent<PlayerStats>().health - fireCost >= 10)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                print("Shooting");
                Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
                player.GetComponent<PlayerStats>().ChangeHealth(-fireCost);
				GetComponent<AbilitySounds> ().PlayAbilitySound ("Fireball");
            }
        }
	}

	private void UseForcePush(){
		forcePush = GetComponent<ForcePush> ();
        if (forcePush != null)
            forcePush.ForcePushObject (forcePushCost);
	}

	private void UseRewindObject(){
		rewindObject = GetComponent<RewindObject> ();
        if (rewindObject != null)
            rewindObject.UseRewindObject (rewindCost);
       
	}
}