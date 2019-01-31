using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float health, damage;

	private PlayerStats _stats;
	private Animator anim;

	public delegate void onAttack();
	public static event onAttack evt_dealDamage;

	// Death event
	public delegate void onDeath();
	public static event onDeath evt_bearDeath;

	private void Start()
	{
		// Stats.
		health = 30;
		damage = 6;

		// Components.
		_stats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
		anim = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		if (health <= 0) evt_bearDeath();
	}

	public void DealDamage(float damage)
	{
		_stats.currentHealth -= damage;
	}

	public void TakeDamage(float dmg)
	{
		health -= dmg * Time.deltaTime;
	}
}
