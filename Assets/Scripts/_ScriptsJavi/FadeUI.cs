using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUI : MonoBehaviour {
	
	private Animator _animator;
 
	void Start () {
		_animator = gameObject.GetComponent<Animator>();
		if(_animator)
			Debug.Log("Recojo el animator de " + gameObject.name);
		_animator.SetBool("Fade", false);
	}

	public void FadeOut(){

		_animator.SetBool("Fade", true);
	}

	public void FadeIn(){
		_animator.SetBool("Fade", false);
	}
}
