using UnityEngine;
using System.Collections;

public class MenuAnimatorController : MonoBehaviour {

	private Animator _animator;
	private AudioSource _audio;

	public bool showOnStart = false;
	
	void Start () {

		_animator = GetComponent<Animator>();
		_audio = GameObject.Find ("AudioClick").audio;

		gameObject.SetActive(showOnStart);
		if(showOnStart){
			Show();
		}
		else{
			gameObject.SetActive(false);
		}
	
	}
	public void Show(){
		gameObject.SetActive(true);
		_animator.SetBool("Show", true);
	}
	public void Hide(){
		_animator.SetBool("Show", false);
		_audio.Play();
	}
}
