using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour {

	public float lifeTime = 0.5f;
	public bool hasAudio = false;
	
	private float _spawnTime = 0.0f;
	
	void OnEnable(){
		_spawnTime = Time.time;
		if(hasAudio)
			audio.Play();
	}
	
	void Update () {
		
		if(Time.time > _spawnTime + lifeTime){
			GameObject.Destroy(gameObject);
		}
	}
}
