using UnityEngine;
using System.Collections;

public class LaserTrajectory : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 newDir;	// New direction

	public float speed;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();	
		rb.velocity = transform.forward * speed;

		newDir = transform.forward; // Gets the laser direction
	}

	void OnTriggerEnter(Collider other)
	{
		Vector3 oldRotation = transform.rotation.eulerAngles;
		transform.Rotate (0, -(2*oldRotation.y), 0);	// Laser collision on side walls

		if (other.tag == "Walls1") // Side walls
		{
			newDir.x = -newDir.x;
		}		
		else
		{
			if(other.tag == "Walls2") // Up/bottom walls
			{
				newDir.z = -newDir.z;
			}
		}
		rb.velocity = newDir * speed;
	}
}