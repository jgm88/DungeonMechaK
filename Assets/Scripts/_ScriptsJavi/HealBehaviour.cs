using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealBehaviour : MonoBehaviour {

	private PlayerBehaviour _playerBehaviour;
	private Vector3 _positionAux;
	private GameObject _gAux;
	private Image _maskSkill;

	public bool canHeal = true;
	public int healAmount = 10;
	public int manaCost = 10;
	public float coolDown = 3f;
	public GameObject healEffect;

	public Image maskSkill{ set{ _maskSkill = value;} get {return _maskSkill; }}


	// Use this for initialization
	void Start () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	public void Heal(){

		if(canHeal){
			_playerBehaviour.ReceiveHeal(healAmount);
			_playerBehaviour.DeductMana(manaCost);
			_positionAux = Camera.main.transform.position;
			_positionAux.y -= 0.5f;
			
			_gAux = Instantiate(healEffect,_positionAux, Camera.main.transform.rotation) as GameObject;
			_gAux.transform.parent = Camera.main.transform;
//			StartCoroutine(CoDestoyParticles(_gAux));
			canHeal = false;
			StartCoroutine(CoVisualCoolDown());
		}
	}

//	IEnumerator CoDestoyParticles(GameObject particles){
//		yield return new WaitForSeconds(3f);
//		Destroy(particles);
//	}
	IEnumerator CoVisualCoolDown(){
		maskSkill.fillAmount = 1;
		for (int i = 0; i < 50; ++i) {
			maskSkill.fillAmount -= 0.02f;
			yield return new WaitForSeconds(coolDown * 0.02f);
		}
		canHeal = true;
	}
}
