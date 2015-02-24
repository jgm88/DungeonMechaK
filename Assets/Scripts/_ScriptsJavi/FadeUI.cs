using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUI : MonoBehaviour {
	
	private Image _image;
	private Color _colorAux;

	public float fadeTime = 0.5f;
 
	void Start () {
		_image = gameObject.GetComponent<Image>();
	}

	public void FadeOut(){
		_image.CrossFadeAlpha(0,fadeTime, false);
	}

	public void FadeIn(){
		StartCoroutine(CoFadeIn());
//		_colorAux = _image.color;
//		_colorAux.a = 0f;
//		_image.color = _colorAux;
//		_image.CrossFadeAlpha(255,fadeTime, false);
	}
	IEnumerator CoFadeIn(){
		for(int i = 0 ; i <= 255 ; ++i){
			_colorAux = _image.color;
			_colorAux.a = i;
			_image.color = _colorAux;
//			yield return new WaitForSeconds(fadeTime*0.01f);
		}
		yield return null;

	}
}
