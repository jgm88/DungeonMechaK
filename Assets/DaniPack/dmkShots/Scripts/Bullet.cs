using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float lifeTime = 0.5f;
	public float speed = 10f;
	public float distancia = 10000f;
	public int force = 10;
	public GameObject explosion;
	public GameObject radioExplosion;
	
	private float spawnTime = 0.0f;
	private Transform tr;
	private GameObject pool;
	private bool disparado = false;
	
	
	//raycast para orientarse hacia el raton
	private GameObject mecha;
	private Ray rayoRaton;
	private RaycastHit hit;
	
	void Start(){
		Screen.lockCursor = true;
		pool = GameObject.Find("PoolCache");
	}
	void OnEnable(){
		tr = transform;
		spawnTime = Time.time;
		mecha = GameObject.FindWithTag("Torch");
		mecha.GetComponent<ligthLife>().lanzarBomba();
	}
	
	void Update () {
		
		if(!disparado){
			/*rayoRaton = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(rayoRaton,out hit);
			
			Vector3 v = hit.point;
			
			gameObject.rigidbody.velocity = (v * speed * Time.deltaTime);
			*/
			gameObject.rigidbody.AddForce(transform.forward * speed);
			disparado = true;
		}
		
		distancia = speed * Time.deltaTime;
		if(Time.time > spawnTime + lifeTime || distancia < 0){
			disparado = false;
			PoolsController.Instance.Spawn(radioExplosion, transform.position, transform.rotation);
			PoolsController.Instance.Spawn(explosion, transform.position, transform.rotation);
			PoolsController.Instance.Destroy(gameObject);
			//pool.SendMessage("Destroy", gameObject, SendMessageOptions.DontRequireReceiver);
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
				other.gameObject.SendMessage("AddImpact",-dir.normalized * force,SendMessageOptions.DontRequireReceiver);
			}
			//DPS
			//c.gameObject.SetActive(false);
		}
	}
}
