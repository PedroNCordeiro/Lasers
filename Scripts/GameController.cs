using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject[] lasers;

	public float waitAfterKill;

	// Destroy lasers and reset ships position
	public void Restart()
	{
		lasers = GameObject.FindGameObjectsWithTag("Laser");
		foreach(GameObject laser in lasers)
			Destroy(laser);

		StartCoroutine (WaitAfterKill());
	}
	
	IEnumerator WaitAfterKill()
	{			
		yield return new WaitForSeconds(2);
		Application.LoadLevel(Application.loadedLevel);
	}

}