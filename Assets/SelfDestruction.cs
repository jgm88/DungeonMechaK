using UnityEngine;
using System.Collections;

public class SelfDestruction : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		StartCoroutine(CODie());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator CODie()
	{
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
