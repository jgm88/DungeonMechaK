using UnityEngine;
using System.Collections;

public class controladorPosicionesCamara : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		startPosition();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void startPosition(){
		this.transform.position = GameObject.Find("puntoInicial").transform.position;
		this.transform.rotation = GameObject.Find("puntoInicial").transform.rotation;
	}
	
	public void comenzarPosition(){
		this.transform.position = GameObject.Find("puntoComenzar").transform.position;
		this.transform.rotation = GameObject.Find("puntoComenzar").transform.rotation;
	}
	
	public void itweenSalirPosition(){
		iTweenEvent.GetEvent(this.gameObject, "moverSalir").Play();
		iTweenEvent.GetEvent(this.gameObject, "rotarSalir").Play();
	}
	
	public void itweenComenzarPosition(){
		iTweenEvent.GetEvent(this.gameObject, "moverComenzar").Play();
		iTweenEvent.GetEvent(this.gameObject, "rotarComenzar").Play();
	}
	
	public void itweenOpcionesPosition(){
		iTweenEvent.GetEvent(this.gameObject, "moverOpciones").Play();
		iTweenEvent.GetEvent(this.gameObject, "rotarOpciones").Play();
	}
}
