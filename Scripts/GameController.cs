using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject[] lasers;
	private int scorePlayer;
	private int scoreEnemy;

	public float waitAfterKill;
	public GUIText scoreText;

	void Start()
	{
		scorePlayer = 0;
		scoreEnemy = 0;
	}

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

	// <human> is the ship that killed the other
	public void AddScore(bool human)
	{
		if (human)
			scorePlayer ++;
		else
			scoreEnemy ++;

		UpdateScore();
	}

	void UpdateScore ()
	{
		scoreText.text = scorePlayer + " - " + scoreEnemy;
	}

}