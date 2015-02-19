using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WickBehaviour : MonoBehaviour {

	public int manaAmount = 5;

	private PlayerBehaviour _playerBehaviour;
	private Text _wickText;

	void Start () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		_wickText = GameObject.Find("WickText").GetComponent<Text>();
		_wickText.enabled = false;

		//TODO cambiar por UI nueva
//		guitextAvivar=GameObject.FindWithTag("GUITextAvivar");
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				_playerBehaviour.ReceiveMana(manaAmount);
			}
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			_wickText.enabled = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			_wickText.enabled = false;
		}
	}
}
