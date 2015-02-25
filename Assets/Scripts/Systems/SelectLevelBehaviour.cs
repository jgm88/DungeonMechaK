using UnityEngine;
using System.Collections;

public class SelectLevelBehaviour : MonoBehaviour {

	public void NextLevelButton(int index)
	{
		if(Time.timeScale ==0)
			Time.timeScale = 1;
		Application.LoadLevel(index);
	}

	public void NextLevelButton(string levelName)
	{
		if(Time.timeScale ==0)
			Time.timeScale = 1;
		Application.LoadLevel(levelName);
	}

	public void exitGame()
	{
		Application.Quit ();
	}
}
