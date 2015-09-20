using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float waitAfterKill;


	// Destroy lasers and reset ships position
	public void Restart()
	{
		StartCoroutine (WaitAfterKill());
	}
	
	IEnumerator WaitAfterKill()
	{			
		print(Time.time);
		yield return new WaitForSeconds(2);
		Application.LoadLevel(Application.loadedLevel);
		print(Time.time);
	}

}
