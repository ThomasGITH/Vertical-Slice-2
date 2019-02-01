using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack : MonoBehaviour
{
	private Bird _bird;
	private PlayerStats _stats;
	private Enemy _enemy;
	private Animator anim;

	public Collider2D chargeRange;

	private void Start()
	{
		_bird = FindObjectOfType<Bird>();
		_stats = FindObjectOfType<PlayerStats>();
		_enemy = FindObjectOfType<Enemy>();
		anim = GameObject.Find("Enemy_Bird").GetComponentInChildren<Animator>();
	}
	public void DealDamage()
	{
		_stats.TakeDamage(_enemy.damage);
		_bird.attackCooldownStart = _bird.attackCooldown;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") AttackTarget();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (_enemy.health > 0)
		{
			if (other.tag == "Player") _bird.isCharging = true;
			chargeRange.enabled = true;
		}
	}

	public void AttackTarget()
	{
		_enemy.speed = 0;
		if (_bird.attackCooldownStart >= 1.2f)
		{
			anim.SetTrigger("Attack");
			_bird.attackCooldownStart = _bird.attackCooldown;
		}
	}

}