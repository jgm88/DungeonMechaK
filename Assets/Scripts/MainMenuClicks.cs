using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuClicks : MonoBehaviour 
{
	public Button JuegaTerapiaButton;
	public Button DMKButton;
	public Button PlayAndCareButton;

	void Start () 
	{
		// JuegaTerapia
		JuegaTerapiaButton.onClick.AddListener (() => { ClickJuegaTerapia (); });

		// DMK
		DMKButton.onClick.AddListener (() => { ClickDMK (); });

		// PlayAndCare
		PlayAndCareButton.onClick.AddListener (() => { ClickPlayAndCare (); });
	}
	
	public void ClickJuegaTerapia ()
	{
		Debug.Log ("JuegaTerapiaButton click");
		Application.OpenURL ("http://www.juegaterapia.org/");
	}
	
	public void ClickDMK ()
	{
		Debug.Log ("DMKButton click");
		Application.OpenURL ("https://www.facebook.com/demekateam");
	}
	
	public void ClickPlayAndCare ()
	{
		Debug.Log ("PlayAndCareButton click");
		//Application.OpenURL ("asdf");
	}
}
