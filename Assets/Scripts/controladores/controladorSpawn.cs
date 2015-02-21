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
		_levelByKeys = 0;//GameObject.FindWithTag ("Player").GetComponent<controladorPickupsKey> ().getNumKeysActuales ();
		foreach (GameObject g in prefabsEnemigos) {
			g.GetComponent<AIPath> ().target = _target.transform;
		}
		startSpawner ();
	}

	void Update ()
	{
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
		IList<int> pointsSpawned = new List<int> ();
		int enemiesInGame = GetNumOfEnemiesInGame ();
		int randIndex = 0;
		//en el bucle controlamos si hemos spawneado un enemigo en ese punto y esperamos 1s para que no se stakeen aunque tengan collider
		for (int i=0; i<= numOfEnemiesSpawned && enemiesInGame <= maxEnemies; i++) {
			randIndex = GetRandPointNormal ();
			if (pointsSpawned.IndexOf (randIndex) != -1) {
				StartCoroutine (COWaitSameSpawnPoint (true, randIndex));
			} else {
				StartCoroutine (COWaitSameSpawnPoint (false, randIndex));
			}
			pointsSpawned.Add (randIndex);
		}
		pointsSpawned.Clear ();
	}

	/// <summary>
	/// Instantiates the enemies in boss area.
	/// </summary>
	public void instantiateEnemiesBoss ()
	{
		int enemiesInGame = GetNumOfEnemiesInGame ();
//		for (int i=0; i<= numOfEnemiesSpawnedBoss && enemiesInGame <= maxEnemies; i++) {
//			Instantiate (prefabsEnemigos [_levelByKeys], spawnPointsBoss [GetRandPointBoss ()].transform.position, Quaternion.identity);
//		}
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
		if (!_inBoss && this.enabled) {
			InstantiateEnemiesNormal ();
			yield return new WaitForSeconds (spawnIntervale);
			yield return StartCoroutine (COSpawnNormal ());
		} else if (this.enabled) {
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

	/// <summary>
	/// Coroutine for wait a second to avoid stacking spawning enemies in same point.
	/// </summary>
	/// <returns>The wait same spawn point.</returns>
	/// <param name="wait">If set to <c>true</c> wait.</param>
	/// <param name="index">Index.</param>
	IEnumerator COWaitSameSpawnPoint (bool wait = false, int index = 0)
	{
		if (wait) {
			yield return new WaitForSeconds (1f);
			//Instantiate (prefabsEnemigos [_levelByKeys], spawnPointsNormal [index].transform.position, Quaternion.identity);
		} else {
			//Instantiate (prefabsEnemigos [_levelByKeys], spawnPointsNormal [index].transform.position, Quaternion.identity);
		}

	}

	#endregion
}
