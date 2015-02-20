using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletPrefab;
	public bool canShot;	
	public float coolDown = 2f;
	public int manaCost = 20;

	private PlayerBehaviour _playerBehaviour;

	void Awake () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		canShot = true;

	}
	public void Shoot(){
		if(canShot && _playerBehaviour.mana >= manaCost){

			_playerBehaviour.DeductMana(manaCost);
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
