using UnityEngine;
using System.Collections;

public class actualizarVida : MonoBehaviour {
	
	private GameObject vida;
	private float vidaTotal;
	private float vidaRestante;
	// Use this for initialization
	void Start () {
		vida = GameObject.FindWithTag("vidaPlayer");
		vidaTotal = GameObject.FindWithTag("Player").GetComponent<manejadorVida>().vida;
		
	}
	
	// Update is called once per frame
	void Update () {
		actualizarGuitextVida();
	}
	
	//funcion que actualiza la vida del guitext
	private void actualizarGuitextVida(){
		//si la vida total del player puede cambiar descomentar esta linea
		//vida = GameObject.FindWithTag("Player").GetComponent<manejadorVida>().vida;
		vidaRestante = GameObject.FindWithTag("Player").GetComponent<manejadorVida>().vida;
		vida.guiText.text = " " + vidaRestante + "/" + vidaTotal;
		if(vidaRestante < 0)
			vida.guiText.text = " HAS MUERTO"; 
	}
}
