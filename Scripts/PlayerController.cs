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

	public float speed;
	public float tilt;
	public Boundary boundary;
	public float rotationSpeed;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rotation = 0;
	}
	
	void FixedUpdate ()
	{
		// Receive input
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool rotateLeft = Input.GetKey ("q");
		bool rotateRight = Input.GetKey ("e");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
  
		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rotation += rotateRight ? rotationSpeed : (rotateLeft ? -rotationSpeed : 0);
		// rotation += rotationSpeed*rotateRight - rotationSpeed*rotateLeft;

		rb.rotation = Quaternion.Euler (0.0f, rotation, rb.velocity.x * -tilt);
	}
}
