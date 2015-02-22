using UnityEngine;
using System.Collections;

public class ondaExpansiva : MonoBehaviour
{
	
	public int damageExplosion = 150;
	public float radioAccion = 4f;
	public float lifeTime = 1f;
	// Use this for initialization
	void Start ()
	{
		(collider as SphereCollider).radius = radioAccion;
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyBehaviour> ().ReceiveDamage (damageExplosion);
		} else if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerBehaviour> ().ReceiveDamage (damageExplosion);
		} else if (other.tag == "Boss") {
			other.gameObject.GetComponent<BossBehaviour> ().ReceiveDamage (damageExplosion);
		}
	}
}
