using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttackDamage : MonoBehaviour
{
	private PlayerStats _stats;
	private Enemy _enemy;
	private Bird _bird;

    void Start()
    {
		_stats = FindObjectOfType<PlayerStats>();
		_enemy = FindObjectOfType<Enemy>();
		_bird = FindObjectOfType<Bird>();
    }

	public void DealDamage()
	{
		_stats.TakeDamage(_enemy.damage);
		_bird.attackCooldownStart = _bird.attackCooldown;
	}
}
