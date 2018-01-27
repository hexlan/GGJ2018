using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Vector3 velocity;
    private MovementState movementState = MovementState.Stopped;

    public float turnRate;
    public float maxSpeed;
    public float acceleration;

    enum MovementState
    {
        Stopped,
        Forward,
        Backward
    }

    void Start()
    {
        velocity = Vector3.zero;
    }

    private void MoveForward(float angleDiffAbs, float angleDiff)
    {
        velocity += new Vector3(acceleration, 0.0f, 0.0f) * Time.deltaTime;

        if (angleDiffAbs < 1 || angleDiffAbs > 359) { }
        else if ((angleDiff <= 180 && angleDiff >= 0) || angleDiff <= -180)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0.0f, turnRate * Time.deltaTime, 0.0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, turnRate * Time.deltaTime, 0.0f));
        }
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var joystickAngle = Mathf.Rad2Deg * (Mathf.Atan2(vertical, horizontal));
        if (joystickAngle < 0)
        {
            joystickAngle += 360;
        }
        var currentRotation = transform.rotation.eulerAngles.y - 90;
        if (currentRotation != 0)
        {
            currentRotation = 360 - currentRotation;
        }

        var angleDiffAbs = Mathf.Abs(joystickAngle - currentRotation);
        var angleDiff = joystickAngle - currentRotation;

        if (Mathf.Abs(horizontal) > 0.05f || Mathf.Abs(vertical) > 0.05f)
        {
            if (movementState == MovementState.Forward)
            {
                if (angleDiffAbs > 135 && angleDiffAbs < 225)
                {
                    velocity -= velocity.normalized * 3 * Time.deltaTime;
                }
                else
                {
                    MoveForward(angleDiffAbs, angleDiff);
                }
            }
            else if (movementState == MovementState.Backward)
            {

            }
            else
            {
                if (angleDiffAbs > 90 && angleDiffAbs < 270)
                {
                    movementState = MovementState.Backward;
                }
                else
                {
                    movementState = MovementState.Forward;
                    MoveForward(angleDiffAbs, angleDiff);
                }
            }
        }

        if (velocity.magnitude > 0.05f)
        {
            velocity -= velocity.normalized * 5.0f * Time.deltaTime;
        }
        else
        {
            velocity = Vector3.zero;
            movementState = MovementState.Stopped;
        }

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        Debug.Log(velocity.magnitude);

        var quaternion = Quaternion.Euler(new Vector3(0.0f, transform.rotation.eulerAngles.y - 90, 0.0f));
        transform.position += quaternion * velocity;
    }

}
