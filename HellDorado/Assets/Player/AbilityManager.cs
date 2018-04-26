using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

	[Header("Rewind")]
	public bool isRewinding = false;
	public float recordTime = 5f;
	public GameObject rewindPanel;
    public int rewindCost = 1;
	private float playerGravity;
	List<PointInTime> pointsInTime;

	[Header("Fireball")]
	public GameObject fireballPrefab;
	public Transform fireballSpawn;
	public GameObject player;

    [Header("Rewind Butttons")]
    public GameObject rewindAvailable;
    public GameObject rewindUnavailable;
    public GameObject rewindActive;

    [Header("Fireball Buttons")]
    public GameObject fireballAvailable;
    public GameObject fireballUnavailable;
    public GameObject fireballActive;
	public int abilityCost;

    [Header("Push Buttons")]
    public GameObject pushAvailable;
    public GameObject pushUnavailable;
    public GameObject pushActive;

	public string currentAbility;
	public Text currentAbilityText;
	PlayerController _controller;

    void Start ()
    {
		pointsInTime = new List<PointInTime>();
		_controller = GetComponent<PlayerController>();
        currentAbility = "Rewind";
		_controller = GetComponent<PlayerController> ();
	}
	
	void Update ()
    {
        ChangeAbility();
		UpdateAbility ();
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
            GetComponent<PlayerScript>().damagePlayer();
            //GetComponent<PlayerScript>().changeHealth(-rewindCost);
        }
        else{
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
		rewindPanel.SetActive (true);
        Debug.Log(playerGravity);
	}

	public void StopRewind ()
	{
		isRewinding = false;
		_controller.Gravity = playerGravity;
		rewindPanel.SetActive (false);
        Debug.Log(playerGravity + " när vi slutat rewind");
	}


    private void ChangeAbility()
    {
        if (Input.GetKeyDown("1"))
        {
            currentAbility = "Rewind";

            rewindActive.SetActive(true);
            rewindAvailable.SetActive(false);
			rewindUnavailable.SetActive (false);
            fireballActive.SetActive(false);
            fireballAvailable.SetActive(true);
        }

        if (Input.GetKeyDown("2"))
        {
            currentAbility = "Fireball";

            fireballActive.SetActive(true);
            fireballAvailable.SetActive(false);
            fireballUnavailable.SetActive(false);

            rewindActive.SetActive(false);
            rewindAvailable.SetActive(true);
        }

        currentAbilityText.text = currentAbility;
    }


    public string GetCurrentAbility()
    {
        return currentAbility;
    }

	private void UpdateAbility() {
		if (currentAbility == "Rewind") {
			UpdateRewind ();
		}

		if (currentAbility == "Fireball") {
			FireFireball ();
		}
	}

	private void UpdateRewind() {
        if (player.GetComponent<PlayerScript>().health <= rewindCost)
            StopRewind();

		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			StartRewind ();
			_controller.TransitionTo<RewindState>();
		}

		if (Input.GetKeyUp(KeyCode.Mouse1))
			StopRewind();
	}

	private void FireFireball() {
		if (player.GetComponent<PlayerScript> ().health - abilityCost <= 0)
			return;
		if(Input.GetKeyDown(KeyCode.Mouse1)) {
			GameObject.Instantiate( fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
			player.GetComponent<PlayerScript>().changeHealth(-abilityCost);
		}
	}
}