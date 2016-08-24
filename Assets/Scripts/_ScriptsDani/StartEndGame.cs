using UnityEngine;
using System.Collections;

public class StartEndGame : MonoBehaviour
{

	public GameObject PathEndGame;
	public Transform firstPoint;
	public AudioClip music;
	private bool _firstTime = true;
	private PauseBehaviour pauseBeha;
	private GameObject hudUI;
	private GameObject endGameUI;
	private GameObject _UICamera;
	// Use this for initialization
	void Start ()
	{
		hudUI = GameObject.Find("HUDPanel");
		endGameUI = GameObject.Find("EndText");
		pauseBeha = GameObject.Find("EventSystem").GetComponent<PauseBehaviour>();
		if (PathEndGame && PathEndGame.activeSelf)
			PathEndGame.SetActive (false);
		endGameUI.SetActive(false);

		_UICamera = GameObject.Find("UICamera");
		
	}
	
	public void EnablePath ()
	{
		if(PathEndGame)
			PathEndGame.SetActive (true);
		if(pauseBeha)
			pauseBeha.endGame = true;
		if(endGameUI)
			endGameUI.SetActive(true);
		if(hudUI)
			hudUI.SetActive(false);
		_UICamera.SetActive(false);

		Camera.main.GetComponent<AudioSource>().Stop ();
		Camera.main.GetComponent<AudioSource>().clip = music;
		Camera.main.GetComponent<AudioSource>().Play ();
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
