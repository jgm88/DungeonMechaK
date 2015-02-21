using UnityEngine;
using System.Collections;

public class InitBossBehaviour : MonoBehaviour
{

	private GameObject _boss;
	private BoosDoorBehaviour _bossDoorBehaviour;
	public GameObject bossThunder;
//	private EnemySpawnerBehaviour _enemySpawnerBehaviour;

	public AudioClip music;
	
	void Awake ()
	{
		_boss = GameObject.Find ("Boss");
		_boss.SetActive (false);


	}
	// Use this for initialization
	void Start ()
	{
	
		_bossDoorBehaviour = transform.parent.GetComponent<BoosDoorBehaviour> ();	
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			RenderSettings.fogDensity = 0.05f;

			//TODO Usar Getcomponent
//			spawner.SendMessage("destriurEnemigosFueraBoss", SendMessageOptions.DontRequireReceiver);

			bossThunder.SetActive (true);
			_bossDoorBehaviour.setDoor (true);
			float duration = bossThunder.GetComponent<ParticleSystem> ().duration;
			StartCoroutine (COSpawnBoss (duration));

			//TODO Cambiar por algo que no este en la torch...
//			torch.GetComponent<ligthLife>().enBossOn();
			
//			mando ademas un mensaje al spawner para que spawneee en el boss
//			spawner.SendMessage("setInBoss",SendMessageOptions.DontRequireReceiver);
			//TODO Cambiar por esto
			//			_enemySpawnerBehaviour.setInBoss();
			Camera.main.audio.Stop ();
			Camera.main.audio.clip = music;
			Camera.main.audio.Play ();
			//
		}
	}

	IEnumerator COSpawnBoss (float duration)
	{
		this.collider.enabled = false;
		yield return new WaitForSeconds (duration);
		_boss.SetActive (true);
		Destroy (bossThunder);
		Destroy (this.gameObject);
	}
}
