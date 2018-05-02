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
    public int rewindCost = 1;
	private float playerGravity;
	List<PointInTime> pointsInTime;

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
		pointsInTime = new List<PointInTime>();
		_controller = GetComponent<PlayerController>();
		_controller = GetComponent<PlayerController> ();
		selectedAbility = Ability.None;
		playerGravity = _controller.Gravity;
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

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind ()
	{
       if (pointsInTime.Count > 0)
            {
                PointInTime pointInTime = pointsInTime[0];
                transform.position = pointInTime.position;

                transform.rotation = pointInTime.rotation;
                pointsInTime.RemoveAt(0);
                GetComponent<PlayerStats>().DamagePlayer(rewindCost);
            }
            else
            {
                StopRewind();
            }
        
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind ()
{   
        if (!isRewinding)
        {
            playerGravity = _controller.Gravity;
            isRewinding = true;
        }
		_controller.Gravity = 0;
		CanvasManager.instance.rewindPanel.SetActive (true);
	}

	public void StopRewind (){
		isRewinding = false;
		_controller.Gravity = playerGravity;
		_controller.TransitionTo<GroundState> ();
		CanvasManager.instance.rewindPanel.SetActive (false);
        Debug.Log(playerGravity + " när vi slutat rewind");
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
				StopRewind ();


			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = true;
				StartRewind ();
				_controller.TransitionTo<RewindState> ();
			}

			if (Input.GetKeyUp (KeyCode.Mouse1)) {
				FreezeTime.freezeTime = false;
				StopRewind ();
			}
		}
	}


	private void FireFireball() {
        if (player.GetComponent<PlayerStats>().health - fireCost >= 10)
        {
            Debug.Log(player.GetComponent<PlayerStats>().health - fireCost);

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                print("Shooting");
                Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
                player.GetComponent<PlayerStats>().ChangeHealth(-fireCost);
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