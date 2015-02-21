using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class permitirPoder : MonoBehaviour
{
	
	public GUIText guitextPoder;
	private controladorAtaqueCC controladorAtaque;
	public string poder;
	public GameObject thunder;
	public List<GameObject> otherThunders;
	private bool _thunderActive = false;
	private SkillsBehaviour permitRestoreMana;
	
	// Use this for initialization
	void Start ()
	{
		controladorAtaque = GameObject.FindWithTag ("Player").GetComponent<controladorAtaqueCC> ();
		permitRestoreMana = GameObject.Find ("SkillsPanel").GetComponent<SkillsBehaviour> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") {
			if (_thunderActive) {
				guitextPoder.enabled = true;
				permitRestoreMana.inWickArea = true;
			}

			if (Input.GetKeyDown (KeyCode.F)) {
				if (!_thunderActive) {
					controladorAtaque.setPoderActual (poder);
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
			guitextPoder.enabled = false;
			permitRestoreMana.inWickArea = false;
		}
	}
}
