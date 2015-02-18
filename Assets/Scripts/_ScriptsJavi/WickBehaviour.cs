using UnityEngine;
using System.Collections;

public class WickBehaviour : MonoBehaviour {

	public int manaAmount = 5;

	private PlayerBehaviour _playerBehaviour;

	void Start () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(Input.GetKeyDown(KeyCode.F)){
				_playerBehaviour.ReceiveMana(manaAmount);
			}
		}
	}
}
