using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetPowerize : MonoBehaviour
{
	public int stateActive;
	public string power;
	public GameObject thunder;
	public Color powerColor;
	public List<GameObject> otherThunders;
	private ChargeBehaviour permitRestoreMana;
	private BossBehaviour bossPhase;
	private AttackPlayerBehaviour attackController;
	private GameObject bloomLight;
	// Use this for initialization
	void Awake ()
	{
		bloomLight = transform.FindChild("Bloom02").gameObject;
		bossPhase = GameObject.FindWithTag("Boss").GetComponent<BossBehaviour>();
	}
	void Start ()
	{
		attackController = GameObject.FindWithTag ("Player").GetComponent<AttackPlayerBehaviour> ();
		permitRestoreMana = GameObject.FindWithTag ("Player").GetComponent<ChargeBehaviour> ();

	}
	void LateUpdate()
	{
		if(stateActive == bossPhase.currentState)
		{
			thunder.SetActive (true);
			bloomLight.SetActive (true);
			bloomLight.GetComponent<MeshRenderer>().material.SetColor("_TintColor",powerColor);
//			bloomMat.color = powerColor;
		}
		else
		{
			thunder.SetActive (false);
			bloomLight.SetActive (false);
		}

	}
	void OnTriggerStay (Collider other)
	{
		if(bossPhase.currentState == stateActive )
		{
			if (other.tag == "Player") {
				if (thunder.activeSelf) {
					permitRestoreMana.inWickArea = true;
					permitRestoreMana.letPowerize = this;
					attackController.setActualPower (power);
					foreach (Transform child in thunder.transform) {
						child.gameObject.SetActive (true);
					}
					foreach (GameObject otherThunder in otherThunders) {
						otherThunder.SetActive (false);
					}
				}
				// TODO quitar de aqui, se comprueba en skill behaviour
//				if (Input.GetKeyDown (KeyCode.Alpha1)) {
//					if (!thunder.activeSelf || thunder.name == "hellSparkYellow") {
//						//					GetComponent<manejadorAudioAnimado> ().reproducirEspecial ();
//						
//					}
//				}
			}

		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			permitRestoreMana.letPowerize = null;
			permitRestoreMana.inWickArea = false;
		}
	}
}
