﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Events;
using System.Collections;

public class KeyHandlerHUDBehaviour : MonoBehaviour {

//	private Button _b1;
	private Gun _gunAOE;
	private Gun _gunDinamite;
	//TODO cambiar por skill intermedia que genere particulas, cooldown y cosicas
	private PlayerBehaviour _p;



	// Use this for initialization
	void Start () {
	
//		_b1 = transform.Find("SkillButton1").GetComponent<Button>();
		_gunAOE = GameObject.Find("GunAOE").GetComponent<Gun>();
		_gunDinamite = GameObject.Find("GunDinamite").GetComponent<Gun>();
		_p = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();

	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			_gunAOE.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			_gunDinamite.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			_p.ReceiveHeal(10);
		}
	}
}
