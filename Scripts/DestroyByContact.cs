using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	private AudioSource[] sources;

	public GameObject playerExplosion;
	public GameObject laserExplosion;

	void Start()
	{
		sources = GetComponents<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Walls1" || other.tag == "Walls2")
			return;

		if (other.tag == "Player")
		{
			Instantiate (playerExplosion, transform.position, transform.rotation);
			sources[1].Play ();
			Destroy(other.gameObject);
		}
		if (other.tag == "Laser") 
		{
			Instantiate (laserExplosion, transform.position, transform.rotation);
			sources[0].Play ();
		}
		Destroy(gameObject);
	}

}