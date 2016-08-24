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
			guitextAvivar.GetComponent<GUIText>().enabled = true;
		Invoke("QuitaGuiText",2f);
	}
	void QuitaGuiText(){
		guitextAvivar.GetComponent<GUIText>().enabled =false;
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Player")
			guitextAvivar.GetComponent<GUIText>().enabled = false;
	}
}
