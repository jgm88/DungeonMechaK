using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class manejadorAudioAnimado : MonoBehaviour
{
	
	//sonidos de impactos contra el objeto animado
	public List<AudioClip> armadura;

	//sonidos asociados al movimiento
	public List<AudioClip>  movimiento;
	
	//sonidos asociados a las voces/sonidos emitidos por el objeto
	public List<AudioClip>  atacar;
	public List<AudioClip>  golpeado;
	public List<AudioClip>  muere;
	public List<AudioClip>  especial;
	public List<AudioClip>  respirar; //sonido para los mobs que aun no te han visto
	
	//tiempos exactos de inicio y fin del ataque de la animacion del arma
	public float tiempoStartAtacarAnimacion;
	
	//me guardo la duracion del clip de muerte reproducido para consultarlo ya que es aleatorio
	private float duracionMuerte;
	private bool atacando = false;
	
	// Use this for initialization
	void Start ()
	{
//		if (respirar.Count > 0)
//			Respiracion ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public float reproducirMovimiento ()
	{
		int indice = Random.Range (0, (movimiento.Count - 1));
		audio.PlayOneShot (movimiento [indice]);
		return movimiento[indice].length;
	}
	
	//al ser herencia recojo el componente de la armadura que sea desde la clase padre
	public void reproducirImpacto ()
	{
		int indice = Random.Range (0, (armadura.Count - 1));
		audio.PlayOneShot (armadura [indice]);
	}
	
	//reproduce el clip cuando es golpeado (gemido)
	public void reproducirGolpeado ()
	{
		int indice = Random.Range (0, (golpeado.Count - 1));
		audio.PlayOneShot (golpeado [indice]);
	}
	
	//reproduce la coroutina de atacar si no esta atacando
	public void reproducirAtacar (float start)
	{
		if (!atacando) {
			tiempoStartAtacarAnimacion = start;
			StartCoroutine (coroutineAtacar ());
		}
	}
	
	//reproduce el clip de la muerte del objeto
	public void reproducirMuerte ()
	{
		int indice = Random.Range (0, (muere.Count - 1));
		audio.PlayOneShot (muere [indice]);
		//guardo la duracion del clip reproducido
		duracionMuerte = muere [indice].length;
		
	}
	public void reproducirEspecial (int explicitIndex = -1)
	{
		int indice;
		if (explicitIndex > -1) {
			indice = explicitIndex;
		} else {
			indice = Random.Range (0, (especial.Count - 1));
		}

		audio.PlayOneShot (especial [indice]);
	}
	
	//devuelve la duracion del clip muerte para reproducirlo antes de destruir el objeto en el manejador vida
	public float getDuracionAudioMuerte ()
	{
		return duracionMuerte;
	}
	public void Respiracion ()
	{
		StartCoroutine (coroutineRespirar ());
	}
	IEnumerator coroutineRespirar ()
	{
		int indice = Random.Range (0, (respirar.Count - 1));
		audio.PlayOneShot (respirar [indice]);
		yield return new WaitForSeconds (audio.clip.length + 2f);
		yield return StartCoroutine (coroutineRespirar ());
	}
	//reproduce el clip de atacar justo cuando el arma esta atacando
	IEnumerator coroutineAtacar ()
	{
		int indice = Random.Range (0, (atacar.Count - 1));
		audio.clip = atacar [indice];
		//reproduzco el clip con retardo para que coincida con el ataque
		audio.PlayDelayed (tiempoStartAtacarAnimacion);
		atacando = true;
		//espero la duracion de la animacion del ataque para no poder spamear el ataque
		yield return new WaitForSeconds (atacar [indice].length);
		atacando = false;
	}
}
