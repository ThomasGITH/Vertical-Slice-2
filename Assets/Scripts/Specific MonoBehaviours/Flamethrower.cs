using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
	public ParticleSystem[] ps_flames;
	public BoxCollider2D aoeBox;
	private bool firing;

	private float fireDamage;

    void Start()
    {
		PlayerController.evt_flames += Flames;
		PlayerController.evt_stopFlames += StopFlames;

		fireDamage = 1f;
	}

    void Update()
    {

	}

	private void Flames()
	{
		foreach (ParticleSystem flame in ps_flames) flame.Emit(2);
		firing = true;
		aoeBox.enabled = true;
	}

	private void StopFlames()
	{
		firing = false;
		aoeBox.enabled = false;
	}
}
