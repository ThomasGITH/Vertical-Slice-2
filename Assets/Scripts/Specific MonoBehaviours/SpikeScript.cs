using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
	private PlayerStats _stats;
	private Patroille_walking _pw;
	private Enemy _enemy;

	private float spikeDamage = 5f;

	private void Start()
	{
		_stats = FindObjectOfType<PlayerStats>();
		_pw = FindObjectOfType<Patroille_walking>();
		_enemy = FindObjectOfType<Enemy>();
	}

	public void DealDamage()
	{
		_stats.TakeDamage(spikeDamage);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			DealDamage();
	}
}
