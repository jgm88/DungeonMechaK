﻿using UnityEngine;
using System.Collections;

public class PauseBehaviour : MonoBehaviour {


	private MouseLook mouseLook;
	private bool inPause;
	private GameObject pauseMenu;
	private ChangeCursor changeCursor;
	private PlayerBehaviour playerBehaviour;
	private bool _endGame = false;

	public bool endGame{
		get { return _endGame;}
		set { _endGame = value;}
	}
	// Use this for initialization
	void Start () {
		mouseLook = GameObject.FindWithTag("Player").GetComponent<MouseLook>();
		playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		changeCursor = GetComponent<ChangeCursor>();
		pauseMenu = GameObject.Find("PauseMenu");

		pauseMenu.SetActive(false);
		inPause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerBehaviour.isVivo() )
		{
			if(!inPause && Input.GetKeyDown(KeyCode.Escape))
			{

				ShowCursor(true);
				mouseLook.enabled = false;
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
				inPause = true;
			}
			else if (inPause && Input.GetKeyDown(KeyCode.Escape))
			{
				QuitPause();
			}
		}
		
	}
	public void ShowCursor(bool show)
	{
		Cursor.visible = false;
		Screen.lockCursor = !show;
		changeCursor.enabled = show;
	}

	public void QuitPause()
	{
		if(!_endGame)
		{
			mouseLook.enabled = true;
		}
		ShowCursor(false);
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		inPause = false;
	}
}
