using UnityEngine;
using System.Collections;

public class actualizarVida : MonoBehaviour {
	
	private GameObject vida;
	private float vidaTotal;
	private float vidaRestante;
	private GameObject player;
	// Use this for initialization
	void Start () {
		vida = GameObject.FindWithTag("vidaPlayer");
		player = GameObject.FindWithTag("Player").gameObject;
		vidaTotal = player.GetComponent<PlayerBehaviour>().life;
	}
	
	// Update is called once per frame
	void Update () {
		actualizarGuitextVida();
	}
	
	//funcion que actualiza la vida del guitext
	private void actualizarGuitextVida(){
		//si la vida total del player puede cambiar descomentar esta linea
		//vida = GameObject.FindWithTag("Player").GetComponent<manejadorVida>().vida;
		vidaRestante = player.GetComponent<PlayerBehaviour>().life;
		vida.GetComponent<GUIText>().text = " " + vidaRestante + "/" + vidaTotal;
		if(vidaRestante < 0)
			vida.GetComponent<GUIText>().text = " HAS MUERTO"; 
	}
}
