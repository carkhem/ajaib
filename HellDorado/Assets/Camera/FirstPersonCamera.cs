using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;

    public MinMaxFloat minMax = new MinMaxFloat(-45.0f, 45.0f);
    public float horizontialSens = 10.0f;
    public float verticalSens = 10.0f;
    public float _rotationX = 0;

    private void Update()
    {
        if(axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * horizontialSens, 0);
        }else if (axes == RotationAxis.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * verticalSens;
            _rotationX = Mathf.Clamp(_rotationX, minMax.Min, minMax.Max);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
