using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

	private Animator anim;
	public enum Ability{
		None, Rewind, Fireball, Push, ObjectRewind
	}
	public Ability selectedAbility;

	[Header("Rewind")]
	public bool isRewinding = false;
	public float recordTime = 5f;
	public float rewindCost = 20;
	public static bool WorldRewind = false;

	[Header("Fireball")]
	public GameObject fireballPrefab;
	public Transform fireballSpawn;
	public GameObject player;
	public int fireCost;
	public float fireballDamage = 3;

	[Header("ForcePush")]
	public int forcePushCost;
	private ForcePush forcePush;

	[Header("ObjectRewind")]
	public int objectRewindCost;
	private RewindObject rewindObject;

	AbilitySounds abilitySounds;
	PlayerController _controller;
	private PlayerStats stats;

    void Start (){
		stats = PlayerStats.instance;
		_controller = GetComponent<PlayerController> ();
		selectedAbility = Ability.None;
		anim = GetComponent<PlayerController> ().lArmAnim;
		abilitySounds = GetComponent<AbilitySounds> ();
	}
	
	void Update (){
        ChangeAbility();

		switch (selectedAbility) {
		case Ability.Rewind:
			UpdateRewind ();
			break;
		case Ability.Fireball:
			if (Input.GetButtonDown ("Fire2")) {
				FireFireball ();
				anim.SetTrigger ("push");
			}
			break;
		case Ability.Push:
			if (Input.GetButtonDown("Fire2")) {
				UseForcePush ();
				anim.SetTrigger ("push");
			}
			break;
		case Ability.ObjectRewind:
			UseRewindObject();
			break;
		default:
			break;
		}
	}

    private void ChangeAbility(){
		if (!TimeBody.isRewinding) {
			if (Input.GetKeyDown ("1") && stats.playerLevel >= 1) {
				print (stats.playerLevel);
				selectedAbility = Ability.Rewind;
				CanvasManager.instance.ChangeAbility (0);
			}


			if (Input.GetKeyDown ("2") && stats.playerLevel >= 1) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.ObjectRewind;
				CanvasManager.instance.ChangeAbility (1);
			}

			if (Input.GetKeyDown ("3") && stats.playerLevel >= 2) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.Fireball;
				CanvasManager.instance.ChangeAbility (2);
			}

			if (Input.GetKeyDown ("4") && stats.playerLevel >= 3) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.Push;
				CanvasManager.instance.ChangeAbility (3);
			}
		}
    }

	private void UpdateRewind() {
		if (player.GetComponent<PlayerStats> ().health > 10) {
            if (player.GetComponent<PlayerStats>().health <= rewindCost)
            {
                TimeBody.isRewinding = false;
                //	StopRewind ();
            }

			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = true;
				TimeBody.isRewinding = true;
				CanvasManager.instance.rewindPanel.SetActive (true);
				//StartRewind ();
				_controller.TransitionTo<RewindState> ();
				abilitySounds.PlayAbilitySound ("Rewind");
			}

			if (Input.GetKeyUp (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = false;
				TimeBody.isRewinding = false;
				_controller.TransitionTo<GroundState> ();
				CanvasManager.instance.rewindPanel.SetActive (false);
				//StopRewind ();
				abilitySounds.StopPlayingAudio();

			}
		} else {
			TimeBody.isRewinding = false;
		}
		if (TimeBody.isRewinding) {
			GetComponent<PlayerStats> ().DamagePlayer (rewindCost);
		} else {
			CanvasManager.instance.rewindPanel.SetActive (false);
			abilitySounds.StopPlayingAudio();
		}

	}

	private void FireFireball() {
        if (player.GetComponent<PlayerStats>().health - fireCost >= 10)
        {
//            print("Shooting");
            GameObject ball = Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
			ball.GetComponent<Fireball2> ().SetMaxDamage (fireballDamage);
            player.GetComponent<PlayerStats>().ChangeHealth(-fireCost);
			abilitySounds.PlayAbilitySound ("Fireball");
        }
	}

	private void UseForcePush(){
		forcePush = GetComponent<ForcePush> ();
		if (forcePush != null){
			player.GetComponent<PlayerStats> ().ChangeHealth (-forcePushCost);
			forcePush.ForcePushObject ();
			abilitySounds.PlayAbilitySound ("Push");
		}
	}

	private void UseRewindObject(){
		rewindObject = GetComponent<RewindObject> ();
		if (rewindObject != null) {
			rewindObject.UpdateFeedback ();
			if (Input.GetButtonDown ("Fire2")) {
				rewindObject.UseRewindObject ();
				player.GetComponent<PlayerStats> ().ChangeHealth (-objectRewindCost);
			} else if (Input.GetKeyUp (KeyCode.Mouse1)) {
				if (rewindObject.HitInfo () != null) {
					if(rewindObject.HitInfo ().GetComponent<ObjectTimeBody> ().isRewinding)
						rewindObject.HitInfo ().GetComponent<ObjectTimeBody> ().StopRewind ();
				}
			}
		}
	}
}