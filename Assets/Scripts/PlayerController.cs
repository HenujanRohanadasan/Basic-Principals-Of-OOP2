using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float speed = 5.0f;
	private float strengthUpSpeed = 30.0f;

	private Rigidbody playerRb;

	private bool hasPowerup = false;
	public static bool hasSmashPower;

	[SerializeField] GameObject focalPoint;
	[SerializeField] GameObject rockets;

	[SerializeField] GameObject powerUpIndicator;
	[SerializeField] GameObject powerUpIndicatorRocket;
	[SerializeField] GameObject powerUpIndicatorSmash;

	[SerializeField] GameObject rocketPrefab;
	[SerializeField] MeshCollider IsladCollider;
	[SerializeField] float seconds;

	// Start is called before the first frame update
	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		PlayerMovement();

		Vector3 playerPos = transform.position;

		powerUpIndicator.transform.position = playerPos + new Vector3(0, -0.5f, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Powerup"))
		{
			hasPowerup = true;
			Destroy(other.gameObject);
			StartCoroutine(powerupCountdownRoutine());
			powerUpIndicator.gameObject.SetActive(true);
		}

		if (other.CompareTag("RocketPowerUp"))
		{
			Destroy(other.gameObject);
			powerUpIndicatorRocket.gameObject.SetActive(true);
			GravityAtack();
		}

		if (other.CompareTag("SmashPowerUp"))
		{
			Destroy(other.gameObject);
			powerUpIndicatorSmash.gameObject.SetActive(true);
			hasSmashPower = true;
			StartCoroutine(smashPowerCountdown());
		}
	}

	IEnumerator powerupCountdownRoutine()
	{
		yield return new WaitForSeconds(8);
		hasPowerup = false;
		powerUpIndicator.gameObject.SetActive(false);
	}

	IEnumerator ActivateMeshCollider()
    {
		yield return new WaitForSeconds(seconds);
		IsladCollider.enabled = true;
    }

	IEnumerator smashPowerCountdown()
    {
		yield return new WaitForSeconds(0.1f);
		hasSmashPower = false;
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
		{
			Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

			enemyRb.AddForce(awayFromPlayer * strengthUpSpeed, ForceMode.Impulse);
		}
	}

	void PlayerMovement()//Abstraction
	{
		float verticalInput = Input.GetAxis("Vertical");

		playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
	}

	void GravityAtack()//Abstraction
	{
		IsladCollider.enabled = false;
		StartCoroutine(ActivateMeshCollider());
    }
}
