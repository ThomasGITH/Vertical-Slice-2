using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerCollider : MonoBehaviour
{
	private BoxCollider2D aoeBox;
	private float damage = 20f;

    void Start()
    {
		aoeBox = GetComponent<BoxCollider2D>();
    }

	private void OnTriggerStay2D(Collider2D other)
	{	
		if (other != null)
		{
			if (other.tag == "Enemy") other.GetComponent<Enemy>().TakeDamage(damage);
		}
	}
}
