using UnityEngine;
using System.Collections;

public class ondaExpansiva : MonoBehaviour {
	
	public float damageExplosion = 150f;
	public float radioAccion = 4f;
	public float lifeTime = 1f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider other){
		if(other.tag == "Enemy"){
			other.gameObject.SendMessage("restarVidaEnemigo", damageExplosion, SendMessageOptions.DontRequireReceiver);
		}
		else if(other.tag == "Player"){
			other.gameObject.SendMessage("restarVida",damageExplosion,SendMessageOptions.DontRequireReceiver);
		}
	}
}
