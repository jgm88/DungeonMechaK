using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject bulletPrefab;
	public bool canShot;	
	public float coolDown = 2f;
	public int manaCost = 20;

	private PlayerBehaviour _playerBehaviour;
	private Image _maskSkill;
	public Image maskSkill{ set{ _maskSkill = value;} get {return _maskSkill; }}

	void Awake () {
		_playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		canShot = true;

	}
	public void Shoot(){
		if(canShot){

			_playerBehaviour.DeductMana(manaCost);
			Instantiate(bulletPrefab, transform.position, transform.rotation);
			canShot = false;
			StartCoroutine(CoVisualCoolDown());				
		}
	}
	IEnumerator CoVisualCoolDown(){
		maskSkill.fillAmount = 1;
		for (int i = 0; i < 50; ++i) {
			maskSkill.fillAmount -= 0.02f;
			yield return new WaitForSeconds(coolDown * 0.02f);
		}
		canShot = true;
	}
}
