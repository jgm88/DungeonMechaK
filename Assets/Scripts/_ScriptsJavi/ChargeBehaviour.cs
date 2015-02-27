using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChargeBehaviour : MonoBehaviour
{

	private WickBehaviour _wickBehaviour;
	private Image _maskSkill;
	private PlayerBehaviour _playerBehaviour;
	private LetPowerize _letPowerize;
	private GameObject _currentFlame;

	public List<GameObject> colorFlames;
	public int manaChargeAmount = 20;
	public bool canCharge = true;
	public float coolDown = 0.1f;
	public bool inWickArea = false;
	public WickBehaviour wickBehaviour{ set { _wickBehaviour = value; } get { return _wickBehaviour; } }
	public LetPowerize letPowerize{ set { _letPowerize = value; } get { return _letPowerize; } }
	public Image maskSkill{ set { _maskSkill = value; } get { return _maskSkill; } }
	public GameObject visualEffect;
	public Transform torchTransform;
	// Use this for initialization
	void Start ()
	{
		_currentFlame = colorFlames[0];
		foreach(var flame in colorFlames)
			flame.SetActive(false);
		_currentFlame.SetActive(true);
		_playerBehaviour = GameObject.FindWithTag ("Player").GetComponent<PlayerBehaviour> ();

	}

	public void Charge ()
	{
		if (canCharge && _wickBehaviour) {
			_playerBehaviour.ReceiveMana (manaChargeAmount);
			canCharge = false;
			_wickBehaviour.currentReload--;
			Instantiate(visualEffect, torchTransform.position, torchTransform.rotation);
			StartCoroutine (CoVisualCoolDown ());
		} else if (canCharge && _letPowerize) {
			activeFlame(_letPowerize.stateActive);
			_playerBehaviour.ReceiveMana (manaChargeAmount);
			canCharge = false;
			Instantiate(visualEffect, torchTransform.position, torchTransform.rotation);
			StartCoroutine (CoVisualCoolDown ());
		}
	}
	private void activeFlame(int state)
	{
		_currentFlame.SetActive (false);
		_currentFlame = colorFlames[state];
		_currentFlame.SetActive(true);
	}

	IEnumerator CoVisualCoolDown ()
	{
		maskSkill.fillAmount = 1;
		for (int i = 0; i < 10; ++i) {
			maskSkill.fillAmount -= 0.1f;
			yield return new WaitForSeconds (coolDown * 0.1f);
		}
		canCharge = true;
	}
}
