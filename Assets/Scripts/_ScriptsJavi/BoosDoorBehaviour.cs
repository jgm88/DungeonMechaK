using UnityEngine;
using System.Collections;

public class BoosDoorBehaviour : MonoBehaviour {

	public int wicksAdquired = 4;

	private GameObject _doorRight3D;
	private GameObject _doorLeft3D;

	// Use this for initialization
	void Start () {
	//TODO foreach que recoga las flamas children y las ponga a setactive false
		_doorRight3D = GameObject.Find("DoorRight");
		_doorLeft3D = GameObject.Find("DoorLeft");
	}

	//TODO hacer funcion que reciba una string con color de llama y active un hijo u otro
	public void setDoor(bool openClose){
		_doorRight3D.SetActive(openClose);
		_doorLeft3D.SetActive(openClose);
		transform.collider.enabled = openClose;
	}

	public void purchaseWick(){
		if(wicksAdquired > 0){
			//Encender llama en UI
			--wicksAdquired;
		}
		else{
			setDoor(false);
		}
	}
}
