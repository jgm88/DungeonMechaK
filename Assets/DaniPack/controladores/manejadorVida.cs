using UnityEngine;
using System.Collections;

[RequireComponent (typeof (armadura))]
public class manejadorVida : MonoBehaviour {
	
	public float vida;
	public controladorEscudo escudado;
	private bool vivo = true;
	private LoadAnimIA loadAnim;
	
	// Use this for initialization
	void Start () {
		loadAnim = gameObject.GetComponent<LoadAnimIA>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	//resto vida y si es <= 0 reproduzco sonidos y destruyo el objeto "enemigo"
	public void restarVidaEnemigo(float cantidad){
		//posibilidad de escudar los enemigos??
		noEscudado(cantidad);
	}
	
	//resto vida y si es <= 0 reproduzco sonidos y destruyo el objeto "player"
	public void restarVida(float cantidad){
		//por defecto entrara siempre aki
		if(escudado == null || !escudado.escudado){
			noEscudado(cantidad);
		}
		else{
			//coger manejador audio inanimado y reproducir sonido de escudo bloqueando
		}
	}
	
	private void noEscudado(float cantidad){
		restarIndiceDefensaArmadura(cantidad);
		if(vida <= 0){
			//me hago una instancia para poder recoger la duracion del clip y destruirlo despues del sonido
			manejadorAudioAnimado manejador = this.gameObject.GetComponent<manejadorAudioAnimado>();
			
			if(gameObject.tag=="Enemy")
				loadAnim.morir();
			manejador.reproducirMuerte();
			if(this.tag != "Player")
				Destroy(this.gameObject, manejador.getDuracionAudioMuerte());
			else{
				muertePj();
				vivo=false;
			}
		}
	}
	
	
	//reduzco el daño segun la armadura, en el porcentaje que proteje la armadura
	private void restarIndiceDefensaArmadura(float cantidad){
		armadura armadura = this.gameObject.GetComponent<armadura>();
		vida -= (cantidad - cantidad*(armadura.getIndiceArmadura()/100));
	}
	
	private void muertePj(){
		if(vivo)
			GameObject.FindWithTag("MainCamera").GetComponent<moverCamaraMuerte>().moverCamara();
	}
	
	public bool isVivo(){
		return vivo;
	}
}
