using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float health, damage;

	public delegate void onAttack();
	public static event onAttack dealDamage; 

	private void Start()
	{
		// Stats.
		health = 30;
		damage = 5;

		// Subscribe to event.
		dealDamage += DealingDamage;
	}

	private void Update()
	{
		if (health <= 0) Death();
	}

	private void DealingDamage()
	{
		dealDamage();
	}

	public void TakeDamage(float dmg)
	{
		health -= dmg * Time.deltaTime;
	}

	private void Death()
	{
		Destroy(gameObject);
	}
}
