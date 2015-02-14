using UnityEngine;
using System.Collections;

public class mueveteputo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(new Vector3(0f,1f,0f));
		gameObject.transform.Translate(new Vector3(0f,0.01f,0f));
	}
}
