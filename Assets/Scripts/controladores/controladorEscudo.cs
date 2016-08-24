using UnityEngine;
using System.Collections;

public class controladorEscudo : MonoBehaviour {
	
	public bool escudado = false;
	public GameObject escudo;
	// Use this for initialization
	void Start () {
		escudo.GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		cubrirse();
	}
	
	void OnTriggerEnter(Collider other){
		if(escudo.GetComponent<Collider>().enabled)
		Debug.Log("bloqueo!!!");
	}
	
	//cambia el estado de cubierto o no
	public void cubierto(){
			this.escudado = !this.escudado;
	}
	
	private void cubrirse(){
		if(Input.GetButtonDown("Fire2")){
			escudo.GetComponent<Animation>().clip = escudo.GetComponent<Animation>().GetClip("escudarse");
			escudo.GetComponent<Animation>().Play();
			escudo.GetComponent<Collider>().enabled = true;
			this.escudado = true;
			StartCoroutine(coroutinaEsperarBloqueando());
		}
	}
	
	private void descubrirse(){
		escudo.GetComponent<Animation>().clip = escudo.GetComponent<Animation>().GetClip("desescudarse");
		escudo.GetComponent<Animation>().Play();
		escudo.GetComponent<Collider>().enabled = false;
		this.escudado = false;
	}
	
	IEnumerator coroutinaEsperarBloqueando(){
		while(Input.GetButton("Fire2")){
			yield return coroutinaEsperarBloqueando();
		}
		descubrirse();
		yield break;
	}
}
