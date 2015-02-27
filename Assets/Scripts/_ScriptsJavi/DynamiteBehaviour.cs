using UnityEngine;
using System.Collections;

public class DynamiteBehaviour : MonoBehaviour {

	public float lifeTime = 0.5f;
	public float speed = 10f;
	public GameObject explosion;
	public GameObject AoeExplosion;

	private float _randomRotation;

	void OnEnable(){
		gameObject.rigidbody.AddForce(transform.forward * speed);
		_randomRotation = Random.Range(0f, 360f);
		rigidbody.AddTorque(_randomRotation,_randomRotation, _randomRotation);
		audio.Play();
		StartCoroutine(CoLifeTimer());

	}
	void Update(){

	}
	IEnumerator CoLifeTimer(){
		yield return new WaitForSeconds(lifeTime);
		Instantiate(explosion, transform.position, transform.rotation);
		Instantiate(AoeExplosion, transform.position, transform.rotation);
		Destroy( gameObject);
//		yield return new WaitForSeconds (2f);
//		Destroy (explosion);

	}
}
