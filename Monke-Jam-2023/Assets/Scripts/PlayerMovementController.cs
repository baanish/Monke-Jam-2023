using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Player Movement controller script for 2D game

    // Declare variables
    [SerializeField]
    public float speed = 9f;
    [SerializeField]
    public float jumpForce = 700f;

    [SerializeField]
    public bool isGrounded = false;
    [SerializeField]
    public bool doubleJump = true;

    [SerializeField]
    public LayerMask groundCheck;

    [SerializeField]
    public long jumpCooldown = 800;
    [SerializeField]
    public long jumpCooldownTimer = 0;
    [SerializeField]
    public long jumpStart = 0;

    [SerializeField]
    public float gravity = 14.7f;

    void Start()
    {
        // Set the gravity
        Physics.gravity = new Vector3(0, -gravity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player
        var movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Jump if the player is pressing the spacebar, check if the player is on the ground allow a double jump.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //transform.GetComponent<Rigidbody>().AddForce(new Vector2(0, jumpForce));
            if (isGrounded)
            {
                // if the player is on the ground, jump
                transform.GetComponent<Rigidbody>().AddForce(new Vector2(0, jumpForce));
                jumpStart = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
                isGrounded = false;
            }
            else if (doubleJump)
            {
                //check if the double jump timer has expired
                jumpCooldownTimer = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
                if (jumpCooldownTimer - jumpStart > jumpCooldown)
                {
                    // if the player is not on the ground, check if they can double jump
                    transform.GetComponent<Rigidbody>().AddForce(new Vector2(0, jumpForce));
                    doubleJump = false;
                }
            }
        }

        //increase gravity if the player is holding s or down arrow
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Physics.gravity = new Vector3(0, -gravity * 2, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -gravity, 0);
        }

        // if velocity is > speed*1.5*sqrt(2), set velocity to speed*1.5*sqrt(2)
        if (transform.GetComponent<Rigidbody>().velocity.magnitude > speed * 1.5 * Mathf.Sqrt(2))
        {
            transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity.normalized * speed * 1.5f * Mathf.Sqrt(2);
        }
    }

    // Check if the player is on the ground
    private void OnCollisionEnter(Collision collision)
    {
        if ((groundCheck & 1 << collision.gameObject.layer) != 0)
        {
            isGrounded = true;
            doubleJump = true;
        }
    }
}
