using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Controller {

	public LayerMask ObjectLayer;

	[Header("Movement")]
	public float MaxSpeed = 10f;
	public float Gravity = 100f;

	[Header("Collision")]
	public float SkinWidth = 0.03f;


	public Vector3 Input {
		get{
			Vector3 input = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), Velocity.y, UnityEngine.Input.GetAxisRaw("Vertical"));
            float y = Camera.main.transform.rotation.eulerAngles.y;
            input = Quaternion.Euler(0f, y, 0f) * input;
			return input;
		}
	}

}