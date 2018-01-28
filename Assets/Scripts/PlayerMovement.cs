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

    public bool isDamaged = false;
    public bool isInvulnerable = false;
    public bool isRespawning = false;
    public Quaternion initialAngle;
    public float acceleration;
    public float turnRate;
    public float maxSpeed;
    public string player;
    public float damageTime = 1.0f;
    public float invulTime = 2.0f;
    public float respawnTime = 3.0f;
    public float damageSpin = 7.0f;
    public float flicker = 1.8f;
    public float slowMultiplier = 10;

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

        var horizontal = Input.GetAxisRaw("Horizontal_P" + player);
        var vertical = Input.GetAxisRaw("Vertical_P" + player);

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

        if(isInvulnerable)
        {
            var renderer = transform.Find("Model").GetComponent<MeshRenderer>();

            if(invulTime < flicker)
            {
                renderer.enabled = !renderer.enabled;
                flicker -= 0.15f;
            }

            invulTime -= Time.deltaTime;
            if(invulTime < 0)
            {
                invulTime = 2.0f;
                isInvulnerable = false;
                renderer.enabled = true;
                flicker = 1.8f;
            }
        }

        if(isRespawning)
        {
            respawnTime -= Time.deltaTime;
            if(respawnTime < 0)
            {
                respawnTime = 3;
                isRespawning = false;
                isInvulnerable = true;
                transform.position = GetComponent<HUD>().respawnPosition;
            }
        }
        else if (isDamaged)
        {
            transform.Rotate(Vector3.up * damageSpin);
            damageTime -= Time.deltaTime;
            if (damageTime < 0)
            {
                isDamaged = false;
                damageTime = 1;
                transform.rotation = initialAngle;

                var hud = GetComponent<HUD>();
                if(hud.lives == 0)
                {
                    hud.carrying = 0;
                    hud.lives = 3;
                    isRespawning = true;
                    transform.position = new Vector3(1000, 0, 0);
                    transform.rotation = Quaternion.Euler(hud.respawnRotation);
                }
                else
                {
                    isInvulnerable = true;
                }
            }
        }
        else if (Mathf.Abs(horizontal) > 0.05f || Mathf.Abs(vertical) > 0.05f)
        {
            if (movementState == MovementState.Forward)
            {
                MoveForward(angleDiffAbs, angleDiff);
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

        var HUD = GetComponent<HUD>();
        var newMaxSpeed = Mathf.Clamp(maxSpeed - HUD.carrying * slowMultiplier, 100, 250);
        if (rigidBody.velocity.magnitude > newMaxSpeed)
        {
            rigidBody.velocity = new Vector3(newMaxSpeed, 0.0f, 0.0f);
        }
        else
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.magnitude, 0.0f, 0.0f);
        }

        var quaternion = Quaternion.Euler(new Vector3(0.0f, transform.rotation.eulerAngles.y - 90, 0.0f));
        rigidBody.velocity = quaternion * rigidBody.velocity;
    }
}
