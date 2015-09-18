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
	
	public Transform bestSpot; // Default position of the ship, if there are no lasers nearby
	public float speed;
	public float safetyDistance; // Defines if the ship should run from the nearest laser or not
	public EnemyBoundary boundary;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		nearestLaser = GetClosestLaser();

		if (nearestLaser == null || Vector3.Distance(transform.position, nearestLaser.transform.position) > safetyDistance) 
			transform.position = Vector3.MoveTowards(transform.position, bestSpot.position, speed * Time.deltaTime); // Move to best spot
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, nearestLaser.transform.position, -speed * Time.deltaTime); // Run away from the laser
		}

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
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