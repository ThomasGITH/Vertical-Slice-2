using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Components.
	private Rigidbody2D rb;
	private Animator anim;

	// Movement.
	private float speed = 3f;
	private float horizontalMove, verticalMove;
	private Vector2 movement;

	// Jump.
	private Vector2 jumpForce = new Vector2(0, 10f);

	// Grounded
	public LayerMask whatIsGround; // Inspector.
	public Transform groundCheck; // Inspector.
	[SerializeField]private bool grounded;
	private float groundCheckRadius;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    void Update()
    {
		Movement();
		Grounded();

		if (grounded)
		{
			if (Input.GetButtonDown("Jump"))
				Jump();
		}
    }

	private void Movement()
	{
		if (Input.GetAxis("Horizontal") != 0)
		{
			horizontalMove = Input.GetAxis("Horizontal") * speed;
			movement = new Vector2(horizontalMove, rb.velocity.y);
			rb.velocity = Vector2.Lerp(rb.velocity, movement, 15 * Time.deltaTime);
		}
	}

	private void Jump()
	{
		verticalMove = Input.GetAxis("Vertical");
		rb.AddForce(jumpForce, ForceMode2D.Impulse); 
	}

	// Check if the player is on the ground.
	private void Grounded()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (grounded)
		{
			anim.SetBool("Grounded", true);
		}
		else
		{
			anim.SetBool("Grounded", false);
		}
	}
}
