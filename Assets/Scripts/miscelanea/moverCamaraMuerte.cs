using UnityEngine;
using System.Collections;

public class moverCamaraMuerte : MonoBehaviour {
	
	
	public GameObject camaraPrincipal;
	public GameObject camaraWeapons;
//	public GameObject guitextMuerte;

	private GameObject player;
	private Material noSkybox;
	private GameObject UIDie;
//	private controladorGuitexts controladorGuitexts;
	
	// Use this for initialization
	void Start () {

		UIDie = GameObject.Find("Diepanel");
		player = transform.parent.gameObject;
		UIDie.SetActive(false);
	}

	//funcion que mueve la camara muerte al morir
	public void moverCamara(){

		camaraWeapons.GetComponent<Camera>().enabled = false;

		
		//desactivo control del pj
		player.GetComponent<MouseLook>().enabled = false;
		player.GetComponent<CharacterController>().enabled = false;
		player.GetComponent<AttackPlayerBehaviour>().enabled = false;
		player.GetComponent<CharacterMotor>().enabled = false;
		GameObject.Find("EventSystem").GetComponent<PauseBehaviour>().ShowCursor(true);
		//eventos itween
		iTweenEvent.GetEvent(this.gameObject, "moverCamaraMuerte").Play();
		iTweenEvent.GetEvent(this.gameObject, "rotarCamaraMuerte").Play();

		desactiveEnemies();
		UIDie.SetActive(true);

		//cambio los settings de la camara y el render para dar aspecto de muerte
		
	}

	
	private void desactiveEnemies()
	{
		// Recogemos todos los enemigos y el boss
		GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject boss = GameObject.FindGameObjectWithTag("Boss");

		// Desactivo los componentes para que se esten quietos
		GameObject.Find("EnemySpawns").SetActive(false);

		if(boss){
			boss.GetComponent<AIPath>().enabled = false;
			boss.GetComponent<SphereCollider>().enabled = false;
			boss.GetComponent<BossBehaviour>().inCombat = true;
			boss.GetComponent<BossBehaviour>().isAttackCD = true;
		}
		foreach(GameObject enemy in enemies)
		{
			enemy.GetComponent<AIPath>().enabled = false;
			enemy.GetComponent<SphereCollider>().enabled = false;
			enemy.GetComponent<EnemyBehaviour>().inCombat = true;
			enemy.GetComponent<EnemyBehaviour>().inCombat = true;
		}


	}



}
