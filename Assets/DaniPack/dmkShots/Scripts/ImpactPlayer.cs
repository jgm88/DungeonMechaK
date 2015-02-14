using UnityEngine;
using System.Collections;

public class ImpactPlayer : MonoBehaviour {

	public int force = 10;
	public float dps = 5f;
	public GameObject pinchos;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Player"){
			//audio.Play();
			//iTweenEvent.GetEvent(pinchos, "activarTrampa").Play();
			//iTweenEvent.GetEvent(pinchos, "esconderPinchos").Play();
			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;
 			if (other.rigidbody){
     			 other.rigidbody.AddForce(dir.normalized * force);
			} 
			else { // use a special script for character controllers:
				other.gameObject.SendMessage("AddImpact",dir.normalized * force,SendMessageOptions.DontRequireReceiver);
    		}
			other.gameObject.SendMessage("restarVida",dps,SendMessageOptions.DontRequireReceiver);
			//DPS
			//c.gameObject.SetActive(false);
		}
	}
}
