using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ThirdPersonCamera : MonoBehaviour {

	public LayerMask CollisionLayers;
	public Transform Target;
	public Vector3 Offset;
	public float DefaultDistance = 5f;
	private Quaternion _rotation;
	private float _currentDistance;

	[Header("Rotation")]
	public MinMaxFloat XAngleClamp;
	public MinMaxFloat YAngleClamp;
	public Vector2 Speed;
	public float RequiredInputMagnitude = 0.5f;
	private float _xRotation;
	private float _yRotation;

	[Header("Zoom")]
	public float ZoomSpeed;
	public MinMaxFloat DistanceClamp;


	private SphereCollider _collider;

	private void Start() {
		_currentDistance = DefaultDistance;
		_collider = GetComponent<SphereCollider>();
	}
		
	private void Update() {
		UpdateRotation();
		UpdateDistance();
	}

	private void UpdateMovement() {
		transform.position = Target.position + Offset + _rotation * Vector3.back * _currentDistance;
		transform.LookAt(Target.position + Offset);
	}

	private void LateUpdate() {
		AdjustForCollision();
		UpdateMovement();
	}

	private void UpdateRotation() {
		Vector2 input = !Input.GetButton("Fire1") ? new Vector2(Input.GetAxisRaw("CameraX"), Input.GetAxisRaw("CameraY")) :new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		if (Mathf.Abs(input.x) > RequiredInputMagnitude)
			_xRotation += input.x * Speed.x * Time.deltaTime;
		if (Mathf.Abs(input.y) > RequiredInputMagnitude)
			_yRotation += input.y * Speed.y * Time.deltaTime;
		_xRotation = Mathf.Clamp(_xRotation, XAngleClamp.Min, XAngleClamp.Max);
		_yRotation = Mathf.Clamp(_yRotation, YAngleClamp.Min, YAngleClamp.Max);
		_rotation = Quaternion.Euler(_yRotation, _xRotation, 0.0f);
		if (_xRotation == 360 || _xRotation == -360)
			_xRotation = 0;
	}

	private void AdjustForCollision() {
		Vector3 desiredPosition = Target.position + Offset + _rotation * Vector3.back * DefaultDistance;
		RaycastHit hit;
		Physics.Linecast(Target.position + Offset, desiredPosition, out hit, CollisionLayers);
		_currentDistance =
			hit.collider != null ? hit.distance - _collider.radius : DefaultDistance;
	}

	private void UpdateDistance() {
		float input = Input.GetAxisRaw("Zoom");
		if (Mathf.Abs(input) > RequiredInputMagnitude)
			DefaultDistance += input * ZoomSpeed * Time.deltaTime;
		DefaultDistance = Mathf.Clamp(DefaultDistance, DistanceClamp.Min, DistanceClamp.Max);
	}
}
