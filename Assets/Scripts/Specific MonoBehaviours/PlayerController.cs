﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	// Components.
	[HideInInspector]
	public Rigidbody2D rb;
	private Animator anim;
	private float gravity = 4f;
	public float velocity;

	// Movement.
	private float speed = 7f;
	private float horizontalMove, verticalMove;
	private Vector2 movement;

	// Jump.
	private Vector2 jumpForce = new Vector2(0, 15f);

	// Grounded.
	public LayerMask whatIsGround; // Inspector.
	public Transform groundCheck; // Inspector.
	public bool grounded;
	private float groundCheckRadius;

	// Look direction.
	private bool facingRight = false;

	// Flamethrower event.
	public delegate void onFlamethrower();
	public static event onFlamethrower evt_flames;
	public delegate void onStopFlamethrower();
	public static event onStopFlamethrower evt_stopFlames;

	public delegate void onPlayerAttack();
	public static event onPlayerAttack evt_playerAttack;

	// Death event.
	public delegate void onDeath();
	public static event onDeath evt_death;



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

		if (grounded && Input.GetButtonDown("Jump")) Jump();

		if (Input.GetKey(KeyCode.LeftShift)) evt_flames();

		if (Input.GetKeyUp(KeyCode.LeftShift)) evt_stopFlames();
	}

	private void Movement()
	{
		if (Input.GetAxis("Horizontal") != 0)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
			movement = new Vector2(horizontalMove, rb.velocity.y);
			rb.velocity = Vector2.Lerp(rb.velocity, movement, 15 * Time.deltaTime);
		}
		else
		{
			horizontalMove = 0;
			movement = new Vector2(horizontalMove, rb.velocity.y);
			rb.velocity = Vector2.Lerp(rb.velocity, movement, 15 * Time.deltaTime);
		}
	}

	private void Jump()
	{
		verticalMove = Input.GetAxis("Vertical");
		rb.AddForce(jumpForce, ForceMode2D.Impulse);
	}

	void Facing() // calls FlipSprite()
	{
		if (!facingRight && Input.GetAxis("Horizontal") > 0 || facingRight && Input.GetAxis("Horizontal") < 0) FlipSprite();
	}

	void FlipSprite()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
