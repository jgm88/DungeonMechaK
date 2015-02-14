using UnityEngine;
using System.Collections;

public class permitirPoder : MonoBehaviour {
	
	public GUIText guitextPoder;
	private controladorAtaqueCC controladorAtaque;
	public string poder;
	public string tagColorTorch;
	
	// Use this for initialization
	void Start () {
		controladorAtaque = GameObject.FindWithTag("Player").GetComponent<controladorAtaqueCC>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		if(other.tag == "Player"){
			guitextPoder.enabled = true;
			if(Input.GetKeyDown(KeyCode.F)){
				controladorAtaque.setPoderActual(poder);
				other.gameObject.GetComponent<controladorPickupsKey>().cambiaTorch(tagColorTorch);
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			guitextPoder.enabled = false;
			
		}
	}
}
