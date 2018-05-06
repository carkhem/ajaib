using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Controller{
    private Vector3 input;
    private Vector3 drag;
    [Header("Movement")]
    public float maxSpeed = 10f;
    public float gravity = 100f;
	[HideInInspector]
	public float movementSpeed;
    private float dashLength;
    private float after;
	private float crouchSpeed = 2;
    private bool crouching = false;
	public float dashCooldown;
	private float dashTimer = 0;
	private bool canDash = true;
    private int timesShift = 0;
    private float shiftTimer = 0;

    [Header("Animation")]
	public Animator lArmAnim;
	public Animator rArmAnim;

	void Start(){
		movementSpeed = maxSpeed;
	}

    public Vector3 InputVector
    {
        get
        {
            input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), Velocity.y, UnityEngine.Input.GetAxisRaw("Vertical"));
            float y = Camera.main.transform.rotation.eulerAngles.y;
            input = Quaternion.Euler(0f, y, 0f) * input;
            return input;
        }
    }

	public void CheckDash() {
		if (!canDash) {
			dashTimer += Time.deltaTime;
			if (dashTimer >= dashCooldown) {
				canDash = true;
				dashTimer = 0;
			}
		}
		if ((Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0 || Input.GetAxisRaw ("Horizontal") != 0 && Input.GetAxisRaw ("Vertical") != 0) && ((Input.GetKeyDown(KeyCode.LeftAlt))  || (Input.GetKeyDown(KeyCode.X))) && canDash) {
			canDash = false;
			TransitionTo<DashState> ();
		}
    }

	public void UpdateCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if(!crouching)
            Crouch();
        }
        if (Input.GetButtonUp("Crouch"))
        {
            if (crouching)
                StopCrouch();
        }
    }

    public void StopCrouch(){
        crouching = false;
        GetComponent<CharacterController>().height = 2;
        GetComponent<PlayerStats>().sneaking = false;
		movementSpeed = maxSpeed;
    }

    private void Crouch(){
        crouching = true;
        GetComponent<CharacterController>().height = 1;
        GetComponent<PlayerStats>().sneaking = true;
		movementSpeed = crouchSpeed;
    }

    public void sprintCheat()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            timesShift++;
            movementSpeed = maxSpeed;

            if (shiftTimer > 1.0f)
            {
                shiftTimer = 0;
                timesShift = 0;
            }
            if(shiftTimer != 0 && timesShift >= 4)
            {
                movementSpeed = 20;
                timesShift = 0;
                shiftTimer = 0;
            }
        }

        if(timesShift >=1)
            shiftTimer += Time.fixedDeltaTime;

    }
}