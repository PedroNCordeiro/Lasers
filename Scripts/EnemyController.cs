using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyBoundary
{
	public float xMin, xMax, zMin, zMax;
}

public class EnemyController : MonoBehaviour {

	// private Rigidbody rb;

	public Transform bestSpot;
	public float speed;

	void Start ()
	{
		// rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		transform.position = Vector3.MoveTowards(transform.position, bestSpot.position, speed * Time.deltaTime);
	}

}