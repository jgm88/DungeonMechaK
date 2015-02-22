using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetPowerize : MonoBehaviour
{
	
	public GUIText guitextPower;
	private controladorAtaqueCC controladorAtaque;
	public string power;
	public GameObject thunder;
	public List<GameObject> otherThunders;
	private SkillsBehaviour permitRestoreMana;
	
	// Use this for initialization
	void Start ()
	{
		controladorAtaque = GameObject.FindWithTag ("Player").GetComponent<controladorAtaqueCC> ();
		permitRestoreMana = GameObject.Find ("SkillsPanel").GetComponent<SkillsBehaviour> ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") {
			guitextPower.enabled = true;
			if (thunder.activeSelf) {
				permitRestoreMana.inWickArea = true;
			}

			if (Input.GetKeyDown (KeyCode.F)) {
				if (!thunder.activeSelf || thunder.name == "hellSparkYellow") {
					controladorAtaque.setPoderActual (power);
					foreach (Transform child in thunder.transform) {
						child.gameObject.SetActive (true);
					}
					thunder.SetActive (true);
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
			guitextPower.enabled = false;
			permitRestoreMana.inWickArea = false;
		}
	}
}
