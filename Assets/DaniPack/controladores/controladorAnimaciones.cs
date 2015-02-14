using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Animation))]
public class controladorAnimaciones : MonoBehaviour {
	
	//clase que representa un "contenedor" para las animaciones y sus tiempos para activar y desactivar el collider
	[System.Serializable]
	public class estructuraAnimaciones{
		public AnimationClip clipAnimacion;
		public float tiempoStartAnimacion;
		public float tiempoEndAnimacion;
		
	}
	
	//hacer un stuct para guardar junto a cada clip su tiempo de inicio y fin de activacion de collider
	public List<estructuraAnimaciones> animaciones;
	private estructuraAnimaciones animacionActual;
	
	// Use this for initialization
	void Start () {
		animacionActual = animaciones[0];
		this.animation.clip = animacionActual.clipAnimacion;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public estructuraAnimaciones getAnimacionActual(){
		return animacionActual;
	}
	
	public void setAnimationClipByName(string name){
		int i = 0;
		foreach(estructuraAnimaciones animacion in animaciones){
			if(animacion.clipAnimacion.name == name)
				animacionActual = animaciones[i];
			i++;
		}
		this.animation.clip = animacionActual.clipAnimacion;
	}
	
	public void playAnimationClip(){
		this.animation.Play();
	}
	
	public float getTiempoAnimationClip(){
		return animacionActual.clipAnimacion.length;
	}
	
	public float getTiempoStartAnimacion(){
		return animacionActual.tiempoStartAnimacion;
	}
}
