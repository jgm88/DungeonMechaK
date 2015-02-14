using UnityEngine;
using System.Collections;

public class controladorGuitexts : MonoBehaviour {
	
	public GameObject mensajeMuerte;
	public GUIText volverMenu;
	public GUIText reintentar;
	private Color original;

	// Use this for initialization
	void Start () {
		original = reintentar.color;
	}
	
	// Update is called once per frame
	void Update () {
		accionReintentar();
		accionVolverMenu();
	}
	
	//activa el menu muerte
	public void enableMensajeMuerte(){
		Screen.lockCursor=false;
		//debe activarse dos veces pork se ralla el itweens
		mensajeMuerte.SetActive(true);
		mensajeMuerte.SetActive(false);
		mensajeMuerte.SetActive(true);
	}
	

	
	public void accionReintentar(){
		//Camera.current.camera = guitextCamera;
		if(reintentar.HitTest(Input.mousePosition)) {
			reintentar.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				Application.LoadLevel (1);
			}
		}
		else{
			reintentar.color = original;
		}
	}
	
	public void accionVolverMenu(){
		//Camera.current.camera = guitextCamera;
		if(volverMenu.HitTest(Input.mousePosition)) {
			volverMenu.color = Color.yellow;
			if(Input.GetButtonDown("Fire1")){
				Application.LoadLevel (0);
			}
		}
		else{
			volverMenu.color = original;
		}
	}
}
