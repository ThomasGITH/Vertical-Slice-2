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

	// Flamethrower.
	[SerializeField] private bool firing;
	private bool canFire;
	public static bool flames = false;

	// Take damage.
	public GameObject screenFlash;


	void Start()
    {

		baseHealth = 35;
		baseFire = 100;

		maxHealth = baseHealth;
		maxFire = baseFire;

		currentHealth = maxHealth;
		currentFire = maxFire;

		// Subscribe to Flamethrower event
		PlayerController.evt_flames += FlamethrowerCost;

    }

    void Update()
    {
		UpdateHealth(); // Update health value & UI.
		UpdateFire(); // Update fire value & UI.
		CanFire(); // Checks if the player can use flamethrower.

		if (Input.GetKeyDown(KeyCode.H))
			TakeDamage(5);
	}

	private void UpdateHealth()
	{
		healthBar.value = Mathf.Lerp(healthBar.value, CalculateHealth(), lerpSpeed * Time.deltaTime);
		healthText.text = currentHealth.ToString();
	}

	private void UpdateFire() // Also runs CanFire.
	{
		fireBar.value = Mathf.Lerp(fireBar.value, CalculateFire(), lerpSpeed * Time.deltaTime);
		firing = Input.GetKey(KeyCode.LeftShift) ? true : false;
	}

	private void CanFire() // Checks if player can fire.
	{
		canFire = currentFire > 0 ? true : false;
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
		screenFlash.GetComponent<Animator>().SetTrigger("Flash");
	}

	private void Flamethrower(float fireDuration)
	{
		currentFire -= fireDuration;
	}

	private void FlamethrowerCost()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			Flamethrower(25f * Time.deltaTime);
		}
		else
		{
			currentFire += fireRegen * Time.deltaTime;
		}
	}
}
