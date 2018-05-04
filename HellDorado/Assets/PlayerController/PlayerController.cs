using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Controller{
    private Vector3 input;
    private Vector3 drag;
    [Header("Movement")]
    public float MaxSpeed = 10f;
    public float Gravity = 100f;
    private float dashLength;
    private float after;
    float a = 10;
    private float CrouchSpeed = 2;
    private float WalkingSpeed;
    private bool crouching = false;
	public float dashCooldown;
	private float dashTimer = 0;
	private bool canDash = true;

    [Header("Animation")]
	public Animator lArmAnim;
	public Animator rArmAnim;

    public Vector3 InputVector
    {
        get
        {
            input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), Velocity.y, UnityEngine.Input.GetAxisRaw("Vertical"));
//          UpdateCrouch();
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
		if ((Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0 || Input.GetAxisRaw ("Horizontal") != 0 && Input.GetAxisRaw ("Vertical") != 0) && (Input.GetKeyDown(KeyCode.LeftAlt) && canDash)) {
			Debug.Log ("DASH");
			canDash = false;
			TransitionTo<DashState> ();
		}
    }

    private void UpdateCrouch()
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

    public void StopCrouch()
    {
        Debug.Log("nu sluta vi croucha");
        crouching = false;
        GetComponent<CharacterController>().height = 2;
        GetComponent<PlayerStats>().sneaking = false;
        MaxSpeed = WalkingSpeed;
    }

    private void Crouch()
    {
        WalkingSpeed = MaxSpeed;
//        Debug.Log("nu crouchar vi");
        crouching = true;
        GetComponent<CharacterController>().height = 1;
        GetComponent<PlayerStats>().sneaking = true;
        MaxSpeed = CrouchSpeed;
    }
}