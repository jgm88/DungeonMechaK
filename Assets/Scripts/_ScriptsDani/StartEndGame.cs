﻿using UnityEngine;
using System.Collections;

public class StartEndGame : MonoBehaviour
{

	public GameObject PathEndGame;
	public Transform firstPoint;
	private bool _firstTime = true;
	private PauseBehaviour pauseBeha;
	private GameObject hudUI;
	private GameObject endGameUI;
	// Use this for initialization
	void Start ()
	{
		hudUI = GameObject.Find("HUDPanel");
		endGameUI = GameObject.Find("EndText");
		pauseBeha = GameObject.Find("EventSystem").GetComponent<PauseBehaviour>();
		if (PathEndGame && PathEndGame.activeSelf)
			PathEndGame.SetActive (false);
		endGameUI.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void EnablePath ()
	{
		PathEndGame.SetActive (true);
		pauseBeha.endGame = true;
		endGameUI.SetActive(true);
		hudUI.SetActive(false);
	}
	
	public void StartPath ()
	{
		if (_firstTime) {
			transform.position.Set (firstPoint.position.x, firstPoint.position.y, firstPoint.position.z);
			_firstTime = false;
			StartCoroutine (COStartPath (3.0f));
		} else {
			StartCoroutine (COStartPath ());
		}
		
		//iTweenEvent.GetEvent (gameObject, "recorridoEndGame").Play ();
	}
	
	IEnumerator COStartPath (float time = 0.0f)
	{
		yield return new WaitForSeconds (time);
		iTweenEvent.GetEvent (gameObject, "recorridoEndGame").Play ();
	}
}
