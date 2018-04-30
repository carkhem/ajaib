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
	public MinMaxFloat mouseClamp;
    public bool dead = false;

	GameObject Character;

	void Start(){
		Character = this.transform.parent.gameObject;
	}

	void Update(){
        if (!dead) { 
        var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		md = Vector2.Scale(md,new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));
		_smoothV.x = Mathf.Lerp (_smoothV.x, md.x, 1f / Smoothing);
		_smoothV.y = Mathf.Lerp (_smoothV.y, md.y, 1f / Smoothing);
		_mouseLook += _smoothV;

		_mouseLook.y = Mathf.Clamp (_mouseLook.y, mouseClamp.Min, mouseClamp.Max);

		transform.localRotation = Quaternion.AngleAxis (-_mouseLook.y, Vector3.right);
		Character.transform.localRotation = Quaternion.AngleAxis (_mouseLook.x, Character.transform.up);

		UpdateCursorLock ();
        }
    }

    public void SetDead(bool d)
    {
        dead = d;
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
