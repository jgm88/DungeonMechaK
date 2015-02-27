using UnityEngine;
using System.Collections;

public class InitBossBehaviour : MonoBehaviour
{

	public GameObject _boss;
	private BoosDoorBehaviour _bossDoorBehaviour;
	public GameObject bossThunder;
	public GameObject firstStatueAura;
	public AudioClip music;
	private GameObject indicationUI;
//	void Awake ()
//	{
//
//
//	}
	// Use this for initialization
	void Start ()
	{
		indicationUI = GameObject.Find("BossIndication");
//		_boss = GameObject.FindWithTag ("Boss");
		_boss.SetActive (false);
		_bossDoorBehaviour = transform.parent.GetComponent<BoosDoorBehaviour> ();	
		indicationUI.SetActive(false);
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			RenderSettings.fogDensity = 0.05f;
			indicationUI.SetActive (true);
			bossThunder.SetActive (true);
			_bossDoorBehaviour.setDoor (true);
			float duration = bossThunder.GetComponent<ParticleSystem> ().duration;
			StartCoroutine (COSpawnBoss (duration));

			Camera.main.audio.Stop ();
			Camera.main.audio.clip = music;
			Camera.main.audio.Play ();
			firstStatueAura.SetActive (true);
			GameObject.Find("StartBattle").GetComponent<AudioSource>().Play();
		}
	}
	
	/// <summary>
	/// Coroutine that starts the spawns in boss area, events and destroys outside area enemies and some objects.
	/// </summary>
	/// <returns>The spawn boss.</returns>
	/// <param name="duration">Duration.</param>
	IEnumerator COSpawnBoss (float duration)
	{
		this.collider.enabled = false;
		//establecemos el spawner a la sala del boss
		controladorSpawn SpawnController = GameObject.Find ("EnemySpawns").GetComponent<controladorSpawn> ();
		SpawnController.IsInBoss (true);
		SpawnController.DestroyEnemiesOutsideBoss ();
		yield return new WaitForSeconds (duration);
		indicationUI.SetActive(false);
		_boss.SetActive (true);
		Destroy (bossThunder);
		Destroy (this.gameObject);
	}
}
