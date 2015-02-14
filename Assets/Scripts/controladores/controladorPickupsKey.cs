using UnityEngine;
using System.Collections;

public class controladorPickupsKey : MonoBehaviour {
	
	public int llavesNecesarias = 4;
	private int llavesRecogidas = 0;
	public GameObject puerta1;
	public GameObject puerta2;
	public GameObject muropuerta;
	
	private string tagOld = "TorchePlayer";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void recogerLlave(){
		llavesRecogidas++;
		if(llavesRecogidas == llavesNecesarias){
			puerta1.SetActive(false);
			puerta2.SetActive(false);
			muropuerta.SetActive(false);
		}
	}
	
	public int getNumKeysActuales(){
		return llavesRecogidas;
	}
	public void cambiaTorch(string torchColor){
		int I;
	}
}
