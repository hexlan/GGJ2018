using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum MovementState
    {
        Stopped,
        Forward,
        Backward
    }

    public float acceleration;
    public float turnRate;
    public float maxSpeed;

    private MovementState movementState = MovementState.Stopped;
    private Rigidbody rigidBody;

    private void MoveForward(float angleDiffAbs, float angleDiff)
    {
        rigidBody.velocity += new Vector3(acceleration, 0.0f, 0.0f);

        if (angleDiffAbs < 1 || angleDiffAbs > 359)
        {
        }
        else if ((angleDiff <= 180 && angleDiff >= 0) || angleDiff <= -180)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0.0f, turnRate, 0.0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, turnRate, 0.0f));
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(rigidBody.velocity.magnitude, 0.0f, 0.0f);

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var joystickAngle = Mathf.Rad2Deg * (Mathf.Atan2(vertical, horizontal));
        if (joystickAngle < 0)
        {
            joystickAngle += 360;
        }
        var currentRotation = -(transform.rotation.eulerAngles.y - 90);
        if (currentRotation < 0)
        {
            currentRotation += 360;
        }

        var angleDiffAbs = Mathf.Abs(joystickAngle - currentRotation);
        var angleDiff = joystickAngle - currentRotation;

        if (Mathf.Abs(horizontal) > 0.05f || Mathf.Abs(vertical) > 0.05f)
        {
            if (movementState == MovementState.Forward)
            {
                MoveForward(angleDiffAbs, angleDiff);
            }
            else if (movementState == MovementState.Backward)
            {

            }
            else
            {
                movementState = MovementState.Forward;
                MoveForward(angleDiffAbs, angleDiff);
            }
        }
        else
        {
            if (rigidBody.velocity.magnitude > 0.05f)
            {
                rigidBody.velocity -= rigidBody.velocity.normalized * 15;
            }
        }

        if (rigidBody.velocity.magnitude > 0.05f)
        {
            rigidBody.velocity -= rigidBody.velocity.normalized * 5;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
            movementState = MovementState.Stopped;
        }

        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = new Vector3(maxSpeed, 0.0f, 0.0f);
        }
        else
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.magnitude, 0.0f, 0.0f);
        }

        var quaternion = Quaternion.Euler(new Vector3(0.0f, transform.rotation.eulerAngles.y - 90, 0.0f));
        rigidBody.velocity = quaternion * rigidBody.velocity;
    }
}
