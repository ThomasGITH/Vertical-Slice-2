using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	[SerializeField] public float speed;
	[SerializeField] private float moveSpeed;
	private float health = 40;

	public bool walkingClockwards, facingLeft, flipSprite, useRigidbody;

	// Components.
	[HideInInspector] public Animator anim;
	private Rigidbody2D rb;
	private PlayerStats _stats;
	private Enemy _enemy;
	private BirdAttack _birdAtk;
	public GameObject target; // Drag inspector.

	private Vector3 theScale;
	private float x, x2;

	// Location.
	private Vector2 spawnPoint;

	// Attack.
	public float distance, attackRange, chargeRange, attackCooldownStart, attackCooldown;
	public bool isCharging, isAttacking, isIdle;


	private void Start()
	{
		// Components.
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		_enemy = GetComponent<Enemy>();
		_stats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
		_birdAtk = FindObjectOfType<BirdAttack>();

		moveSpeed = speed;
		attackRange = 1.5f;
		chargeRange = 4f;

		theScale = transform.localScale;
		x = transform.localScale.x;
		x2 = x * -1;

		// Location.
		spawnPoint = transform.position;

		// Attack.
		attackCooldown = 0;
	}

	private void Update()
	{
		// Facing.
		if (_enemy.health > 0)
		{
			if (target.transform.position.x < transform.position.x) facingLeft = true;
			if (target.transform.position.x > transform.position.x) facingLeft = false;
			transform.localScale = new Vector3(facingLeft ? x2 : x, transform.localScale.y, transform.localScale.z);
			RangeCheck();
		}

		// Attack.
		attackCooldownStart += Time.deltaTime;
		if (isCharging) Charge();
		if (isAttacking) _birdAtk.AttackTarget();
		if (!isCharging && !isAttacking) Idle();
	}

	public void Charge()
	{
		speed = _enemy.speed;
		anim.SetTrigger("Walk");
		moveSpeed = speed * Time.deltaTime;
		transform.Translate(facingLeft ? -moveSpeed : moveSpeed, 0, 0);
	}

	public void Idle()
	{
		anim.SetTrigger("Idle");
		speed = 0;
		moveSpeed = 0;
	}

	private void RangeCheck()
	{
		distance = transform.position.x - target.transform.position.x;

		isAttacking = distance > 0 && distance < attackRange;
		isCharging = distance > 0 && distance < chargeRange || distance < -attackRange && distance > -chargeRange;
		isIdle = !isAttacking && !isCharging;


		if (isAttacking) isCharging = false;
	}
}
