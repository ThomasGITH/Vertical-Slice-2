using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
	public ParticleSystem[] ps_flames;
	private PlayerStats _stats;
	public BoxCollider2D aoeBox;

    void Start()
    {
		PlayerController.evt_flames += Flames;
		PlayerController.evt_stopFlames += StopFlames;
	}

    void Update()
    {
		if (PlayerStats.isDead)
		{
			PlayerController.evt_flames -= Flames;
			PlayerController.evt_flames -= StopFlames;
		}
	}

	private void Flames()
	{
		foreach (ParticleSystem flame in ps_flames) if (flame != null) flame.Emit(2);
		if (aoeBox != null) aoeBox.enabled = true;
	}

	private void StopFlames()
	{
		if (aoeBox != null) aoeBox.enabled = false;
	}
}
