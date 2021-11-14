using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody cam;
    public Transform camT;

    public float rotationSmooth = .12f;
    public float omega = 100f;
    public float distance = 5f;

    public bool LockCursor;

    public Vector2 minmax = new Vector2(-40, 85);
    Vector3 RotationSmoothV;
    Vector3 CurrentRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    float yaw = 0f;
    float pitch = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKey("up") || Input.GetKey("w"))
        {
            cam.AddForce(camT.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            cam.AddForce(-camT.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            cam.AddForce(camT.right, ForceMode.VelocityChange);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            cam.AddForce(-camT.right, ForceMode.VelocityChange);
        }

        yaw += Input.GetAxis("Mouse X") * omega * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * omega * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minmax.x, minmax.y);

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw), ref RotationSmoothV, rotationSmooth);

        transform.eulerAngles = CurrentRotation;
    }

}
