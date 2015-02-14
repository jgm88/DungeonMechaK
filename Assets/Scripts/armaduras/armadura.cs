using UnityEngine;
using System.Collections.Generic;

public class armadura : MonoBehaviour {
	
	//los atributos de las armaduras
	public float indiceArmadura;	//el indice va de 0 a 100
	public float durabilidad;
	public float indiceEsquive;
	private string tipo;
	
	//Use this for initialization
	
	void Start () {
	}
	
	//Update is called once per frame
	void Update () {

	}
	
	
	//getters y setters
	public float getIndiceArmadura(){
		return indiceArmadura;
	}
	
	public float getDurabilidad(){
		return durabilidad;
	}
	
	public float getIndiceEsquive(){
		return indiceEsquive;
	}
	
	public string getTigo(){
		return tipo;
	}
	
	public void setTipo(string tipo){
		this.tipo = tipo;
	}
	
	public string getTipo(){
		return tipo;
	}
}

