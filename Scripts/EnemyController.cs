using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyBoundary
{
	public float xMin, xMax, zMin, zMax;
}

public class EnemyController : MonoBehaviour {

	private Rigidbody rb;
	private GameObject nearestLaser;
	private float nextFire;
	private float nearestLaserDist; // Distance to the nearest laser

	public Transform bestSpot; // Default position of the ship, if there are no lasers nearby
	public float speed;
	public float safetyDistance; // Defines if the ship should run from the nearest laser or not
	public EnemyBoundary boundary;
	public Transform humanPlayer;
	public float fireRate;
	public GameObject laser;
	public Transform laserSpawn;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		nearestLaserDist = Mathf.Infinity;
	}

	void Update ()
	{		
		float step = speed * Time.deltaTime;

		// Rotate to the Human Player Ship
		if (humanPlayer != null)
		{
			Vector3 targetDir = humanPlayer.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}

		// Find nearest laser shot
		nearestLaser = GetClosestLaser();
		if (nearestLaser != null)
			nearestLaserDist = Vector3.Distance(transform.position, nearestLaser.transform.position);
		
		// Move to best spot if no lasers nearby
		if (nearestLaser == null || nearestLaserDist > safetyDistance + 1) 
			transform.position = Vector3.MoveTowards(transform.position, bestSpot.position, step/3); 
		else
		// Run away from nearest laser
		{
			if (nearestLaserDist < safetyDistance - 1)
			{
				Vector3 dodgeLaser = Vector3.MoveTowards(transform.position, nearestLaser.transform.position, -step); // Run away from the laser
				dodgeLaser += (Vector3.one * Time.deltaTime); // Add some noise (so it doesn't seem so robotic)
				transform.position = dodgeLaser;
			}
		}

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		// //Fire shots whenever possible
		// if (Time.time > nextFire)
		// {
		// 	nextFire = Time.time + fireRate;
		// 	// Instantiate shot
		// 	Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
		// }

	}

	GameObject GetClosestLaser()
	{
		GameObject[] lasers;
		lasers = GameObject.FindGameObjectsWithTag("Laser");

		nearestLaser = null;
		foreach(GameObject laser in lasers)
		{
			if (nearestLaser == null)
				nearestLaser = laser;
			if (Vector3.Distance(transform.position, laser.transform.position) < Vector3.Distance(transform.position, nearestLaser.transform.position))
				nearestLaser = laser;
		}

		return nearestLaser;
	}
	
}