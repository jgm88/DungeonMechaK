using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controladorSpawn : MonoBehaviour
{
	#region private Fields
	private int _levelByKeys = 0;
	private bool _inBoss = false;
	private GameObject _target;
	#endregion

	#region public fields
	public List<GameObject> spawnPointsNormal;
	public List<GameObject> spawnPointsBoss;
	public float spawnIntervale = 30f;
	public List<GameObject> prefabsEnemigos;
	public int numOfEnemiesSpawned = 15;
	public int numOfEnemiesSpawnedBoss = 5;
	public int maxEnemies = 18;
	#endregion
	
	// Use this for initialization
	void Start ()
	{
		_target = GameObject.FindWithTag ("Player");
		_levelByKeys = GameObject.FindWithTag ("Player").GetComponent<controladorPickupsKey> ().getNumKeysActuales ();
		foreach (GameObject g in prefabsEnemigos) {
			g.GetComponent<AIPath> ().target = _target.transform;
		}
		startSpawner ();
	}

	void Update ()
	{
		Debug.Log (GetNumOfEnemiesInGame ());
	}

	#region private Functions

	/// <summary>
	/// Gets the number of enemies in game.
	/// </summary>
	/// <returns>The number of enemies in game.</returns>
	private int GetNumOfEnemiesInGame ()
	{
		int enemies = 0;
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (enemy.name != "Boss")
				enemies++;
		}
		
		return enemies;
	}

	/// <summary>
	/// Gets a rand point for normal enemies Spawner.
	/// </summary>
	/// <returns>The rand point normal.</returns>
	private int GetRandPointNormal ()
	{
		int indice = Random.Range (0, (spawnPointsNormal.Count - 1));
		return indice;
	}

	/// <summary>
	/// Gets a rand point for boss spawner.
	/// </summary>
	/// <returns>The rand point boss.</returns>
	private int GetRandPointBoss ()
	{
		int indice = Random.Range (0, (spawnPointsBoss.Count - 1));
		return indice;
	}

	#endregion

	#region public Functions

	/// <summary>
	/// Destroies the enemies outside boss area.
	/// </summary>
	public void DestroyEnemiesOutsideBoss ()
	{
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (enemy.name != "Boss")
				Destroy (enemy);
		}
	}

	/// <summary>
	/// Determines whether we are is in boss area.
	/// </summary>
	/// <returns><c>true</c> if this instance is in boss; otherwise, <c>false</c>.</returns>
	public void IsInBoss ()
	{
		_inBoss = true;
	}

	/// <summary>
	/// Gets a random num for spawning enemies in normal area
	/// </summary>
	/// <returns>The index.</returns>
	public int NextIndexNormal ()
	{
		return GetRandPointNormal ();
	}

	/// <summary>
	/// Gets a random num for spawning enemies in boss area
	/// </summary>
	/// <returns>The index .</returns>
	public int NextIndexBoss ()
	{
		return GetRandPointBoss ();
	}

	/// <summary>
	/// Instantiates the enemies in normal area.
	/// </summary>
	public void InstantiateEnemiesNormal ()
	{
		int enemiesInGame = GetNumOfEnemiesInGame ();
		for (int i=0; i<= numOfEnemiesSpawned && enemiesInGame <= maxEnemies; i++) {
			Instantiate (prefabsEnemigos [_levelByKeys], spawnPointsNormal [GetRandPointNormal ()].transform.position, Quaternion.identity);
		}
	}

	/// <summary>
	/// Instantiates the enemies in boss area.
	/// </summary>
	public void instantiateEnemiesBoss ()
	{
		int enemiesInGame = GetNumOfEnemiesInGame ();
		for (int i=0; i<= numOfEnemiesSpawnedBoss && enemiesInGame <= maxEnemies; i++) {
			Instantiate (prefabsEnemigos [_levelByKeys], spawnPointsBoss [GetRandPointBoss ()].transform.position, Quaternion.identity);
		}
	}

	/// <summary>
	/// Starts the spawner.
	/// </summary>
	public void startSpawner ()
	{
		StartCoroutine (COSpawnNormal ());
	}

	#endregion

	#region Coroutines
	
	/// <summary>
	/// CORoutine that controls the normal Spawner by a time interval.
	/// </summary>
	/// <returns>Recursive Coroutine Spawning Enemies.</returns>
	IEnumerator COSpawnNormal ()
	{
		if (!_inBoss) {
			InstantiateEnemiesNormal ();
			yield return new WaitForSeconds (spawnIntervale);
			yield return StartCoroutine (COSpawnNormal ());
		} else {
			StartCoroutine (COSpawnBoss ());
		}
	}
	
	/// <summary>
	/// CORoutine that controls the boss Spawner by a time interval.
	/// </summary>
	/// <returns>Recursive Coroutine Spawning Enemies.</returns>
	IEnumerator COSpawnBoss ()
	{
		instantiateEnemiesBoss ();
		yield return new WaitForSeconds (spawnIntervale);
		yield return StartCoroutine (COSpawnBoss ());
	}

	#endregion
}
