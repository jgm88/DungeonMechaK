using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//requiere del componente controladorAnimciones
[RequireComponent (typeof (controladorAnimaciones))]

public class atacarCC : MonoBehaviour {
	
	public float force;
	public int weaponDamage;
	private Collider colliderObject;
	private bool golpeado = false;
	private bool atacando = false;
	private controladorAnimaciones controladorAnimaciones;
	private GameObject player;
	public GameObject sangrado;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		colliderObject = this.collider;
		colliderObject.enabled = false;
		controladorAnimaciones = GetComponent<controladorAnimaciones>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//recojo el evento de golpear a un enemigo
	void OnTriggerEnter(Collider other){
		if(other.collider.tag == "Enemy" && colliderObject.enabled == true && !golpeado){

			//falta crear un un metodo para obtener el tiempo start animacion
//			other.SendMessage("reproducirImpacto", controladorAnimaciones.getTiempoStartAnimacion(), SendMessageOptions.DontRequireReceiver);
//			other.SendMessage("restarVidaEnemigo", weaponDamage, SendMessageOptions.DontRequireReceiver);
			other.GetComponent<EnemyBehaviour>().ReceiveDamage(weaponDamage);
			golpeado = true;
			//reproduzco el sprite de sangre
			bloodSprite();

		}
		else if(other.CompareTag("Boss") && colliderObject.enabled == true && !golpeado)
		{

			other.GetComponent<BossBehaviour>().ReceiveDamage(weaponDamage);
		}
	}
	
	public void atacar(){
		if(!atacando)
			StartCoroutine(COatacar());
	}
	
	//coroutinas que reproducen la animacion y activan el collider el tiempo exacto que puede golpear
	IEnumerator COatacar(){
		atacando = true;
		float tiempoStart = controladorAnimaciones.getAnimacionActual().tiempoStartAnimacion;
		
//		player.SendMessage("reproducirAtacar", tiempoStart, SendMessageOptions.DontRequireReceiver);
		//reproduzco la animacion asociada al "animation" del struct
		controladorAnimaciones.playAnimationClip();
		
		
		yield return new WaitForSeconds(tiempoStart);
		
		colliderObject.enabled = true;
		yield return StartCoroutine( colliderActivoExacto() );
	}
	
	//coroutina que activa el collider el tiempo exacto para cada arma
	IEnumerator colliderActivoExacto(){
		float tiempoStart = controladorAnimaciones.getAnimacionActual().tiempoStartAnimacion;
		float tiempoTotal = controladorAnimaciones.getAnimacionActual().clipAnimacion.length;
		float tiempoEnd = controladorAnimaciones.getAnimacionActual().tiempoEndAnimacion;
		
		//espero el tiempo para deshabilitar el collider
		yield return new WaitForSeconds(tiempoTotal - tiempoStart - tiempoEnd);
		
		colliderObject.enabled = false;
		golpeado = false;
		
		//espero a que concluya la animacion para poder volver a atacar
		yield return new WaitForSeconds(tiempoEnd);
		
		atacando = false;
	}
	
	//funcion que instancia el sprite del sangrado
	
	private void bloodSprite(){
		List<string> nombres = new List<string>();
		int indice = Random.Range(0, 3);
		//me guardo las terminaciones de los nombres de las animaciones de sangrado
		nombres.Add("A"); nombres.Add("B");nombres.Add("C");nombres.Add("D");
		
		//cojo la posicion del punto de sangrado asociado al arma para instanciar el objeto
		Vector3 puntoImpacto = GameObject.Find("PuntoSangre").transform.position;
		
		//ajusto la posicion del sangrado ya que orthello fija el eje z a cero
		//puntoImpacto = puntoImpacto + new Vector3(0,0,-0.4f);
		
		GameObject temporal = Instantiate(sangrado, puntoImpacto, Quaternion.identity) as GameObject;
		OTAnimatingSprite ot = temporal.GetComponentInChildren<OTAnimatingSprite>();
		
		//comienza la animacion elegida random
		ot.PlayOnce("blood" + nombres[indice]);
	}
}
