using UnityEngine;
using System.Collections;

public class Manejador : MonoBehaviour {
	
	
	public static bool arriba,abajo,izquierda,derecha,atack;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		arriba=abajo=izquierda=derecha=atack=false;
		
		if(Input.GetButtonDown("Fire1"))
			atack=true;
		
		if(Input.GetAxis("Vertical")>0)
			arriba= true;
		else if(Input.GetAxis("Vertical")<0)
			abajo=true;
		
		if(Input.GetAxis("Horizontal")>0)
			derecha=true;
		else if(Input.GetAxis("Horizontal")<0)
			izquierda=true;
	}
}
