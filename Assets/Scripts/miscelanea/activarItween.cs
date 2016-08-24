using UnityEngine;
using System.Collections;

public class activarItween : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//activarItweenMuerte();
	}
	
	public void activarItweenMuerte(){
		
		//if(Input.GetKeyDown(KeyCode.P)){
			Debug.Log("entra itween");
			this.gameObject.GetComponent<GUIText>().enabled = true;
		iTweenEvent.GetEvent(this.gameObject, "puta").Play();
		//}
	}
}
