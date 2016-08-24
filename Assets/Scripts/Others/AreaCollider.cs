using UnityEngine;
using System.Collections;



public class AreaCollider : MonoBehaviour {
	
	public float radiusMax =4f;
	//fuerza de impulso
	public int force = 10;
	private SphereCollider myCollider;
	
	void Start () {
		myCollider = transform.GetComponent<SphereCollider>();
		myCollider.radius = radiusMax;
	}
	void Update () {
		//GrowRadius();
	}
	void OnTriggerEnter (Collider other) {
		if(other.tag=="Enemy"){
			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;
 			if (other.GetComponent<Rigidbody>()){
     			 other.GetComponent<Rigidbody>().AddForce(dir.normalized * force);
			} 
			else { // use a special script for character controllers:
				other.gameObject.SendMessage("AddImpact",dir.normalized * force,SendMessageOptions.DontRequireReceiver);
    		}
			//DPS
			//c.gameObject.SetActive(false);
		}
	}
	
	void GrowRadius(){
		myCollider.radius++;
	}
}
