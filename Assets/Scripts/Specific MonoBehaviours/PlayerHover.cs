using UnityEngine;
using System.Collections;

public class PlayerHover : MonoBehaviour
{
	private float hoverHeight = 3.3f; // Desired hover height.
	private float hoverGap = 1.5f; // Less height for grounded.
	private float hoverForce = 9f;
	private float damping = 0.3f; // Smoothing.

	private PlayerController _pc;


	void Start()
	{
		_pc = FindObjectOfType<PlayerController>();
	}

	private void Update()
	{
		Grounded();
	}

	void FixedUpdate()
	{
		// Raycast below player
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, hoverHeight, _pc.whatIsGround);
		Debug.DrawRay(transform.position, -Vector2.up, Color.blue, 1f);

		// If it hits something...
		if (hit.collider != null)
		{

			// Calculate distance between player and surface.
			float distance = Mathf.Abs(hit.point.y - transform.position.y);
			float heightError = hoverHeight - distance;

			// Calculate damping.
			float force = hoverForce * heightError - _pc.rb.velocity.y * damping;

			// Apply force to the player.
			_pc.rb.AddForce(Vector3.up * force);
		}
	}

	private void Grounded()
	{
		RaycastHit2D groundRay = Physics2D.Raycast(transform.position, -Vector2.up, hoverHeight, _pc.whatIsGround);
		_pc.grounded = Physics2D.Raycast(transform.position, -Vector2.up, (hoverHeight / 2), _pc.whatIsGround);
	}
}