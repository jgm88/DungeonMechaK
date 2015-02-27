using UnityEngine;
using System.Collections;

public class ImpactPlayer : MonoBehaviour {

	public int force = 10;
	public int dps = 5;
	public GameObject pinchos;


	void OnTriggerEnter (Collider other) 
	{
		if(other.tag=="Player")
		{
			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;

			#region OLD CODE
// 			if (other.rigidbody)
//			{
//     			 other.rigidbody.AddForce(dir.normalized * force);
//			} 
//			else 
//			{ 
//				// use a special script for character controllers:
//				other.gameObject.SendMessage("AddImpact",dir.normalized * force,SendMessageOptions.DontRequireReceiver);
//    		}

			//DPS
			//c.gameObject.SetActive(false);
			#endregion

			// Aplicar daño y fuerza
			other.GetComponent<PlayerBehaviour> ().ReceiveDamage (dps);
			other.GetComponent<ImpactReceiver> ().AddImpact (dir.normalized * force);

			// Animación del pincho
			audio.Play();
			iTweenEvent.GetEvent(pinchos, "activarTrampa").Play();
			iTweenEvent.GetEvent(pinchos, "esconderPinchos").Play();
		}
	}
}
