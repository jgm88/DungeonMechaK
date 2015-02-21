﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
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
	public bool isAttackCD;
	/// <summary>
	/// State is in combat.
	/// </summary>
	public bool inCombat;
	/// <summary>
	/// The attack Cooldown.
	/// </summary>
	public float attackCD = 3f;
	/// <summary>
	/// The impact force.
	/// </summary>
	public float impactForce = 20f;
	/// <summary>
	/// The attack damage.
	/// </summary>
	public int damage = 10;
	/// <summary>
	/// Internal animator state machine.
	/// </summary>
	protected Animator animator;

	private manejadorAudioAnimado soundManajer;
	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		soundManajer = GetComponent<manejadorAudioAnimado>();

	}
	
//	// Update is called once per frame
//	void Update () {
//	}

	void LateUpdate()
	{
		if(animator)
		{
			animator.SetBool("receiveDamage",receiveDamage);
			animator.SetBool("inCombat",inCombat);
			animator.SetBool("isAttackCooldown",isAttackCD);
			animator.SetInteger("life",life);
		}
	}
	public void ReceiveDamage(int damage)
	{
		if (animator && life > 0)
		{
			life -= damage;
			if (life > 0)
			{
				soundManajer.reproducirImpacto();
				receiveDamage = true;
				StartCoroutine(COHit(1.2f));
			}
			else
			{
				GetComponent<CharacterController>().enabled = false;
				GetComponent<AIPath>().enabled = false;
				collider.enabled = false;
				soundManajer.reproducirMuerte();
				Destroy(gameObject,3.5f);	
			}

		}
	}
	
	void OnTriggerStay(Collider other){
		if(!receiveDamage && !isAttackCD && other.tag == "Player" ){ // && vidaPlayer.isVivo()
			atacar (other.gameObject);
//			Vector3 dir = other.transform.position - transform.position;
//			dir.y = 0;
//			if (other.GetComponent<CharacterController>().rigidbody){
//				Debug.Log("HAGO IMPACTO CON FISICA");
//				other.GetComponent<CharacterController>().rigidbody.AddForce(dir.normalized * impactForce);
//			}
//			else
//			{
//				Debug.Log("HAGO IMPACTO");
//				other.GetComponent<ImpactReceiver>().AddImpact(dir.normalized * impactForce);
//			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			inCombat = true;
			isMoving = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			inCombat = false;
			isMoving = true;
		}
	}
	
	//funcion para atacar, bloquea el spam de ataques y envia mensajes
	private void atacar(GameObject player){
		isAttackCD = true;
		StartCoroutine(COAtacar());
		soundManajer.reproducirAtacar(0.5f);
		player.GetComponent<PlayerBehaviour>().ReceiveDamage(damage);

	}

	//coroutina que bloquea el spam de ataques al tiempo deseado
	IEnumerator COAtacar(){
		yield return new WaitForSeconds(attackCD);
		isAttackCD = false;
	}
	/// <summary>
	/// Wait to finish the hit animation
	/// </summary>
	/// <returns>return the trigger </returns>
	/// <param name="time">Cooldown time </param>
	IEnumerator COHit(float time)
	{
		yield return new WaitForSeconds(time);
		receiveDamage = false;
	}

}
