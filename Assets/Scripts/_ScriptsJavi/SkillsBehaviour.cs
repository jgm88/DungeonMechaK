using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Events;
using System.Collections;

public class SkillsBehaviour : MonoBehaviour {

//	private Button _b1;
	private Gun _gunAOE;
	private Gun _gunDinamite;
	//TODO cambiar por skill intermedia que genere particulas, cooldown y cosicas
	private PlayerBehaviour _playerBehaviour;
	private Transform _transformPlayer;
	private Vector3 _positionAux;
	private GameObject _gAux;

	public bool inWickArea = false;

	public int manaAmount = 5;
	public int healAmount = 10;
	public int healCost = 10;

	public GameObject healEffect;



	// Use this for initialization
	void Start () {
	
//		_b1 = transform.Find("SkillButton1").GetComponent<Button>();
		_gunAOE = GameObject.Find("GunAOE").GetComponent<Gun>();
		_gunDinamite = GameObject.Find("GunDinamite").GetComponent<Gun>();
		_transformPlayer = GameObject.FindWithTag("Player").transform;
		_playerBehaviour = _transformPlayer.gameObject.GetComponent<PlayerBehaviour>();

	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1) && inWickArea) {
			_playerBehaviour.ReceiveMana(manaAmount);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			_gunAOE.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			_gunDinamite.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			if(_playerBehaviour.mana >= healCost){
				_playerBehaviour.ReceiveHeal(healAmount);
				_playerBehaviour.DeductMana(healCost);
				_positionAux = _transformPlayer.position;
				_positionAux.y -= 0.5f;

				_gAux = Instantiate(healEffect,_positionAux, _transformPlayer.rotation) as GameObject;
				_gAux.transform.parent = _transformPlayer;
			}
		}
	}
}
