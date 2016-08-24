﻿using UnityEngine;
using System.Collections;

public class AttackPlayerBehaviour : MonoBehaviour
{


	public int weaponDamage;
	private bool isAttack = false;
	private bool golpeado = false;
	private Collider weaponCollider;
	private Animator animator;
	public string actualPower = "";
	// Use this for initialization
	void Start ()
	{
		weaponCollider = GameObject.Find ("SwordPhysics").GetComponent<Collider> ();
		weaponCollider.enabled = false;
		animator = this.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		animator.SetBool ("attack", isAttack);
		if (Input.GetButtonDown ("Fire1") && !isAttack) {// para shields && !shieldController.escudado){
			atacar ();
		}
	}

	//establezco el poder del arma en el combate final
	public void setActualPower (string poder)
	{
		actualPower = poder;
	}
	
	public string getActualPower ()
	{
		return actualPower;
	}

	public void atacar ()
	{
		if (!isAttack)
			StartCoroutine (COatacar ());
	}

	//coroutinas que reproducen la animacion y activan el collider el tiempo exacto que puede golpear
	IEnumerator COatacar ()
	{
		yield return new WaitForEndOfFrame();
		isAttack = true;

		yield return new WaitForSeconds (0.2f);
		GameObject.Find("AttackSound").GetComponent<manejadorAudioAnimado>().reproducirAtacar(0.0f);
		weaponCollider.enabled = true;
		yield return StartCoroutine (colliderActivoExacto ());
	}

	//coroutina que activa el collider el tiempo exacto para cada arma
	IEnumerator colliderActivoExacto ()
	{
				
		//espero el tiempo para deshabilitar el collider
		yield return new WaitForSeconds (0.5f);
		weaponCollider.enabled = false;
		golpeado = false;
		
		//espero a que concluya la animacion para poder volver a atacar
		yield return new WaitForSeconds (animator.GetCurrentAnimatorClipInfo (0).Length - 0.9f);
		
		isAttack = false;
	}

	//recojo el evento de golpear a un enemigo
	void OnTriggerStay (Collider other)
	{
		if (other.GetComponent<Collider>().tag == "Enemy" && weaponCollider.enabled == true && !golpeado) {
			
			//falta crear un un metodo para obtener el tiempo start animacion
			//			other.SendMessage("reproducirImpacto", controladorAnimaciones.getTiempoStartAnimacion(), SendMessageOptions.DontRequireReceiver);
			//			other.SendMessage("restarVidaEnemigo", weaponDamage, SendMessageOptions.DontRequireReceiver);
			other.GetComponent<EnemyBehaviour> ().ReceiveDamage (weaponDamage);
			golpeado = true;
			//reproduzco el sprite de sangre
			//TODO MIRAR PARA HACER QUE CORRA LA SANGRE
//			bloodSprite();
			
		} else if (other.CompareTag ("Boss") && weaponCollider.enabled == true && !golpeado) {
			
			other.GetComponent<BossBehaviour> ().ReceiveDamage (weaponDamage);
		}
	}

}
