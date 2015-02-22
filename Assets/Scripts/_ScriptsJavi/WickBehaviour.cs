using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WickBehaviour : MonoBehaviour {

	public string color;
	public bool adquired = false;
	public int maxReload = 5;
	public float coolDownSeconds = 5f;
	public float rotationVelocity = 5f;
	public int currentReload;

	private ChargeBehaviour _chargeBehaviour;
	private GameObject _wickText;
	private bool _inCoolDown = false;
	private BoosDoorBehaviour _bossDoorBehaviour;
	private GameObject _sparks;
	private GameObject _smoke;
	private Text[] vText;

	void Start () {

		currentReload = maxReload;

		_sparks = transform.Find("Sparks").gameObject;
		_smoke = transform.Find("Smoke").gameObject;
		_bossDoorBehaviour = GameObject.Find("BossDoor").GetComponent<BoosDoorBehaviour>();
		_chargeBehaviour = GameObject.Find("Player").GetComponent<ChargeBehaviour>();
		_wickText = GameObject.Find("WickText");
		vText =_wickText.GetComponentsInChildren<Text>();	

		setActiveWickText(false);
		_smoke.SetActive(false);
	}
	void LateUpdate(){
		transform.Rotate(0f,rotationVelocity,0f);
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(currentReload > 0){

				_chargeBehaviour.inWickArea = true;
				_chargeBehaviour.wickBehaviour = this;

			}
			else{
				_chargeBehaviour.inWickArea = false;
				_chargeBehaviour.wickBehaviour = null;
				setActiveWickText(false);
				if(!adquired){
					adquired = true;
					_chargeBehaviour.transform.gameObject.audio.PlayOneShot();
					_bossDoorBehaviour.purchaseWick(color);
				}
				_sparks.SetActive(false);
				_smoke.SetActive(true);
			}
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player" && currentReload > 0){
			setActiveWickText(true);
		}
	}
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			setActiveWickText(false);
			_chargeBehaviour.inWickArea = false;
			_chargeBehaviour.wickBehaviour = null;
			//Debemos controlar que no se haya lanzado la corrutina para no lanzar mas de una
			if(!_inCoolDown){
				_inCoolDown = true;
				StartCoroutine(CoolDownTocharge());
			}
		}
	}
	IEnumerator CoolDownTocharge(){
		yield return new WaitForSeconds(coolDownSeconds);
		currentReload = maxReload;
		_sparks.SetActive(true);
		_smoke.SetActive(false);
		_inCoolDown = false;
	}
	void setActiveWickText(bool isEnable){
		foreach(Text t in vText){
			t.enabled = isEnable;
		}
	}
}
