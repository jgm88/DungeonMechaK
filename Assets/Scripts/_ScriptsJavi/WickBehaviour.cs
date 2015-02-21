using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WickBehaviour : MonoBehaviour {

	public string color;
	public bool adquired = false;
	public int maxReload = 30;
	public float coolDownSeconds = 5f;
	public float rotationVelocity = 5f;

	private int _currentReload;
	private SkillsBehaviour _skillsBehaviour;
	private Text _wickText;
	private bool _inCoolDown = false;
	private BoosDoorBehaviour _bossDoorBehaviour;
	private GameObject _sparks;

	void Start () {

		_currentReload = maxReload;

		_sparks = transform.Find("Sparks").gameObject;
		_bossDoorBehaviour = GameObject.Find("BossDoor").GetComponent<BoosDoorBehaviour>();
		_skillsBehaviour = GameObject.Find("SkillsPanel").GetComponent<SkillsBehaviour>();
		_wickText = GameObject.Find("WickText").GetComponent<Text>();
		_wickText.enabled = false;
		
	}
	void LateUpdate(){
		transform.Rotate(0f,rotationVelocity,0f);
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(_currentReload > 0){

				_skillsBehaviour.inWickArea = true;
				if (Input.GetKeyDown(KeyCode.Alpha1)){
				    --_currentReload;
				}
			}
			else{
				_skillsBehaviour.inWickArea = false;
				_wickText.enabled = false;
				if(!adquired){
					adquired = true;
					_bossDoorBehaviour.purchaseWick(color);
				}
				_sparks.SetActive(false);
			}
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player" && _currentReload > 0){
			_wickText.enabled = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			_wickText.enabled = false;
			_skillsBehaviour.inWickArea = false;
			//Debemos controlar que no se haya lanzado la corrutina para no lanzar mas de una
			if(!_inCoolDown){
				_inCoolDown = true;
				StartCoroutine(CoolDownTocharge());
			}
		}
	}
	IEnumerator CoolDownTocharge(){
		yield return new WaitForSeconds(coolDownSeconds);
		_currentReload = maxReload;
		_sparks.SetActive(true);
		_inCoolDown = false;
	}
}
