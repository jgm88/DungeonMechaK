using UnityEngine;
using System.Collections;

public class linterna : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			if(this.light.enabled)
				this.light.enabled = false;
			else
				this.light.enabled = true;
		}
	}
	
	public void encenderLinterna(){
		this.light.enabled = true;
	}
}
