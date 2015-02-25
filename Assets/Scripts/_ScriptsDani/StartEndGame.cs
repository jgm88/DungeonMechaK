using UnityEngine;
using System.Collections;

public class StartEndGame : MonoBehaviour
{

	public GameObject PathEndGame;
	private PauseBehaviour pause;
	// Use this for initialization
	void Start ()
	{
		pause = GameObject.Find("EventSystem").GetComponent<PauseBehaviour>();
		if (PathEndGame && PathEndGame.activeSelf)
			PathEndGame.SetActive (false);


	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void EnablePath ()
	{
		PathEndGame.SetActive (true);
		pause.endGame = true;
	}
}
