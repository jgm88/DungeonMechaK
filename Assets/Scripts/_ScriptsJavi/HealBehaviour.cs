using UnityEngine;
using System.Collections;

public class HealBehaviour : MonoBehaviour {

	private PlayerBehaviour _playerBehaviour;
	private Vector3 _positionAux;
	private GameObject _gAux;
	private bool _canHeal = true;

	public int healAmount = 10;
	public int healCost = 10;
	public float healCoolDown = 3f;
	public GameObject healEffect;


	// Use this for initialization
	void Start () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	public void Heal(){

		if(_canHeal && _playerBehaviour.mana >= healCost){
			_playerBehaviour.ReceiveHeal(healAmount);
			_playerBehaviour.DeductMana(healCost);
			_positionAux = Camera.main.transform.position;
			_positionAux.y -= 0.5f;
			
			_gAux = Instantiate(healEffect,_positionAux, Camera.main.transform.rotation) as GameObject;
			_gAux.transform.parent = Camera.main.transform;
			StartCoroutine(CoDestoyParticles(_gAux));
			_canHeal = false;
			StartCoroutine(CoHealCoolDown());
		}
	}

	IEnumerator CoDestoyParticles(GameObject particles){
		yield return new WaitForSeconds(3f);
		Destroy(particles);
	}
	IEnumerator CoHealCoolDown(){
		yield return new WaitForSeconds(healCoolDown);
		_canHeal = true;
	}
}
