using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public bool canShot;
	
	public float frecuencia = 10;
	public float conoDispersion = 1.5f;
	public bool firing = false;
	public float dps = 20f;
	public float fuerzaPS = 20f;
	public string boton;
	public GameObject mecha;
	public float coolDown = 2f;

	//CACA DE INPUTS



	
	
	void Awake () {
		if(spawnPoint == null) spawnPoint = transform;
		canShot = true;
	}
	public void Shoot(){
		if(canShot){
			Quaternion coneRandomRotation = Quaternion.Euler(Random.Range(-conoDispersion, conoDispersion), 
			                                                 Random.Range(-conoDispersion, conoDispersion),0);
			Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation * coneRandomRotation);
			canShot = false;
			StartCoroutine(COCoolDown());
		}

	}

	IEnumerator COCoolDown(){
		
		yield return new WaitForSeconds(coolDown);
		canShot = true;

	}
}
