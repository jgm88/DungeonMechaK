using UnityEngine;
using System.Collections;

public class pickupKey : MonoBehaviour {
	
	private controladorPickupsKey controladorPickups;
	public GameObject flama;
	// Use this for initialization
	void Start () {
		controladorPickups = GameObject.FindWithTag("Player").GetComponent<controladorPickupsKey>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			controladorPickups.recogerLlave();
			flama.SetActive(true);
			Destroy(this.gameObject);
		}
	}
}
