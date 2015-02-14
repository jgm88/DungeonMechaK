using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controladorSpawn : MonoBehaviour {
	
	public List<GameObject> puntosSpawnNormal;
	public List<GameObject> puntosSpawnBoss;
	private int nivelSegunLlaves = 0;
	public float intervaloSpawn = 30f;
	public List<GameObject> prefabsEnemigos;
	public int numeroEnemigosSpawneados = 20;
	public int numeroEnemigosSpawneadosBoss = 5;
	private bool enBoss = false;
	private GameObject target;
	
	// Use this for initialization
	void Start () {
		target=GameObject.FindWithTag("Player");
		nivelSegunLlaves = GameObject.FindWithTag("Player").GetComponent<controladorPickupsKey>().getNumKeysActuales();
		foreach(GameObject g in prefabsEnemigos){
			g.GetComponent<AIPath>().target=target.transform;
		}
		startSpawner();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void destriurEnemigosFueraBoss(){
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
			if(enemy.name !="Boss")
				Destroy (enemy);
		}
	}
	public void setInBoss(){
		enBoss = true;
	}
	
	private int generarPuntoRandomNormal(){
		int indice = Random.Range(0, (puntosSpawnNormal.Count -1));
		return indice;
	}
	
	private int generarPuntoRandomBoss(){
		int indice = Random.Range(0, (puntosSpawnBoss.Count -1));
		return indice;
	}
	
	public int nextIndiceNormal(){
		return generarPuntoRandomNormal();
	}
	
	public int nextIndiceBoss(){
		return generarPuntoRandomBoss();
	}
	
	public void instantiateEnemigosNormal(){
		for(int i=0; i<= numeroEnemigosSpawneados; i++){
			Instantiate(prefabsEnemigos[nivelSegunLlaves], puntosSpawnNormal[generarPuntoRandomNormal()].transform.position, Quaternion.identity);
		}
	}
	
	public void instantiateEnemigosBoss(){
		for(int i=0; i<= numeroEnemigosSpawneadosBoss; i++){
			Instantiate(prefabsEnemigos[nivelSegunLlaves], puntosSpawnBoss[generarPuntoRandomBoss()].transform.position, Quaternion.identity);
		}
	}
	
	public void startSpawner(){
		StartCoroutine(COSpawnearNormal());
	}
	
	//coroutina que instancia enemigos cada intervalo
	IEnumerator COSpawnearNormal(){
		if(!enBoss){
			instantiateEnemigosNormal();
			yield return new WaitForSeconds(intervaloSpawn);
			yield return StartCoroutine(COSpawnearNormal());
		}
		else{
			StartCoroutine(COSpawnearBoss());
		}
	}
	
	//coroutina que instancia enemigos cada intervalo
	IEnumerator COSpawnearBoss(){
		instantiateEnemigosBoss();
		yield return new WaitForSeconds(intervaloSpawn);
		yield return StartCoroutine(COSpawnearBoss());
	}
}
