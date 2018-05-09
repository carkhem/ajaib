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
	private MinMaxFloat xClamp;
	private MinMaxFloat currentYClamp;
	private bool constrained;

	private float vrot;

	private GameObject player;

	void Start(){
		player = PlayerStats.instance.gameObject;
		currentYClamp = yClamp;
		constrained = false;
	}

	void Update(){
		var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		md = Vector2.Scale(md,new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));
		_smoothV.x = Mathf.Lerp (_smoothV.x, md.x, 1f / Smoothing);
		_smoothV.y = Mathf.Lerp (_smoothV.y, md.y, 1f / Smoothing);
		_mouseLook += _smoothV;

		_mouseLook.y = Mathf.Clamp (_mouseLook.y, currentYClamp.Min, currentYClamp.Max);
		transform.localRotation = Quaternion.AngleAxis (-_mouseLook.y, Vector3.right);
		Vector3 _rotation = new Vector3 (0, md.x, 0) * Sensitivity;
		player.transform.Rotate (_rotation);



//		transform.Rotate (Vector3.right, md.y);
//		transform.localRotation = Mathf.Clamp(md.y
//		print (md.y);
//		vrot -= Input.GetAxis ("Mouse Y") * Sensitivity;
//		vrot = Mathf.Clamp (vrot, xClamp.Min, xClamp.Max);
//		transform.localRotation = Quaternion.Euler (vrot, 0, 0);

			UpdateCursorLock ();
    }

	public void SetConstraints(float minX, float maxX, float minY, float maxY){
		currentYClamp.Min = minY;
		currentYClamp.Max = maxY;
		xClamp.Min = player.transform.eulerAngles.y + minX;
		xClamp.Max = player.transform.eulerAngles.y + maxX;
		constrained = true;
	}

	public void RemoveConstraints (){
		currentYClamp = yClamp;
		constrained = false;
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
