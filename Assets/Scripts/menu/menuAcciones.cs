using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class menuAcciones : MonoBehaviour {
	
	public GUIText comenzar;
	public GUIText opciones;
	public GUIText salir;
	public GUIText SalirSi;
	public GUIText SalirNo;
	public GUIText volverControl;
	public GUIText adentrate;
	public GUIText unMomento;
	
	private Color original;
	
	// Use this for initialization
	void Start () {
		original = comenzar.color;
	}
	
	// Update is called once per frame
	void Update () {
		overComenzar();
		overOpciones();
		overSalir();
		overSalirSi();
		overSalirNo();
		overVolver();
		overUnMomento();
		overAdentrate();
	}
	
	//opciones al estar sobre "Comenzar"
	private void overComenzar(){
		if(comenzar.HitTest(Input.mousePosition)) {
			comenzar.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				this.gameObject.SendMessage("itweenComenzarPosition", SendMessageOptions.DontRequireReceiver);
				comenzarControles();
			}
		}
		else{
			comenzar.color = original;
		}
	}
	
	//opciones al estar sobre "Opciones"
	private void overOpciones(){
		if(opciones.HitTest(Input.mousePosition)) {
			opciones.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				this.gameObject.SendMessage("itweenOpcionesPosition", SendMessageOptions.DontRequireReceiver);
				volverControles();
			}
		}
		else{
			opciones.color = original;
		}
	}
	
	//funciones al estar sobre "Salir"
	private void overSalir(){
		if(salir.HitTest(Input.mousePosition)) {
			salir.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				this.gameObject.SendMessage("itweenSalirPosition", SendMessageOptions.DontRequireReceiver);
				confirmarSalir();
			}
		}
		else{
			salir.color = original;
		}
	}
	
	private void overSalirSi(){
		if(SalirSi.HitTest(Input.mousePosition)){
			SalirSi.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				Application.Quit();
			}
		}
		else{
			SalirSi.color = original;
		}
	}
	
	private void overSalirNo(){
		if(SalirNo.HitTest(Input.mousePosition)){
			SalirNo.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				confirmarSalir();
			}
		}
		else{
			SalirNo.color = original;
		}
	}
	
	private void overVolver(){
		if(volverControl.HitTest(Input.mousePosition)){
			volverControl.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				volverControles();
			}
		}
		else{
			volverControl.color = original;
		}
	}
	
	private void overAdentrate(){
		if(adentrate.HitTest(Input.mousePosition)){
			adentrate.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				Application.LoadLevel("LoadingDungeon1");
			}
		}
		else{
			adentrate.color = original;
		}
	}
	
	private void overUnMomento(){
		if(unMomento.HitTest(Input.mousePosition)){
			unMomento.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				comenzarControles();
			}
		}
		else{
			unMomento.color = original;
		}
	}
	
	private void confirmarSalir(){
		
		ArrayList guitexts = new ArrayList();
		//cojo todos los elementos tageados como menu
		guitexts.AddRange(GameObject.FindGameObjectsWithTag("MenuPrincipal"));
		
		foreach(GameObject obj in guitexts){
			obj.guiText.enabled = !obj.guiText.enabled;
		}
		cambiarSalir();
	}
	
	private void cambiarSalir(){
		
		ArrayList guitexts = new ArrayList();
		//cojo todos los elementos tageados como menu
		guitexts.AddRange(GameObject.FindGameObjectsWithTag("MenuSalir"));
		
		foreach(GameObject obj in guitexts){
			obj.guiText.enabled = !obj.guiText.enabled;
		}
	}
	
	private void ocultarPrincipal(){
		
		ArrayList guitexts = new ArrayList();
		//cojo todos los elementos tageados como menu
		guitexts.AddRange(GameObject.FindGameObjectsWithTag("MenuPrincipal"));
		
		foreach(GameObject obj in guitexts){
			obj.guiText.enabled = !obj.guiText.enabled;
		}
	}
	
	private void volverControles(){
		
		ocultarPrincipal();
		ArrayList guitexts = new ArrayList();
		//cojo todos los elementos tageados como menu
		guitexts.AddRange(GameObject.FindGameObjectsWithTag("Controles"));
		
		foreach(GameObject obj in guitexts){
			obj.guiText.enabled = !obj.guiText.enabled;
		}
	}
	
	private void comenzarControles(){
		
		ocultarPrincipal();
		ArrayList guitexts = new ArrayList();
		//cojo todos los elementos tageados como menu
		guitexts.AddRange(GameObject.FindGameObjectsWithTag("MenuComenzar"));
		
		foreach(GameObject obj in guitexts){
			obj.guiText.enabled = !obj.guiText.enabled;
		}
	}
	
}
