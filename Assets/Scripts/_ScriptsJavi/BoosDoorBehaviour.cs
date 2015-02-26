using UnityEngine;
using System.Collections;

public class BoosDoorBehaviour : MonoBehaviour
{

	public int wicksAdquired = 4;
	public GameObject openDoorText;

	private GameObject _doorRight3D;
	private GameObject _doorLeft3D;

	private GameObject _flameRed;
	private GameObject _flameNormal;
	private GameObject _flameBlue;
	private GameObject _flameGreen;

	private GameObject _flameRedUI;
	private GameObject _flameNormalUI;
	private GameObject _flameBlueUI;
	private GameObject _flameGreenUI;

	void Start ()
	{
		openDoorText.SetActive(false);
		_doorRight3D = GameObject.Find ("DoorRight");
		_doorLeft3D = GameObject.Find ("DoorLeft");

		_flameRed = transform.Find ("FlameRed").gameObject;
		_flameNormal = transform.Find ("FlameNormal").gameObject;
		_flameBlue = transform.Find ("FlameBlue").gameObject;
		_flameGreen = transform.Find ("FlameGreen").gameObject;

		_flameRed.SetActive (false);
		_flameNormal.SetActive (false);
		_flameBlue.SetActive (false);
		_flameGreen.SetActive (false);

		_flameRedUI = GameObject.Find ("FlameRedUI");
		_flameNormalUI = GameObject.Find ("FlameNormalUI");
		_flameBlueUI = GameObject.Find ("FlameBlueUI");
		_flameGreenUI = GameObject.Find ("FlameGreenUI");

		_flameRedUI.SetActive (false);
		_flameNormalUI.SetActive (false);
		_flameBlueUI.SetActive (false);
		_flameGreenUI.SetActive (false);

	}

	//TODO hacer funcion que reciba una string con color de llama y active un hijo u otro
	public void setDoor (bool openClose)
	{
		_doorRight3D.SetActive (openClose);
		_doorLeft3D.SetActive (openClose);
		transform.collider.enabled = openClose;
	}

	public void purchaseWick (string color)
	{
		if (wicksAdquired > 0) {
			if (color == "red") {
				_flameRed.SetActive (true);
				_flameRedUI.SetActive (true);
			} else if (color == "normal") {
				_flameNormal.SetActive (true);
				_flameNormalUI.SetActive (true);
			} else if (color == "blue") {
				_flameBlue.SetActive (true);
				_flameBlueUI.SetActive (true);	
			} else if (color == "green") {
				_flameGreen.SetActive (true);
				_flameGreenUI.SetActive (true);
			}
			--wicksAdquired;

		}
		if (wicksAdquired == 0) {
			setDoor (false);
			StartCoroutine (COShowDoorOpened ());
		}
	}

	IEnumerator COShowDoorOpened ()
	{
		openDoorText.SetActive (true);
		yield return new WaitForSeconds (4.0f);
		openDoorText.SetActive (false);
	}
}
