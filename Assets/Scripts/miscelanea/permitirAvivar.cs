using UnityEngine;
using System.Collections;

public class permitirAvivar : MonoBehaviour {
	
	public GameObject guitextAvivar;
	
	// Use this for initialization
	void Start () {
		guitextAvivar=GameObject.FindWithTag("GUITextAvivar");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player")
			guitextAvivar.guiText.enabled = true;
		Invoke("QuitaGuiText",2f);
	}
	void QuitaGuiText(){
		guitextAvivar.guiText.enabled =false;
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Player")
			guitextAvivar.guiText.enabled = false;
	}
}
