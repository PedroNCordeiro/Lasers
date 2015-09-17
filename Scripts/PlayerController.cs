using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float rotation;
	private float nextFire;

	public float speed;
	public float tilt;
	public Boundary boundary;
	public float rotationSpeed;
	public float mouseDelta; 	// Space, in the x-axis, between the ship position and the mouse position, needed to rotate the ship
	public float rotationDelta;	// Limits in a ship rotation

	public GameObject laser;
	public Transform laserSpawn;
	public float fireRate;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rotation = 0;
	}
	
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			// Instantiate shot
			Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
		}
	}

	void FixedUpdate ()
	{
		// Receive input
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		float mouseX = Input.mousePosition.x/37.5f - 8;
		bool rotateRight = mouseX - rb.position.x  >= mouseDelta ? true : false;
		bool rotateLeft = rb.position.x - mouseX >= mouseDelta ? true : false;


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
  
		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rotation += rotateRight ? rotationSpeed : (rotateLeft ? -rotationSpeed : (rotation > 0 ? -rotationSpeed : (rotation < 0 ? rotationSpeed : 0)));
		rotation = Mathf.Clamp(rotation, -rotationDelta, rotationDelta);

		rb.rotation = Quaternion.Euler (rb.velocity.z * tilt, rotation, rb.velocity.x * -tilt);

	}
}