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

    [Header("Animation")]
	public Animator lArmAnim;
	public Animator rArmAnim;

    //	[Header("Collision")]
    //	public float SkinWidth = 0.03f;


    public Vector3 InputVector
    {
        get
        {
            input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), Velocity.y, UnityEngine.Input.GetAxisRaw("Vertical"));
//            Dash();
//            UpdateCrouch();
            float y = Camera.main.transform.rotation.eulerAngles.y;
            input = Quaternion.Euler(0f, y, 0f) * input;
            return input;
        }
    }

    private void Dash() {
        a += Time.fixedDeltaTime;
       // Debug.Log(a);
        if (Input.GetKeyDown(KeyCode.E))
        {
            a = 0;
            dashLength = 4;
            //input.x += dashLength;
            Debug.Log("E");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            a = 0;
            dashLength = -4;
            //input.x -= dashLength;
            Debug.Log("Q" + input);
        }
        if (a < 0.5f)
        {
            input.x += dashLength;
           
        }
        //if(isDashing) { 
        //    after = Mathf.Lerp(input.x, dashLength, 0.1f);
        //    Debug.Log(after);
        //    Debug.Log("skillnaden mellan input och after" + input.x + " - " + after);
        //    if (dashLength == after)
        //        isDashing = false;
        //}
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