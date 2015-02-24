using UnityEngine;
using System.Collections;

public class StartEndGame : MonoBehaviour
{

	public GameObject PathEndGame;

	// Use this for initialization
	void Start ()
	{

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
	}
}
