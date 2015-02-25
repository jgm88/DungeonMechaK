﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBehaviour : MonoBehaviour
{

	public Material bossSkinMat;
	/// <summary>
	/// The death particles.
	/// </summary>
	public GameObject deathParticles;
	public GameObject bossFinalExplosion;
	/// <summary>
	/// The initial damage.
	/// </summary>
	public int initialDamage = 20;
	/// <summary>
	/// The particles.
	/// </summary>
	public ParticleSystem particles;
	/// <summary>
	/// The final damage.
	/// </summary>
	public int finalDamage = 25;
	/// <summary>
	/// The final life.
	/// </summary>
	public int finalLife = 500;
	/// <summary>
	/// The initial life.
	/// </summary>
	public int initialLife = 200;
	/// <summary>
	/// Enemy is receiving damage.
	/// </summary>
	public bool receiveDamage;
	/// <summary>
	/// Enemy is moving
	/// </summary>
	public bool isMoving;
	/// <summary>
	/// The life of enemy.
	/// </summary>
	public int life;
	/// <summary>
	/// Attack is in cooldown.
	/// </summary>
	public bool isAttackCD = true;
	/// <summary>
	/// State is in combat.
	/// </summary>
	public bool inCombat = false;
	/// <summary>
	/// is the boss stunde?
	/// </summary>
	public bool isStunned = false;
	/// <summary>
	/// The speed movement.
	/// </summary>
	public float speedMovement = 2.5f;
	/// <summary>
	/// The attack Cooldown.
	/// </summary>
	public float attackCD = 3f;
	/// <summary>
	/// The impact force.
	/// </summary>
	public float impactForce = 20f;
	/// <summary>
	/// The stun time.
	/// </summary>
	public float stunTime = 4f;
	/// <summary>
	/// The original color.
	/// </summary>
	private Color originalColor;
	/// <summary>
	/// The number states.
	/// </summary>
	private int numStates = 4;
	/// <summary>
	/// The current state.
	/// </summary>
	private int currentState = 0;
	/// <summary>
	/// The controlador ataque pj.
	/// </summary>
	private AttackPlayerBehaviour playerAttackController;
	/// <summary>
	/// The current damage.
	/// </summary>
	private int currentDamage;
	/// <summary>
	/// The color states.
	/// </summary>
	private List<Color> colorStates = new List<Color> ();
	/// <summary>
	/// The powers list.
	/// </summary>
	private List<string> powersList = new List<string> ();
	/// <summary>
	/// The weaknes power of this boss.
	/// </summary>
	public string weaknesPower;
	/// <summary>
	/// The stun sprite.
	/// </summary>
	public GameObject stunSprite;
	/// <summary>
	/// The player.
	/// </summary>
	private GameObject player;
	/// <summary>
	/// The guitext victoria.
	/// </summary>
	private GameObject guitextVictoria;

	/// <summary>
	/// The ai path controller script
	/// </summary>
	private AIPath aiPath;

	private BossAnimationController _animationController;
	private bool _isDying = false;

	private manejadorAudioAnimado _auidioController;

	private GameObject impactPoint;

	private float distance;

//	private manejadorAudioAnimado soundManajer;
	// Use this for initialization
	void Awake ()
	{

//		soundManajer = GetComponent<manejadorAudioAnimado>();
		bossSkinMat.color = Color.white;
		aiPath = GetComponent<AIPath> ();
		currentDamage = initialDamage; 
		life = initialLife;
		agregarColoresYPoderes ();
		weaknesPower = powersList [currentState];
		_auidioController = GetComponent<manejadorAudioAnimado> ();
		
	}

	void Start ()
	{
//		stunSprite = GameObject.Find("Stun").gameObject;
//		stunSprite.SetActive(false);
		guitextVictoria = GameObject.Find ("victoria");
		player = GameObject.FindWithTag ("Player");
		playerAttackController = player.GetComponent<AttackPlayerBehaviour> ();
		impactPoint = GameObject.Find("MazeImpactPoint");
		_animationController = GetComponent<BossAnimationController> ();
	}
	
	//	// Update is called once per frame
	void Update ()
	{
		if(_isDying)
			_animationController.PlayDeath();
		else if(isMoving)
			_animationController.PlayRun();

//			if(inCombat)
//			{
//				distance = Vector3.Distance(player.transform.localPosition,impactPoint.transform.position;
//			}
			
	}

	//agrega los colores de cada fase
	private void agregarColoresYPoderes ()
	{
		colorStates.Add (originalColor);
		colorStates.Add (Color.green);
		colorStates.Add (Color.red);
		colorStates.Add (Color.blue);
		colorStates.Add (Color.white);
		powersList.Add ("original");
		powersList.Add ("green");
		powersList.Add ("red");
		powersList.Add ("blue");
	}

	public void ReceiveDamage (int damage)
	{

		if (damage > 100) {
			stun ();
		} else if (!receiveDamage && weaknesPower == playerAttackController.actualPower) { 
			muerteBoss ();

			life -= damage;
			if (life > 0) {
				_animationController.PlayHit();
				_auidioController.reproducirGolpeado ();
				receiveDamage = true;
				StartCoroutine (COHit (1f));
			} else {
				currentState++;
				cambiarFases ();	
			}
		}
	}
	//funcion que toma acciones segun la fase a laque cambia
	private void cambiarFases ()
	{

		if (currentState <= 3) {
			weaknesPower = powersList [currentState];
			bossSkinMat.color = colorStates [currentState];
			life = initialLife;
			particles.startColor = colorStates [currentState];
			particles.Play ();
		} else if (currentState == 4) {
			weaknesPower = powersList [currentState];
			bossSkinMat.color = colorStates [currentState];
			life = finalLife;
			currentDamage = finalDamage;
		} else
			muerteBoss ();
	}

	private void stun ()
	{
		if (!_isDying) {
			_animationController.PlayStun();
			isStunned = true;
			isAttackCD = true;
			isMoving = false;

			stunSprite.SetActive (true);
			aiPath.enabled = false;
			StartCoroutine (COStunn ());
		}
	}
	
	IEnumerator COStunn ()
	{
//		_animationController.setStunned ();
		yield return new WaitForSeconds (stunTime);
		isAttackCD = false;
		isStunned = false;
		isMoving = true;
		stunSprite.SetActive (false);
		aiPath.enabled = true;
//		if (isMoving)
//			_animationController.setRunning ();
//		else
//			_animationController.setIdle ();
	}

	/// <summary>
	/// Muertes the boss.
	/// funcion que realiza las acciones necesarias al morir
	/// </summary>
	private void muerteBoss ()
	{
		_isDying = true;
		aiPath.enabled = false;
		isMoving = false;
		_auidioController.reproducirEspecial (1);

		//destruimos las armas para el tour con la camara
//		Destroy (GameObject.Find ("Armas"));
//		deathParticles.SetActive (true);
		float duration = deathParticles.transform.GetChild (0).GetComponent<ParticleSystem> ().duration;

		//desactivamos spawner
		GameObject.Find ("EnemySpawns").SetActive (false);

		//destruimos el resto de enmigos
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			Destroy (enemy);
		}

		//coroutina que controla el lanzamiento de particulas de muerte del boss
		StartCoroutine (CODestroyBoss (duration));
	}

	IEnumerator CODestroyBoss (float duration)
	{
		float finalExplosion = bossFinalExplosion.transform.GetChild (0).GetComponent<ParticleSystem> ().duration;
		float realDuration = duration - finalExplosion;
		float disableDelay = bossFinalExplosion.transform.GetChild (0).GetComponent<ParticleSystem> ().startDelay;
		//_auidioController.reproducirEspecial (1);
		yield return new WaitForSeconds (realDuration);
		bossFinalExplosion.SetActive (true);
		yield return new WaitForSeconds (disableDelay);
		_auidioController.reproducirEspecial (0);
		GameObject.Find ("cyclop_Boss").SetActive (false);
//		deathParticles.SetActive (false);
		yield return new WaitForSeconds (finalExplosion - disableDelay);
		Destroy (this.gameObject);
	}

	/// <summary>
	/// Raises the endGamePath when defeating boss and isables player things.
	/// </summary>
	void OnDestroy ()
	{
		//Enable NodesPath
		//Comprobamos si el player esta en la escena (da null reference si te mata el boss)
		if (player) {
			//Disable all components
			foreach (MonoBehaviour c in player.GetComponents<MonoBehaviour>()) {
				c.enabled = false;
			}
			StartEndGame path = player.GetComponent<StartEndGame> ();
			//volvemos a activar el path e iniciamos
			path.enabled = true;
			path.EnablePath ();
			//launch itween event
			path.StartPath ();
			//remove child nodes
			foreach (Transform t in player.transform) {
				if (t.gameObject.name != "Main Camera") {
					Destroy (t.gameObject);
				}
			}
		}
		//Disable traps for the tour
		foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap")) {
			trap.collider.enabled = false;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player" && !_isDying ) {
			_animationController.PlayIdle();
			// && vidaPlayer.isVivo()

			atacar (other.gameObject);
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {

			aiPath.enabled = false;
			inCombat = true;
			isMoving = false;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Player")) {
			if (!_isDying)
				aiPath.enabled = true;
			inCombat = false;
			isMoving = true;
		}
	}
	
	//funcion para atacar, bloquea el spam de ataques y envia mensajes
	private void atacar (GameObject player)
	{
//		player.GetComponent<PlayerBehaviour> ().ReceiveDamage (currentDamage);
		if(!isAttackCD)
		{
			_animationController.PlayAttack();
			StartCoroutine (COAtacar ());
		}
	}
	
	//coroutina que bloquea el spam de ataques al tiempo deseado
	IEnumerator COAtacar ()
	{

		yield return new WaitForSeconds(0.5f);
		distance = float.MaxValue;
//		for(int i = 0; i< 20; ++i)
//		{
//			Debug.Log("INICIO BUCLE");
//			distance = Vector3.Distance(impactPoint.transform.localPosition, player.transform.localPosition);
//			if(distance< 1f) i = 20;
//			Debug.Log("GOLPE Distancia a "+ distance);
//			yield return null;
//		}

		isAttackCD = true;
		player.GetComponent<PlayerBehaviour>().ReceiveDamage(currentDamage);
		yield return new WaitForSeconds (attackCD);
		isAttackCD = false;

	
	}
	/// <summary>
	/// Wait to finish the hit animation
	/// </summary>
	/// <returns>return the trigger </returns>
	/// <param name="time">Cooldown time </param>
	IEnumerator COHit (float time)
	{
		aiPath.enabled = false;
		isMoving = false;
		yield return new WaitForSeconds (time);
		if(!_isDying)
		{
			isMoving = true;
			aiPath.enabled = true;
		}
		receiveDamage = false;
	}


}
