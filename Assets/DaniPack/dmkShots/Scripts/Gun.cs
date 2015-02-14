using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	
	public float frecuencia = 10;
	public float conoDispersion = 1.5f;
	public bool firing = false;
	public float dps = 20f;
	public float fuerzaPS = 20f;
	//public GameObject muzzleFlashFront
	//fuego de arma, con luz
	private float lastFireTime = -1;
	
	
	void Awake () {
		if(spawnPoint == null) spawnPoint = transform;
	}
	public bool canShot(){
		if(bulletPrefab.name=="Dinamita") return GameObject.FindWithTag("Torch").GetComponent<ligthLife>().canBomba();
		if(bulletPrefab.name=="FuegoSkill") return GameObject.FindWithTag("Torch").GetComponent<ligthLife>().canFire();
		return true;
	} 
	void Update () {
		if(firing && canShot()){
			if(Time.time > lastFireTime + 1 / frecuencia){
				//spawn visual bullet
				Quaternion coneRandomRotation = Quaternion.Euler(Random.Range(-conoDispersion, conoDispersion), 
					Random.Range(-conoDispersion, conoDispersion),0);
				GameObject go = PoolsController.Instance.Spawn(bulletPrefab, 
					spawnPoint.position, spawnPoint.rotation * coneRandomRotation) as GameObject;
				Bullet bullet = go.GetComponent<Bullet>();
				
				lastFireTime = Time.time;
				
			}
		}
	}
	public void OnStartFire(){
		if(Time.timeScale == 0) return;
		firing = true;
		//transform.parent.FindChild("Torch").SendMessage("lanzaBomba",firing,SendMessageOptions.DontRequireReceiver);
	}
	public void OnStopFire(){
		firing = false;
		
	}
}
