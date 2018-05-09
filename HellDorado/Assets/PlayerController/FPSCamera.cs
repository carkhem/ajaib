using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

	public bool lockCursor = true;
	private bool m_cursorIsLocked = true;

	private Vector2 _mouseLook;
	private Vector2 _smoothV;
	public float Sensitivity = 5.0f;
	public float Smoothing = 2.0f;
	public MinMaxFloat yClamp;
	private bool isStatic = false;

	float yRotate = 0.0f;
	float xRotate = 0.0f;

	private GameObject player;

	void Start(){
		player = PlayerStats.instance.gameObject;
	}

	void Update(){
		if (!isStatic) {
			yRotate += Input.GetAxis ("Mouse Y") * Sensitivity;
			xRotate = Input.GetAxisRaw ("Mouse X") * Sensitivity + player.transform.eulerAngles.y;
			yRotate = Mathf.Clamp (yRotate, yClamp.Min, yClamp.Max);

			transform.localEulerAngles = new Vector3 (-yRotate, 0, 0.0f);
			player.transform.eulerAngles = new Vector3 (0, xRotate, 0.0f);

			UpdateCursorLock ();
		}
    }

	public void SetStatic (bool condition){
		
	}

	public void UpdateCursorLock()
	{
		//if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate();
	}

	private void InternalLockUpdate()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			m_cursorIsLocked = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (!m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

}
