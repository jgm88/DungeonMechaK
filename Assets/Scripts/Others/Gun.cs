using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletPrefab;
	public bool canShot;	
	public float coolDown = 2f;
	public int manaCost = 20;

	private PlayerBehaviour _pb;

	void Awake () {
		_pb = GameObject.FindWithTag("player").GetComponent<PlayerBehaviour>();
		canShot = true;

	}
	public void Shoot(){
		if(canShot){
			_pb.DeductMana(manaCost);
			Instantiate(bulletPrefab, transform.position, transform.rotation);
			canShot = false;
			StartCoroutine(COCoolDown());				
		}
	}


	IEnumerator COCoolDown(){
		
		yield return new WaitForSeconds(coolDown);
		canShot = true;

	}
}
