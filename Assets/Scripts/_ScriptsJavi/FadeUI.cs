using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUI : MonoBehaviour {
	
	private Animator _animator;
 
	void Awake()
	{
		_animator = gameObject.GetComponent<Animator>();

	}
	void Start () {
	
		_animator.SetBool("Fade", false);
	}

	public void FadeOut(){

		_animator.SetBool("Fade", true);
	}

	public void FadeIn(){
		_animator.SetBool("Fade", false);
	}
}
