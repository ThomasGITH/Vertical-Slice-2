using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	// Sliders.
	public Slider healthBar, fireBar;
	private float fireRegen = 3f;
	private float lerpSpeed = 3f;

	// Stats.
	[HideInInspector]
	public float baseHealth, currentHealth, maxHealth, baseFire, currentFire, maxFire;

	// Components.
	private PlayerController _pc;

	// Text components.
	public Text levelText, healthText, fireText;

	// Flamethrower.
	[SerializeField] private bool firing;
	private bool canFire;
	public static bool flames = false;

	// Take damage.
	public GameObject screenFlash;
	public Camera cam;
	public static bool isDead;


	void Start()
    {
		// Components.
		_pc = GetComponent<PlayerController>();

		baseHealth = 35;
		baseFire = 100;

		maxHealth = baseHealth;
		maxFire = baseFire;

		currentHealth = maxHealth;
		currentFire = maxFire;


		// Subscribe to events.
		PlayerController.evt_flames += FlamethrowerCost;
    }

    void Update()
    {
		UpdateHealth(); // Update health value & UI.
		UpdateFire(); // Update fire value & UI.
		CanFire(); // Checks if the player can use flamethrower.

		if (currentFire < 100 && !firing) currentFire += fireRegen * Time.deltaTime; // Flamethrower regeneration.

		// Developer cheat code [Self kill]
		if (Input.GetKeyDown(KeyCode.H)) InstaKill();

		// Die conditions
		if (currentHealth <= 0) Death();
		if (transform.position.y < -15) InstaKill();
	}

	private void UpdateHealth()
	{
		healthBar.value = Mathf.Lerp(healthBar.value, CalculateHealth(), lerpSpeed * Time.deltaTime);
		healthText.text = currentHealth.ToString(); // Not visable in final build.
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

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		screenFlash.GetComponent<Animator>().SetTrigger("Flash");
		cam.GetComponent<Animator>().SetTrigger("Shake");
	}

	private void Flamethrower(float fireDuration)
	{
		if (currentFire > 0)
		{
			currentFire -= fireDuration;
		}
	}

	private void FlamethrowerCost()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			Flamethrower(25f * Time.deltaTime);
		}
	}

	private void Death()
	{
		// If player dies..
		GetComponent<PlayerHover>().enabled = false;
		isDead = true;
		_pc.anim.SetTrigger("Death");
		Invoke("DeathScreen", 3f);
	}

	private void DeathScreen()
	{
		// SceneSwitch, switchTo("GameOver");
		isDead = false;
		SceneManager.LoadScene("GameOver");
	}

	private void InstaKill() { DeathScreen(); }
}
