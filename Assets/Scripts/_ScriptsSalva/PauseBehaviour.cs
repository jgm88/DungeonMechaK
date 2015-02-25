using UnityEngine;
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
		pauseMenu = GameObject.Find("WindowBackground");

		pauseMenu.SetActive(false);
		inPause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerBehaviour.isVivo() && !_endGame)
		{
			if(!inPause && Input.GetKeyDown(KeyCode.Escape))
			{
				ShowCursor(true);
				pauseMenu.SetActive(true);
				mouseLook.enabled = false;
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
		Screen.showCursor = false;
		Screen.lockCursor = !show;
		changeCursor.enabled = show;
	}

	public void QuitPause()
	{
		ShowCursor(false);
		pauseMenu.SetActive(false);
		mouseLook.enabled = true;
		Time.timeScale = 1;
		inPause = false;
	}
}
