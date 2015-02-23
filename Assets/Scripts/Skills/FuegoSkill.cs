using UnityEngine;
using System.Collections;

public class FuegoSkill : MonoBehaviour {
	
	public float lifeTime = 0.5f;
	public int dps = 70;

	private float spawnTime = 0.0f;

	void OnEnable(){
		spawnTime = Time.time;
	}

	void Update () {
	
		if(Time.time > spawnTime + lifeTime){
			GameObject.Destroy(gameObject);
		}
	}
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Enemy"){
			other.GetComponent<EnemyBehaviour>().ReceiveDamage(dps);
		}
		else if(other.tag=="Boss")
		{
			other.GetComponent<BossBehaviour>().ReceiveDamage(dps);
		}
	}
}