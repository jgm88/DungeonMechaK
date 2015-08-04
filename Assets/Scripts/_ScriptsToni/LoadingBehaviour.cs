using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBehaviour : MonoBehaviour 
{
	public Text ProgressText;
	//public Image ProgressBar;

	//private RectTransform _mask;


	void Awake ()
	{
		//_mask = ProgressBar.GetComponent<Mask> ().rectTransform;
	}

	// Use this for initialization
	void Start () 
	{
		//StartCoroutine (CO_StartLoadLevelAsync ());
		Application.LoadLevel (AppDelegate.Instance.NextLevelToLoad);
	}

	/// <summary>
	/// Starts loading the level asynchroniously.
	/// </summary>
	private IEnumerator CO_StartLoadLevelAsync ()
	{
		AsyncOperation loader = Application.LoadLevelAsync (AppDelegate.Instance.NextLevelToLoad);

		while (!loader.isDone)
		{
			// Move the progress bar
			//_mask.anchorMax = new Vector2 (loader.progress, _mask.anchorMax.y);

			// Show percent progress on text
			ProgressText.text = string.Format ("{0:00} %", (loader.progress * 100.0f));

			// Wait for next frame
			yield return null;
		}

		yield return null;
	}
}
