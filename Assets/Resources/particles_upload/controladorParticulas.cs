using UnityEngine;
using System.Collections;

public class controladorParticulas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		startParticles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void startParticles(){
		this.particleEmitter.maxSize = 1;
		StartCoroutine(COStartParticles());
	}
	
	IEnumerator COStartParticles(){
		yield return new WaitForSeconds(3);
		this.particleEmitter.maxSize = 0;
	}
}
