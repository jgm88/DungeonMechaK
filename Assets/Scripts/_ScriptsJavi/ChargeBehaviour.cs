using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChargeBehaviour : MonoBehaviour {

	private WickBehaviour _wickBehaviour;
	private Image _maskSkill;
	private PlayerBehaviour _playerBehaviour;
	private LetPowerize _letPowerize;
	public int manaChargeAmount = 20;
	public bool canCharge = true;
	public float coolDown = 0.1f;
	public bool inWickArea = false;
//	public GameObject visualEffect;
	public WickBehaviour wickBehaviour{ set { _wickBehaviour = value;} get { return _wickBehaviour;}}
	public LetPowerize letPowerize{ set { _letPowerize = value;} get { return _letPowerize;}}
	public Image maskSkill{ set{ _maskSkill = value;} get {return _maskSkill; }}

	// Use this for initialization
	void Start () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();

	}

	public void Charge(){
		if(canCharge && _wickBehaviour){
			_playerBehaviour.ReceiveMana(manaChargeAmount);
			canCharge = false;
			_wickBehaviour.currentReload--;
			StartCoroutine(CoVisualCoolDown());
		}
		else if(canCharge && _letPowerize)
		{
			_playerBehaviour.ReceiveMana(manaChargeAmount);
			canCharge = false;
			StartCoroutine(CoVisualCoolDown());
		}
	}

	IEnumerator CoVisualCoolDown(){
		maskSkill.fillAmount = 1;
		for (int i = 0; i < 10; ++i) {
			maskSkill.fillAmount -= 0.1f;
			yield return new WaitForSeconds(coolDown * 0.1f);
		}
		canCharge = true;
	}
}
