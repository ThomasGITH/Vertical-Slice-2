using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : MonoBehaviour
{
	private Patroille_walking _pw;
	private PlayerStats _stats;
	private Enemy _enemy;
	private Animator anim;

	private void Start()
	{
		_pw = FindObjectOfType<Patroille_walking>();
		_stats = FindObjectOfType<PlayerStats>();
		_enemy = FindObjectOfType<Enemy>();
		anim = GameObject.Find("Enemy_Bear").GetComponentInChildren<Animator>();
	}
	public void DealDamage()
	{
		_stats.TakeDamage(_enemy.damage);
		_pw.attackCooldownStart = _pw.attackCooldown;
	}
}
