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
	private ChargeBehaviour _chargeBehaviour;


	// Use this for initialization
	void Start () {
	
		_gunAOE = GameObject.Find("GunAOE").GetComponent<Gun>();
		_gunDinamite = GameObject.Find("GunDinamite").GetComponent<Gun>();
		GameObject gPlayerAux = GameObject.FindWithTag("Player") as GameObject;
		_playerBehaviour = gPlayerAux.GetComponent<PlayerBehaviour>();
		_healBehaviour = gPlayerAux.GetComponent<HealBehaviour>();
		_chargeBehaviour = gPlayerAux.GetComponent<ChargeBehaviour>();

		//Recogemos las imagenes para los colldowns
		_chargeBehaviour.maskSkill = GameObject.Find("MaskSkill1").GetComponent<Image>();
		_gunAOE.maskSkill = GameObject.Find("MaskSkill2").GetComponent<Image>();
		_gunDinamite.maskSkill = GameObject.Find("MaskSkill3").GetComponent<Image>();
		_healBehaviour.maskSkill = GameObject.Find("MaskSkill4").GetComponent<Image>();	

	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			if(_chargeBehaviour.canCharge){
				_chargeBehaviour.Charge();
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			if(_gunAOE.canShot && _playerBehaviour.mana >= _gunAOE.manaCost){
				_gunAOE.Shoot();
			}		
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			if(_gunDinamite.canShot && _playerBehaviour.mana >= _gunDinamite.manaCost){
				_gunDinamite.Shoot();		
			}

		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			if(_healBehaviour.canHeal && _playerBehaviour.mana >= _healBehaviour.manaCost){
				_healBehaviour.Heal();
			}

		}
	}

}
