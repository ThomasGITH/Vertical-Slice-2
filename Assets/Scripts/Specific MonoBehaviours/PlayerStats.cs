using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	// Sliders.
	public Slider healthBar, fireBar;
	private float fireRegen = 3f;
	private float lerpSpeed = 3f;

	// Stats.
	[HideInInspector]
	public float baseHealth, currentHealth, maxHealth, baseFire, currentFire, maxFire;

	// Text components.
	public Text levelText, healthText, fireText;


    void Start()
    {

		baseHealth = 35;
		baseFire = 100;

		maxHealth = baseHealth;
		maxFire = baseFire;

		currentHealth = maxHealth;
		currentFire = maxFire;
    }

    void Update()
    {
		UpdateHealth();
		UpdateFire();

		if (Input.GetKeyDown(KeyCode.H))
			TakeDamage(5);

		if (Input.GetKey(KeyCode.LeftShift))
			Flamethrower(25f * Time.deltaTime);

		if (currentFire < maxFire)
			currentFire += fireRegen * Time.deltaTime;
    }

	private void UpdateHealth()
	{
		healthBar.value = Mathf.Lerp(healthBar.value, CalculateHealth(), lerpSpeed * Time.deltaTime);
		healthText.text = currentHealth.ToString();
	}

	private void UpdateFire()
	{
		fireBar.value = Mathf.Lerp(fireBar.value, CalculateFire(), lerpSpeed * Time.deltaTime);
	}
	
	float CalculateFire()
	{
		return currentFire / maxFire;
	}

	float CalculateHealth()
	{
		return currentHealth / maxHealth;
	}


	private void TakeDamage(float damage)
	{
		currentHealth -= damage;
	}

	private void Flamethrower(float fireDuration)
	{
		currentFire -= fireDuration;
	}
}
