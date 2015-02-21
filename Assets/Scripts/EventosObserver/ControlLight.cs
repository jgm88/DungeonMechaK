using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ControlLight : MonoBehaviour {
	
	public float respawnTime = 10f; 
	public string sprite;
	/// <summary>
	/// Cantidad de energia de la llama
	/// </summary>
	private float energy;
	
	public GameObject flama;
	private controladorPickupsKey pickUpController;
	
	private bool tengoPickUp;
	private ligthLife life;
	
	private float deadVelocity;
	private SphereCollider areaAccion;
	
	private OTAnimatingSprite anim;
	
	/// <summary>
	/// compenentes de la antorcha para decrementar la fuerza de la luz
	/// </summary>
	private Torchelight torch;

	//TODO METER ESTO EN CADA MECHA
	private 
	
	void Start()
	{
		energy=100.0f;
		deadVelocity=3f;
		areaAccion=gameObject.GetComponent<SphereCollider>();
		torch= transform.parent.gameObject.GetComponent<Torchelight>();
		anim= transform.parent.GetChild(4).GetComponent<OTAnimatingSprite>();
		anim.alpha=0.0f;
		tengoPickUp=true;
		pickUpController=GameObject.FindWithTag("Player").GetComponent<controladorPickupsKey>();
	}
	
	private void canTurn(bool can)
	{
		life.comeToTurnOn=can;
	}
	
	void OnTriggerEnter(Collider luzPj)
	{
		if(luzPj.gameObject.CompareTag("Player"))
		{
			life=luzPj.gameObject.GetComponentInChildren<ligthLife>();
			canTurn(true);
//			GameObject.Find("GUITextAvivar").guiText.enabled=true;
		}
//		Invoke("QuitaGuiText",1f);	
	}

	void OnTriggerStay(Collider luzPj)
	{
		if(luzPj.gameObject.CompareTag("Player"))
		{
			if(Input.GetKeyDown(KeyCode.F)){
				//luzPj.gameObject.SendMessage("reproducirEspecial",SendMessageOptions.DontRequireReceiver);
				drenar();
			}
		}
	}

	void OnTriggerExit(Collider luzPj)
	{
		if(luzPj.gameObject.CompareTag("Player"))
			canTurn(false);
	}
	
	public void drenar()
	{
		turnOff();
		
		if(torch.IntensityLight<=0.0f)
			TemporalDesactivate();
	}
	
	private float turnOff()
	{	
		return torch.IntensityLight-=deadVelocity*Time.deltaTime;		
	}
	
	private void desactivar(bool activa)
	{
		//transform.parent.GetChild(0).gameObject.SetActive(activa);
		transform.parent.GetChild(1).gameObject.SetActive(activa);
		areaAccion.enabled=!areaAccion.enabled;		
	}
	
	private void activaFlama()
	{
		pickUpController.recogerLlave();
		flama.SetActive(true);
		tengoPickUp=false;
	}
	
	private void TemporalDesactivate()
	{
		
		canTurn(false);
		desactivar(false);
		anim.alpha=1.0f;
		anim.PlayOnce();
		if(tengoPickUp)
			activaFlama();
		//yield return new WaitForSeconds(anim.animation.duration);
		//anim.alpha=0;
		Invoke("Ocultar",anim.animation.duration);
		Invoke("Reactivar", respawnTime);
	}
	private void Ocultar(){
		anim.alpha=0;	
	}
	private void Reactivar(){
		desactivar(true);
		torch.IntensityLight=1;
	}
	
}

