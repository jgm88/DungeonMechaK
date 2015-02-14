using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controladorAtaqueCC : MonoBehaviour {
	
	public List<GameObject> armas;
	public GameObject armaActiva;
	private int indice;
	public controladorEscudo shieldController;
	int numerArmas;
	private string poderActual = "";
	
	// Use this for initialization
	void Start () {
		setDefaultRenders();
	}
	
	// Update is called once per frame
	void Update () {
		atacar ();
		seleccionArma();
	}
	
	//establezco el poder del arma en el combate final
	public void setPoderActual(string poder){
		poderActual = poder;
	}
	
	public string getPoderActual(){
		return poderActual;
	}
	
	//cambio de arma por nombre del arma (deprecated)
	public void cambiarArmaActiva(string nombreArma){
		foreach(GameObject arma in armas){
			if(arma.name == nombreArma){
				armaActiva.SetActive(false);
				armaActiva = arma;
				armaActiva.SetActive(true);
			}
		}
	}
	
	//selecciona el arma anterior o siguiente con la ruleta del raton
	public void seleccionArma(){
		if (Input.GetAxis("Mouse ScrollWheel") > 0){ // forward
			//selecciono el arma siguiente y la activo y desactivo la actual
			armaActiva.SetActive(false);
			armaActiva = armas[(getIndiceArmaActual() + 1) % (armas.Count)]; 
			armaActiva.SetActive(true);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0){
			//lo mismo pero con la anterior
			armaActiva.SetActive(false);
			armaActiva = armas[Mathf.Abs((getIndiceArmaActual() - 1)) % (armas.Count)];
			armaActiva.SetActive(true);
		}
	}
	
	//devuelve el indice del array del arma actual
	private int getIndiceArmaActual(){
		int i = 0;
		foreach(GameObject arma in armas){
			if(arma.Equals(armaActiva))
				return i;
			i++;
		}
		return i;
	}
	
	//ejecuta la coroutina atacar asociada al arma seleccionada
	public void atacar(){
		if(Input.GetButtonDown("Fire1")){// para shields && !shieldController.escudado){
			atacarCC atacarCC = armaActiva.GetComponent<atacarCC>();
			atacarCC.atacar();
		}
	}
	
	private void setDefaultRenders(){
		foreach(GameObject arma in armas){
			arma.SetActive(false);
		}
		if(armaActiva == null)
			armaActiva = armas[0];
		armaActiva.SetActive(true);
	}
}
