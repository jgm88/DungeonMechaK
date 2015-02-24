using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetPowerize : MonoBehaviour
{
	
	public GUIText guitextPower;
	private AttackPlayerBehaviour attackController;
	public string power;
	public GameObject thunder;
	public List<GameObject> otherThunders;
	private ChargeBehaviour permitRestoreMana;
	
	// Use this for initialization
	void Start ()
	{
		attackController = GameObject.FindWithTag ("Player").GetComponent<AttackPlayerBehaviour> ();
		permitRestoreMana = GameObject.FindWithTag ("Player").GetComponent<ChargeBehaviour> ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") {
			guitextPower.enabled = true;
			if (thunder.activeSelf) {
				permitRestoreMana.inWickArea = true;
				permitRestoreMana.letPowerize = this;
			}
			// TODO quitar de aqui, se comprueba en skill behaviour
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if (!thunder.activeSelf || thunder.name == "hellSparkYellow") {
					GetComponent<manejadorAudioAnimado> ().reproducirEspecial ();
					attackController.setActualPower (power);
					thunder.SetActive (true);
					foreach (Transform child in thunder.transform) {
						child.gameObject.SetActive (true);
					}
					foreach (GameObject otherThunder in otherThunders) {
						otherThunder.SetActive (false);
					}
				}
			}
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			permitRestoreMana.letPowerize = null;
			guitextPower.enabled = false;
			permitRestoreMana.inWickArea = false;
		}
	}
}
