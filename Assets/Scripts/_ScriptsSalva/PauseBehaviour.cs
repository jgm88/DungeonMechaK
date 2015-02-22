using UnityEngine;
using System.Collections;

public class PauseBehaviour : MonoBehaviour {

	private MouseLook mouseLook;
	private bool inPause;
	// Use this for initialization
	void Start () {
		mouseLook = GameObject.FindWithTag("Player").GetComponent<MouseLook>();
		inPause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!inPause && Input.GetKeyDown(KeyCode.Escape))
		{
			mouseLook.enabled = false;
			Time.timeScale = 0;
			inPause = true;
		}
		else if (inPause && Input.GetKeyDown(KeyCode.Escape))
		{
			mouseLook.enabled = true;
			Time.timeScale = 1;
			inPause = false;
		}
		
	}
}
