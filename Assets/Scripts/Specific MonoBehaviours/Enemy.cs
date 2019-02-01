using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float health, damage, speed;

	private PlayerStats _stats;
	private Patroille_walking _pw;
	private Animator anim;

	private void Start()
	{
		// Stats.
		health = 30;
		damage = 6;
		speed = 1;

		// Components.
		_stats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
		_pw = FindObjectOfType<Patroille_walking>();
		anim = GetComponentInChildren<Animator>();

		_pw.speed = speed;
		_pw.moveSpeed = speed;
	}

	private void Update()
	{
		if (health <= 0) Death();
	}

	public void DealDamage(float damage)
	{
		_stats.currentHealth -= damage;
	}

	public void TakeDamage(float dmg)
	{
		health -= dmg * Time.deltaTime;
	}

	private void Death()
	{
		speed = 0;
		anim.SetTrigger("Death");
		Destroy(gameObject, 3f);
	}
}
