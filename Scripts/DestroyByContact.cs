using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	private GameController gameController;
	private AudioSource source;

	public GameObject playerExplosion;
	public GameObject laserExplosion;

	void Start()
	{
		source = GetComponent<AudioSource>();

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent <GameController>();

		if (gameController == null)
			Debug.Log("Cannot find 'GameController' script");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Walls1" || other.tag == "Walls2")
			return;

		if (other.tag == "Player") // Human player or enemy player
		{
			Instantiate (playerExplosion, transform.position, transform.rotation);
			Destroy(other.gameObject);

			if (other.transform.position.z < 0) // Enemy ship was shot
				gameController.AddScore(false);
			else // Player ship was shot
				gameController.AddScore(true);

			gameController.Restart();
		}

		if (other.tag == "Laser") 
		{
			Instantiate (laserExplosion, transform.position, transform.rotation);
			source.Play ();
		}
		Destroy(gameObject);
	}

}