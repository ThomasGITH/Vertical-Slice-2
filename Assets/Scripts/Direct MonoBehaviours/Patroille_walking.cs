using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroille_walking : MonoBehaviour
{
	public float distance, speed, moveSpeed;
	public bool walking_clockwards, flip_sprite, use_rigidbody;
	private float border_left, border_right;

	// Components.
	private Rigidbody2D rb;
	public Animator anim;
	private PlayerStats _stats;
	private Enemy _enemy;

	private Vector3 theScale;
	private float x, x2;

	// Attack.
	public float attackCooldownStart, attackCooldown;


	void Start()
	{
		distance /= 2;
		border_left = transform.position.x - distance;
		border_right = transform.position.x + distance;
		moveSpeed = speed;
		_stats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
		_enemy = GetComponent<Enemy>();

		anim = GetComponentInChildren<Animator>();

		if (flip_sprite) GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;

		if (use_rigidbody)rb = GetComponent<Rigidbody2D>();

		theScale = transform.localScale;
		x = transform.localScale.x;
		x2 = x * -1;

		// Attack.
		attackCooldown = 0;

		// Death.
		Enemy.evt_bearDeath += Death;
	}

	void Update()
	{
		if (transform.position.x < border_left)
		{
			walking_clockwards = true;
		}
		else if (transform.position.x > border_right)
		{
			walking_clockwards = false;
		}

		if (use_rigidbody)
		{
			rb.velocity = new Vector2(walking_clockwards ? speed * 1 : -speed * 1, rb.velocity.y);
		}
		else
		{
			transform.Translate(walking_clockwards ? speed : -speed, 0, 0);
		}

		transform.localScale = new Vector3(walking_clockwards ? x2 : x, transform.localScale.y, transform.localScale.z);

		// Attack.
		attackCooldownStart += Time.deltaTime;
	}

	void FlipSprite()
	{
		walking_clockwards = !walking_clockwards;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") AttackTarget();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (_enemy.health > 0)
		if (other.tag == "Player") Patrol();
	}

	private void AttackTarget()
	{
		speed = 0;
		if (attackCooldownStart >= 1.5f)
		{
			anim.SetTrigger("Attack");
			attackCooldownStart = attackCooldown;
		}
	}

	private void Patrol()
	{
		speed = moveSpeed;
		anim.SetTrigger("Walk");
	}

	private void Death()
	{
		speed = 0;
		anim.SetTrigger("Death");
		Destroy(gameObject, 3f);
	}
}
