﻿using UnityEngine;
using System.Collections;

public class CloseDoorAreaBehaviour : MonoBehaviour {

	private GameObject _boss;
	private BoosDoorBehaviour _bossDoorBehaviour;
//	private EnemySpawnerBehaviour _enemySpawnerBehaviour;

	public AudioClip music;
	

	// Use this for initialization
	void Start () {

		_boss = GameObject.FindWithTag("Boss");
		_bossDoorBehaviour = GameObject.Find("BoosDoor").GetComponent<BoosDoorBehaviour>();

	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			RenderSettings.fogDensity = 0.05f;

			//TODO Usar Getcomponent
//			spawner.SendMessage("destriurEnemigosFueraBoss", SendMessageOptions.DontRequireReceiver);

			_boss.SetActive(true);
			_bossDoorBehaviour.setDoor(true);

			//TODO Cambiar por algo que no este en la torch...
//			torch.GetComponent<ligthLife>().enBossOn();
			
			//mando ademas un mensaje al spawner para que spawneee en el boss
//			spawner.SendMessage("setInBoss",SendMessageOptions.DontRequireReceiver);
			//TODO Cambiar por esto
			//			_enemySpawnerBehaviour.setInBoss();
			Camera.main.audio.Stop();
			Camera.main.audio.clip = music;
			Camera.main.audio.Play();
			Destroy(this.gameObject);
		}
	}
}
