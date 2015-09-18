using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	private AudioSource source;

	public GameObject playerExplosion;
	public GameObject laserExplosion;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Walls1" || other.tag == "Walls2")
			return;

		if (other.tag == "Player")
		{
			Instantiate (playerExplosion, transform.position, transform.rotation);
			Destroy(other.gameObject);
		}
		if (other.tag == "Laser") 
		{
			Instantiate (laserExplosion, transform.position, transform.rotation);
			source.Play ();
		}
		Destroy(gameObject);
	}

}