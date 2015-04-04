using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controladorSpawn : MonoBehaviour
{
	#region private Fields

	private bool _inBoss = false;
	private GameObject _target;
	#endregion

	#region public fields
	public List<GameObject> spawnPointsNormal;
	public List<GameObject> spawnPointsBoss;
	public float spawnIntervale = 5f;
	public List<GameObject> prefabsEnemigos;
	public int maxEnemies = 18;
	public int enemiesInGame = 0;
	#endregion
	
	// Use this for initialization
	void Start ()
	{
		_target = GameObject.FindWithTag ("Player");

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
	public void IsInBoss (bool isInBoss)
	{
		_inBoss = isInBoss;
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
	/// Starts the spawner.
	/// </summary>
	public void startSpawner ()
	{
		StartCoroutine (InstantiateEnemiesNormal ());
	}

	#endregion

	#region Coroutines

	/// <summary>
	/// Instantiates the enemies in boss area.
	/// </summary>
	IEnumerator instantiateEnemiesBoss ()
	{
		int randIndex = 0;
		randIndex = NextIndexBoss ();

		yield return new WaitForSeconds (spawnIntervale);

		if (enemiesInGame <= maxEnemies) {
			Instantiate (prefabsEnemigos [0], spawnPointsBoss [randIndex].transform.position, Quaternion.identity);
			enemiesInGame++;
		}

		yield return StartCoroutine (instantiateEnemiesBoss ());
	}

	/// <summary>
	/// Instantiates the enemies in normal area.
	/// </summary>
	IEnumerator InstantiateEnemiesNormal ()
	{
		if (!_inBoss && this.enabled) {

			int randIndex = 0;
			randIndex = NextIndexNormal ();
			yield return new WaitForSeconds (spawnIntervale);
			if (enemiesInGame <= maxEnemies) {

				Instantiate (prefabsEnemigos [0], spawnPointsNormal [randIndex].transform.position, Quaternion.identity);
				enemiesInGame++;
			}

			StartCoroutine (InstantiateEnemiesNormal ());
		} else if (this.enabled) {
			spawnIntervale = 3;
			StartCoroutine (instantiateEnemiesBoss ());
		}
	}

	#endregion
}
