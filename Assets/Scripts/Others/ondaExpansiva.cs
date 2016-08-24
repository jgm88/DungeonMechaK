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
		(GetComponent<Collider>() as SphereCollider).radius = radioAccion;
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Enemy") 
		{
			// Kill Enemy
			AchievementManager.Instance.NotifyDynamiteKillEnemy ();
			other.gameObject.GetComponent<EnemyBehaviour> ().ReceiveDamage (damageExplosion);
		} 
		else if (other.tag == "Player") 
		{
			// Kill player
			AchievementManager.Instance.NotifyDynamiteKillPlayer ();
			other.gameObject.GetComponent<PlayerBehaviour> ().ReceiveDamage (damageExplosion);
		}
		else if (other.tag == "Boss") 
		{
			other.gameObject.GetComponent<BossBehaviour> ().ReceiveDamage (damageExplosion);
		}
	}
}
