using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class AccionadorController : MonoBehaviour {
	
	public List<GameObject> observadores;
	protected static List<EventController> observadoresEvent= new List<EventController>();
	private static bool _pulsado;
	public static bool pulsado
	{
		get{return _pulsado;}
		set{_pulsado=value;}
	}
	
	
	void Awake()
	{
		foreach(GameObject g in observadores)
			observadoresEvent.Add(g.GetComponent<EventController>());	
		
	}
	
	
	public void anyadeObersavor(GameObject g)
	{
		observadoresEvent.Add(g.GetComponent<EventController>());
	}
	protected void enviarActualizacion()
	{
		foreach(EventController c in observadoresEvent)
		{
			c.actualizar();
		}
	}
	
	protected void actializa(int index)
	{
		observadoresEvent[index].actualizar();
	}
	
	/*void OnTriggerEnter(Collider c)
	{
		
	}
	*/
	
}
