using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {
	
	private float t1;
	private float t2;
	private bool espera;

	// Use this for initialization
	void Start () {
		GameObject.Find("mirilla2").guiTexture.enabled = true;
		GameObject.Find("mirilla2").guiTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		cambioMirilla();
	}
	
	private void cambioMirilla(){
		if(Input.GetMouseButtonDown(0)){
			GameObject.Find("mirilla1").guiTexture.enabled = false;
			GameObject.Find("mirilla2").guiTexture.enabled = true;
			//gaurdo el instante del impacto
			t1 = Time.time;
			t2 = t1;
			espera = true;
		}
		
		//calculo el tiempo transcurrido desde el ultimo impacto
		if(espera){
			t2 += Time.deltaTime;
		}
		//si transcurre el tiempo deseado se desbloquea el semaforo y cambio mirilla
		if(espera && (t2 -t1) >= 1){
			espera = false;
			GameObject.Find("mirilla1").guiTexture.enabled = true;
			GameObject.Find("mirilla2").guiTexture.enabled = false;
		}
	}
}
