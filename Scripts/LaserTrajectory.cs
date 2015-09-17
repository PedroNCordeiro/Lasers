using UnityEngine;
using System.Collections;

public class LaserTrajectory : MonoBehaviour {

	private Rigidbody rb;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
		rb.velocity = transform.forward * speed;
	}

	void OnTriggerEnter(Collider other){

		Debug.Log("Old rotation: " + transform.rotation.eulerAngles);

		Vector3 oldRotation = transform.rotation.eulerAngles;
		transform.Rotate (0, -(2*oldRotation.y), 0);	// Laser collision on side walls
		rb.velocity = transform.forward * speed;
		
		Debug.Log("New rotation: " + transform.rotation.eulerAngles);
	}
}