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
	private bool disparado = false;
	
	
	//raycast para orientarse hacia el raton
	private GameObject mecha;
	private Ray rayoRaton;
	private RaycastHit hit;
	
	void Start(){
		Screen.lockCursor = true;
	}
	void OnEnable(){
		tr = transform;
		spawnTime = Time.time;
		mecha = GameObject.FindWithTag("Torch");
		mecha.GetComponent<ligthLife>().lanzarBomba();
	}
	
	void Update () {
		
		if(!disparado){

			gameObject.rigidbody.AddForce(transform.forward * speed);
			disparado = true;
		}

	}
}
