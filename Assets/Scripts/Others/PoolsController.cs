using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Almacena los objetos de la pool, Serializable muestra los atributes
/// </summary>
[System.Serializable]
public class ObjectPool{ //ObjectCache	
	public GameObject prefab;
	//numero maximo de municion, enemigos, etc
	public int cacheSize= 10;
	private GameObject[] objects ;
	private int cacheIndex = 0;
	
	public void Initialize ()
	{
		objects = new GameObject[cacheSize];
		
		// intancia los objetos en el array y los pasa a inactivo
		for (int i = 0; i < cacheSize; i++)
		{
			objects[i] = MonoBehaviour.Instantiate (prefab) as GameObject;
			objects[i].SetActive (false);
			objects[i].name = objects[i].name + i;
		}
	}
	
	public GameObject GetNextObjectInCache (){
		GameObject obj = objects[cacheIndex];
		// El cacheIndex comienza en la posición del objeto creado en ultimo lugar,
		// este suele estar libre, en caso de que no sea asi, hace un bucle a traves
		// del cache hasta encontrar uno libre
	
		// si encontramos uno no activo se para el bucle
		for (int i = 0; i < cacheSize && obj.activeSelf; i++) {
			// comprobamos que no se salga del array
			cacheIndex = (cacheIndex + 1) % cacheSize;
			obj = objects[cacheIndex];	
		}
		
		// The object should be inactive. If it's not, log a warning and use
		// the object created the longest ago even though it's still active.
		if (obj.activeSelf) {
			PoolsController.Instance.Destroy (obj);
		}
	
		// Increment index and make it loop around
		// if it exceeds the size of the cache
		cacheIndex = (cacheIndex + 1) % cacheSize;
		return obj;
	}
}
public class PoolsController : MonoBehaviour { //Spawner
	
	/// <summary>
	/// Podemos crear varias pools, de balas, misiles, etc.
	/// </summary>
	public ObjectPool[] caches;	
	private Hashtable activeCachedObjects;
	
	//SINGLETON
	public static PoolsController Instance { get; private set;}
	
	void Awake () {
		
		Instance = this;
		
		// Numero total de objetos entre todas las pools
		int totalCache = 0;
	
		// recorremos las pools y las inicializamos
		foreach(ObjectPool c in caches){
			c.Initialize();
			totalCache += c.cacheSize;
		}
		// Creamos una hash table  con capacidad para todos los items de las pools
		activeCachedObjects = new Hashtable (totalCache);
	}
	
	public GameObject Spawn (GameObject prefab,Vector3 position,Quaternion rotation)  {
		
		ObjectPool cache = null;
		// Find the cache for the specified prefab
		foreach(ObjectPool c in Instance.caches){
			if(c.prefab == prefab){
				cache = c; break;
			}
		}
		// If there's no cache for this prefab type, just instantiate normally
		if (cache == null) {
			return Instantiate (prefab, position, rotation) as GameObject;
		}
	
		// Find the next object in the cache
		GameObject obj = cache.GetNextObjectInCache();
	
		// Set the position and rotation of the object
		obj.transform.position = position;
		obj.transform.rotation = rotation;
	
		// Set the object to be active
		obj.SetActive (true);
		Instance.activeCachedObjects[obj] = true;
	
		return obj;
	}
	
	public void Destroy (GameObject objectToDestroy) {
		if (Instance && Instance.activeCachedObjects.ContainsKey(objectToDestroy)) {
			objectToDestroy.SetActive(false);
			if(objectToDestroy.GetComponent<Rigidbody>())
				objectToDestroy.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Instance.activeCachedObjects[objectToDestroy] = false;
		}
		else Destroy(objectToDestroy);
	}
}

