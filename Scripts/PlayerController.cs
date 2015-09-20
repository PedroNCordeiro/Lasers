using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float nextFire;

	public float speed;
	public Boundary boundary;
	public float rotationSpeed;

	public GameObject laser;
	public Transform laserSpawn;
	public float fireRate;
	public float spawnTime;

	IEnumerator Start () {
		yield return new WaitForSeconds(spawnTime);

		rb = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		if (Time.time > nextFire && Input.GetButton("Fire1") && rb != null)
		{
			nextFire = Time.time + fireRate;
			// Instantiate shot
			Instantiate (laser, laserSpawn.position, laserSpawn.rotation);
		}
	}

	void FixedUpdate ()
	{
		if (rb != null)
		{
			// Receive input
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.velocity = movement * speed;
	  
			rb.position = new Vector3
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

			Vector3 mouseCoord = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
			mouseCoord.y = 0;

			Vector3 targetDir = mouseCoord - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.time, 0.0F);

			transform.rotation = Quaternion.LookRotation(newDir);

			
		}
	}
}