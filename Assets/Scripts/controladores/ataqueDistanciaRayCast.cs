using UnityEngine;
using System.Collections;

//requiere del componente controladorAnimciones
[RequireComponent (typeof (controladorAnimaciones))]
public class ataqueDistanciaRayCast : MonoBehaviour {
	
	private string lookedObject = "nada";
	public int weaponDamage = 30;
	private bool espera = false;
	private Color colorOriginal;
	//public Rigidbody bullet;
	private Vector3 cameraPosition;
	private Transform cameraDirection;
	private GameObject target;
	private RaycastHit hit;
	private controladorAnimaciones controladorAnimaciones;
	public AudioClip sonidoDisparo;
	private manejadorAudioAnimado controladorAudio;
	//private float tiempoStartAnimacion;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		disparar();
	}
	
	//función publica para disparar
	public void disparar(){
		if(Camera.main != null){
			cameraPosition = Camera.main.transform.position;
			cameraDirection = Camera.main.transform;
		}
		
		if(Input.GetButtonDown("Fire1") && !espera){
			espera = true;
			//recojo el objeto impactado y su tag
			objetoImpactado();
			StartCoroutine(COdisparar());
		}
	}
	
	//coroutina usada durante el disparo para controlar tiempos
	IEnumerator COdisparar(){
		//recojo su controlador de audio
		controladorAnimaciones = GetComponent<controladorAnimaciones>();
		controladorAnimaciones.playAnimationClip();
		audio.PlayOneShot(sonidoDisparo);
		//tiempoStartAnimacion = controladorAnimaciones.getTiempoStartAnimacion();
		
		//envio los mensjes
		if(lookedObject == "Enemy"){
			target.SendMessage("reproducirImpacto", SendMessageOptions.DontRequireReceiver);
			target.SendMessage("restarVida", weaponDamage, SendMessageOptions.DontRequireReceiver);
		}
		
		
		
		//me guardo su color original para que se pongan rojos en el impacto (opcional)
		//colorOriginal = target.renderer.material.color;
		
		//compruebo si es null por si justo acabo de destruirlo
		//if(GameObject.FindWithTag(lookedObject) != null)
			//target.renderer.material.color = Color.red;
		yield return new WaitForSeconds(controladorAnimaciones.getTiempoAnimationClip());
		espera = false;
	}
	
	
	//funcion que recoge el obeto impactado (si hay impacto)
	private void objetoImpactado(){
		if (Physics.Raycast (cameraPosition, cameraDirection.forward, out hit, 10000.0f)) {
            lookedObject = hit.collider.tag;
			target = GameObject.FindWithTag(lookedObject);
			//Debug.Log(target);
        }
	}
}
