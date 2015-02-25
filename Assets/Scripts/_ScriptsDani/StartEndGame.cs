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
		pause.endGame = true
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
