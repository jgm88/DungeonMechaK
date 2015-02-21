using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Events;
using System.Collections;

public class SkillsBehaviour : MonoBehaviour {

	//TODO Sacar logica de Heal a otro script
	private Gun _gunAOE;
	private Gun _gunDinamite;
	private PlayerBehaviour _playerBehaviour;
	private HealBehaviour _healBehaviour;

	public bool inWickArea = false;
	public int manaChargeAmount = 5;


	// Use this for initialization
	void Start () {
	
		_gunAOE = GameObject.Find("GunAOE").GetComponent<Gun>();
		_gunDinamite = GameObject.Find("GunDinamite").GetComponent<Gun>();
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		_healBehaviour = GameObject.FindWithTag("Player").GetComponent<HealBehaviour>();

	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1) && inWickArea) {
			_playerBehaviour.ReceiveMana(manaChargeAmount);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			_gunAOE.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			_gunDinamite.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			_healBehaviour.Heal();
		}
	}

}
