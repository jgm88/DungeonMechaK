using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ManejadorIA))]

public class LoadAnimIA : MonoBehaviour {
	
	private ManejadorIA manejador;
	private OTAnimatingSprite animacion;
	public string caminar;
	public string morirse;
	public string atacar;
	//private manejadorVida vidaController;
	private bool isVivo;
	
	void Awake()
	{
		manejador= this.gameObject.GetComponent<ManejadorIA>();
		animacion= this.gameObject.GetComponentInChildren<OTAnimatingSprite>();
		isVivo=true;
	}
	// Update is called once per frame
	void Update () {
		if(!animacion.isPlaying && isVivo){
			animacion.Play("walk");
		}	
	}
	
	public void morir()
	{
		if(animacion.isPlaying)
			animacion.Stop();
		gameObject.GetComponent<AIPath>().canMove=false;
		
		transform.FindChild("Eweapon").gameObject.SetActive(false);
		animacion.speed=0.2f;
		animacion.PlayOnce(morirse);
		transform.position= new Vector3(transform.position.x,transform.position.y-1f,transform.position.z);
		isVivo=false;
	}
	public void attack()
	{
		if(animacion.isPlaying)
			animacion.Stop();
		animacion.PlayOnce(atacar);
		
	}
	public void desdibujar(){
		animacion.alpha=0;	
	}
}
