using UnityEngine;
using System.Collections;

public class DynamiteBehaviour : MonoBehaviour {

	public float lifeTime = 0.5f;
	public float speed = 10f;
	public GameObject explosion;
	public GameObject AoeExplosion;

	void OnEnable(){
		gameObject.rigidbody.AddForce(transform.forward * speed);
		StartCoroutine(CoLifeTimer());

	}
	IEnumerator CoLifeTimer(){
		yield return new WaitForSeconds(lifeTime);
		Instantiate(explosion, transform.position, transform.rotation);
		Instantiate(AoeExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
