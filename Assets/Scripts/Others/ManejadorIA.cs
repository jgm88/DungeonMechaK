using UnityEngine;
using System.Collections;

public class ManejadorIA : MonoBehaviour {
	
	private Vector3 posAnterior;
	
	[System.NonSerialized]
	public bool isMoving;
	// Use this for initialization
	void Start () {
		posAnterior=transform.position;
		isMoving=false;
	}
	
	private bool enMovimiento()
	{
		if(!posAnterior.Equals(transform.position))
		{
			posAnterior=transform.position;
			return true;
		}
			
		return false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(enMovimiento())
			isMoving=true;
		
			
	}
}
