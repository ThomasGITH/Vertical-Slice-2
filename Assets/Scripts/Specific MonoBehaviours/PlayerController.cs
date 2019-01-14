using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Components.
	[HideInInspector]
	public Rigidbody2D rb;
	private Animator anim;
	private float gravity = 2f;

	// Movement.
	private float speed = 3f;
	private float horizontalMove, verticalMove;
	private Vector2 movement;

	// Jump.
	private Vector2 jumpForce = new Vector2(0, 10f);

	// Grounded
	public LayerMask whatIsGround; // Inspector.
	public Transform groundCheck; // Inspector.
	public bool grounded;
	private float groundCheckRadius;

	// Look direction
	private bool facingRight = false;



	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		rb.gravityScale = gravity;
	}

	void Update()
	{
		Movement();
		Facing();

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
		Debug.Log("Jump called");
		verticalMove = Input.GetAxis("Vertical");
		rb.AddForce(jumpForce, ForceMode2D.Impulse);
	}

	void Facing() // calls FlipSprite()
	{
		if (!facingRight && rb.velocity.x > 0 || facingRight && rb.velocity.x < 0)
		{
			FlipSprite();
		}
	}

	void FlipSprite()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
