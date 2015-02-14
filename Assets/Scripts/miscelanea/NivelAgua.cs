using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NivelAgua : MonoBehaviour {
	
	//es la mejor aprosimacion para la velocidad de subir el agua
	public float tiempoSubirAgua = 0.02f;
	
	private bool espera = false;
	private float nivelInicial;
	private bool bajando = false;
	
	
	// Use this for initialization
	void Start () {
		nivelInicial = gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		SubirNivelAguaPorTiempo();
		comprobarNivel();
	}
	
	public void SubirNivelAguaPorTiempo(){
		if(!bajando)
			gameObject.transform.Translate(Vector3.up * Time.deltaTime * tiempoSubirAgua);
		else 
			gameObject.transform.Translate(Vector3.down*Time.deltaTime*0.5f);
		
	}
	
	public void bajarNivelAgua(float cantidad){
	
	}
	
	public void setNivelInicial(){
			bajando = true;
	}
	
	private void comprobarNivel(){
		if(transform.position.y <= nivelInicial)
			bajando = false;
	}
	

}
