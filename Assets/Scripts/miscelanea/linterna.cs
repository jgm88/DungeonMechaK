using UnityEngine;
using System.Collections;

public class linterna : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			if(this.GetComponent<Light>().enabled)
				this.GetComponent<Light>().enabled = false;
			else
				this.GetComponent<Light>().enabled = true;
		}
	}
	
	public void encenderLinterna(){
		this.GetComponent<Light>().enabled = true;
	}
}
