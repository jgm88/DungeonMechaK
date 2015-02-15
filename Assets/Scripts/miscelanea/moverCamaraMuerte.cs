using UnityEngine;
using System.Collections;

public class moverCamaraMuerte : MonoBehaviour {
	
	
	public GameObject camaraPrincipal;
	public GameObject camaraWeapons;
	public GameObject guitextMuerte;

	private GameObject player;
	private Material noSkybox;
	private controladorGuitexts controladorGuitexts;
	
	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject;
		controladorGuitexts = GameObject.FindWithTag("GuitextsContainer").GetComponent<controladorGuitexts>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//funcion que mueve la camara muerte al morir
	public void moverCamara(){

		camaraWeapons.camera.enabled = false;
		controladorGuitexts.enableMensajeMuerte();
		
		//desactivo control del pj
		player.GetComponent<MouseLook>().enabled = false;

		player.GetComponent<CharacterMotor>().canControl = false;
		
		//eventos itween
		iTweenEvent.GetEvent(this.gameObject, "moverCamaraMuerte").Play();
		iTweenEvent.GetEvent(this.gameObject, "rotarCamaraMuerte").Play();
		iTweenEvent.GetEvent(guitextMuerte, "mostrarMuerte").Play();
		
		
		
		//cambio los settings de la camara y el render para dar aspecto de muerte
		
	}

	
	

}
