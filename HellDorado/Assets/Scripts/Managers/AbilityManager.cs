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
	public ParticleSystem rewindParticles;

	[Header("Fireball")]
	public GameObject fireballPrefab;
	public Transform fireballSpawn;
	public GameObject player;
	public int fireCost;
	public float fireballDamage = 3;

	[Header("ForcePush")]
	public int forcePushCost;
	private ForcePush forcePush;
    public ParticleSystem pushParticles;

    [Header("ObjectRewind")]
	public int objectRewindCost;
	private RewindObject rewindObject;

    public GameObject abilityBar;
	AbilitySounds abilitySounds;
	PlayerController _controller;
	private PlayerStats stats;
	private GameManager gm;

    void Start (){
		stats = PlayerStats.instance;
		gm = GameManager.instance;
		_controller = GetComponent<PlayerController> ();
		selectedAbility = Ability.None;
		anim = GetComponent<PlayerController> ().lArmAnim;
		abilitySounds = GetComponent<AbilitySounds> ();
		abilityBar = CanvasManager.instance.abilityBar;
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
			if (Input.GetKeyDown ("1") && gm.abilityCount >= 1) {
				selectedAbility = Ability.Rewind;
				CanvasManager.instance.ChangeAbility (0);
                abilityBar.GetComponent<AbilityBar>().ChangeIcon(1);
                gm.keyPress();
            }


			if (Input.GetKeyDown ("2") && gm.abilityCount >= 2) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.ObjectRewind;
				CanvasManager.instance.ChangeAbility (1);
                abilityBar.GetComponent<AbilityBar>().ChangeIcon(2);
                gm.keyPress();
            }

			if (Input.GetKeyDown ("3") && gm.abilityCount >= 3) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.Fireball;
				CanvasManager.instance.ChangeAbility (2);
                abilityBar.GetComponent<AbilityBar>().ChangeIcon(3);
                gm.keyPress();
            }

			if (Input.GetKeyDown ("4") && gm.abilityCount >= 4) {
				if (TimeBody.isRewinding)
					return;
				selectedAbility = Ability.Push;
				CanvasManager.instance.ChangeAbility (3);
               abilityBar.GetComponent<AbilityBar>().ChangeIcon(4);
                gm.keyPress();
            }
		}
    }

	private void UpdateRewind() {
		if (player.GetComponent<PlayerStats> ().health > stats.maxHealth / 10) {
            if (player.GetComponent<PlayerStats>().health <= rewindCost)
            {
                TimeBody.isRewinding = false;
                //	StopRewind ();
            }

			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				rewindParticles.Play();
				FreezeTime.freezeTime = true;
				TimeBody.isRewinding = true;
				CanvasManager.instance.rewindPanel.SetActive (true);
				//StartRewind ();
				_controller.TransitionTo<RewindState> ();
				abilitySounds.PlayAbilitySound ("Rewind");
				_controller.lArmAnim.SetBool ("rewind", true);
			}

			if (Input.GetKeyUp (KeyCode.Mouse1)) {
				rewindParticles.Stop();
				FreezeTime.freezeTime = false;
				TimeBody.isRewinding = false;
				_controller.TransitionTo<GroundState> ();
				CanvasManager.instance.rewindPanel.SetActive (false);
				//StopRewind ();
				abilitySounds.StopPlayingAudio();
				_controller.lArmAnim.SetBool ("rewind", false);
			}
		} else {
			TimeBody.isRewinding = false;
		}
		if (TimeBody.isRewinding) {
			GetComponent<PlayerStats> ().DamagePlayer (rewindCost*GetComponent<TimeBody>().rewindSpeed);
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
		if (forcePush != null && stats.health > forcePushCost){
			player.GetComponent<PlayerStats> ().ChangeHealth (-forcePushCost);
			forcePush.ForcePushObject ();
			abilitySounds.PlayAbilitySound ("Push");
            pushParticles.Play();
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