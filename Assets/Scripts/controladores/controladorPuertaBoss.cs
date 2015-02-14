using UnityEngine;
using System.Collections;

public class controladorPuertaBoss : MonoBehaviour {
	
	public GameObject puerta1;
	public GameObject puerta2;
	public GameObject bloqueoPuerta;
	public GameObject spawner;
	public GameObject torch;
	public GameObject boss;
	public AudioClip pistaBoss;
	private controladorAtaqueCC ataqueCC;
	
	void Start () {
		ataqueCC = GameObject.FindWithTag("Player").GetComponent<controladorAtaqueCC>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			
			spawner.SendMessage("destriurEnemigosFueraBoss", SendMessageOptions.DontRequireReceiver);
			ataqueCC.SendMessage("cambiarArmaActiva", "DoubleAxe",SendMessageOptions.DontRequireReceiver);
			boss.SetActive(true);
			RenderSettings.fogDensity = 0.05f;
			puerta1.SetActive(true);
			puerta2.SetActive(true);
			bloqueoPuerta.SetActive(true);
			torch.GetComponent<ligthLife>().enBossOn();
			
			//mando ademas un mensaje al spawner para que spawneee en el boss
			spawner.SendMessage("setInBoss",SendMessageOptions.DontRequireReceiver);
			Camera.main.audio.Stop();
			Camera.main.audio.clip = pistaBoss;
			Camera.main.audio.Play();
			Destroy(this.gameObject);
		}
	}
}
