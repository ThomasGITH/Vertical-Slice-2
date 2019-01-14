using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public float jumppower;
    public float walkSpeed;

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumppower = jumppower > 0 ? jumppower : 200.0f;
        velocity = Vector2.zero;
        walkSpeed = walkSpeed > 0 ? walkSpeed : 10.0f;
    }

    void Update()
    {
        velocity.x = Input.GetAxis("Horizontal") * walkSpeed;

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        rb.velocity = new Vector2(Mathf.Clamp(velocity.x, -3, 3) ,rb.velocity.y);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumppower);
    }
}
