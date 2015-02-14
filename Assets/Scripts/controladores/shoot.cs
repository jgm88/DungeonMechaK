using UnityEngine;
using System;
using System.Collections.Generic;

public class shoot : MonoBehaviour {

	private string lookedObject = "nada";
	public int weaponDamage = 30;
	private float t1;
	private float t2;
	private bool espera;
	private Color colorOriginal;
	//public Rigidbody bullet;
	private Vector3 cameraPosition;
	private Transform cameraDirection;
	private GameObject target;
	public AudioClip bowFire;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		buscarObjetoEImpactar();
	}
	
	/// <summary>
	/// busca el objeto con raycast, si es enemigo hace el impacto y le baja la vida
	/// </summary>
	private void buscarObjetoEImpactar(){
		
		if(Input.GetMouseButtonDown(0)){
			//reproducir audio
			audio.clip = bowFire;
			audio.Play();
		}
		
		RaycastHit hit;
		cameraPosition = Camera.main.transform.position;
		cameraDirection = Camera.main.transform;
		//busco el objeto y guardo el tag
        if (Physics.Raycast (cameraPosition, cameraDirection.forward, out hit, 10000.0f)) {
            lookedObject = hit.collider.tag;
			target = GameObject.FindWithTag(lookedObject);
        }
		
		//si es enemigo llamo a su funcion para restarse vida
		if(lookedObject == "Enemy" && Input.GetMouseButtonDown(0) && !espera){
			
			target.SendMessage("reproducirImpacto", SendMessageOptions.DontRequireReceiver);
			target.SendMessage("restarVida", weaponDamage, SendMessageOptions.DontRequireReceiver);
			
			
			//guardo el hijo "vida" para usar los metodos que la manejan
			//GameObject child = target.transform.GetChild(0).gameObject;
			
			//me guardo la vida restante para redimensionar la vida
			//enemyLife life = target.GetComponent<enemyLife>();
			
			//guardo el color original para poder restaurarlo luego
			//colorOriginal = target.renderer.material.color;
			////preparo los argumentos para redimensionar la vida
			List <float> args = new List<float>();
			//args.Add(weaponDamage);
			//float diferencia = life.totalLife - life.life;
			//args.Add(life.life);
			//child.SendMessage("encogerVida",args, SendMessageOptions.DontRequireReceiver);
			//compruebo si es null por si justo acabo de destruirlo
			if(GameObject.FindWithTag(lookedObject) != null)
				target.renderer.material.color = Color.red;
			
			//gaurdo el instante del impacto
			t1 = Time.time;
			t2 = t1;
			espera = true;
			
			
		}
		//calculo el tiempo transcurrido desde el ultimo impacto
		if(espera){
			t2 += Time.deltaTime;
			
		}
		//si transcurre el tiempo deseado se desbloquea el semaforo y cambio el color al original
		if(espera && (t2 -t1) >= 0.5){
			espera = false;
			if(target != null)
				target.renderer.material.color = colorOriginal;
		}
	}
}
