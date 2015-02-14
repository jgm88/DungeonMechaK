using UnityEngine;
using System.Collections;

public class FuegoSkill : MonoBehaviour {
	
	public float lifeTime = 0.5f;
	public int force = 10;
	public float dps = 70f;
	
	private GameObject mecha;
	private float spawnTime = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable(){
		spawnTime = Time.time;
		
		mecha = GameObject.FindWithTag("Torch");
		mecha.GetComponent<ligthLife>().lanzarllamas();
	}
	// Update is called once per frame
	void Update () {
	
		if(Time.time > spawnTime + lifeTime){
				PoolsController.Instance.Destroy(gameObject);
			}
	}
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Enemy"){
			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;
			if (other.rigidbody){
	 			 other.rigidbody.AddForce(dir.normalized * force);
			} 
			else { // use a special script for character controllers:
				other.gameObject.SendMessage("AddImpact",dir.normalized * force,SendMessageOptions.DontRequireReceiver);
			}
			other.SendMessage("restarVidaEnemigo", dps, SendMessageOptions.DontRequireReceiver);
		}
	}
}