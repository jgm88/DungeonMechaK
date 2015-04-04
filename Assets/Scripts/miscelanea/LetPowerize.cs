using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class LetPowerize : MonoBehaviour
{
	public int stateActive;
	public string power;
	public GameObject thunder;
	public Color powerColor;
	public GameObject boss;
	public List<GameObject> otherThunders;
	private ChargeBehaviour permitRestoreMana;
	private BossBehaviour bossPhase;
	private AttackPlayerBehaviour attackController;
	private GameObject bloomLight;

	private Text textW,textB;
	// Use this for initialization
	void Awake ()
	{
		textW = GameObject.Find("WickTextWhite").GetComponent<Text>();
		textB = GameObject.Find("WickTextBlack").GetComponent<Text>();
		bloomLight = transform.FindChild ("Bloom02").gameObject;
		bossPhase = boss.GetComponent<BossBehaviour> ();
	}
	void Start ()
	{
		attackController = GameObject.FindWithTag ("Player").GetComponent<AttackPlayerBehaviour> ();
		permitRestoreMana = GameObject.FindWithTag ("Player").GetComponent<ChargeBehaviour> ();

	}
	void LateUpdate ()
	{
		if (stateActive == bossPhase.currentState) {
			thunder.SetActive (true);
			bloomLight.SetActive (true);
			bloomLight.GetComponent<MeshRenderer> ().material.SetColor ("_TintColor", powerColor);
//			bloomMat.color = powerColor;
		} else {
			thunder.SetActive (false);
			bloomLight.SetActive (false);
		}

	}
	void OnTriggerStay (Collider other)
	{
		if (bossPhase.currentState == stateActive) {
			if (other.tag == "Player") {
				textB.enabled = textW.enabled = true;
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
//					if (!_soundLaunched) {
//						GetComponent<manejadorAudioAnimado> ().reproducirEspecial ();
//						_soundLaunched = true;
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
			textB.enabled = textW.enabled = false;
		}
	}
}
	