using UnityEngine;
using System.Collections;

public class controladorAtaqueCCEnemy : MonoBehaviour {
	
	private bool atacando = false;
	private GameObject player;
	public float enemyDamage = 5f;
	public float cadenciaAtaque = 3f;
	private manejadorVida vidaPlayer;
	public float force=20f;
	public GameObject padre;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		vidaPlayer = player.GetComponent<manejadorVida>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		if(!atacando && other.tag == "Player" && vidaPlayer.isVivo()){
			atacar ();
			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;
 			if (other.rigidbody){
     			 other.rigidbody.AddForce(dir.normalized * force);
			} 
			else { 
				other.gameObject.SendMessage("AddImpact",dir.normalized * force,SendMessageOptions.DontRequireReceiver);
    		}
		}
	}
	
	//funcion para atacar, bloquea el spam de ataques y envia mensajes
	private void atacar(){
		atacando = true;
		//envio mensajes
		if(padre!=null){
			padre.GetComponent<LoadAnimIA>().attack();
		}
		this.transform.parent.gameObject.SendMessage("reproducirAtacar", 0.2f, SendMessageOptions.DontRequireReceiver);
		player.SendMessage("restarVida", enemyDamage,SendMessageOptions.DontRequireReceiver);	
		player.SendMessage("reproducirGolpeado", SendMessageOptions.DontRequireReceiver);
		player.SendMessage("reproducirImpacto", SendMessageOptions.DontRequireReceiver);
		StartCoroutine(COAtacar());
	}
	//coroutina que bloquea el spam de ataques al tiempo deseado
	IEnumerator COAtacar(){
		yield return new WaitForSeconds(cadenciaAtaque);
		atacando = false;
	}
}
