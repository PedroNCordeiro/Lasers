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
		if (other.tag == "Walls")
		{
			Vector3 oldRotation = transform.rotation.eulerAngles;
			transform.Rotate (0, -(2*oldRotation.y), 0);
			rb.velocity = transform.forward * speed;
		}
	}
}