using UnityEngine;
using System.Collections;

public class cameraFace : MonoBehaviour {
	
	private bool fijado = false;
	public Transform faceTo;
	// Use this for initialization
	void Start () {
		if(GameObject.FindWithTag("Player") != null && faceTo != null)
			faceTo = GameObject.FindWithTag("Player").transform;
		faceCamera();
	}
	
	//automaticamente busca el player por tag y si existe y ya no esta asignado otro transform lo añade
	void Awake(){
		
	}
	// Update is called once per frame
	void Update () {
		if(!fijado){
			faceTo = GameObject.FindWithTag("Player").transform;
			fijado = true;
		}

	}
	
	void FixedUpdate() {
        faceCamera();
    }
	
	public void faceCamera(){
		
		this.transform.LookAt(faceTo); 
	}
}
