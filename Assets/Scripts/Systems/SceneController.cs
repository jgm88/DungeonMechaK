using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		RenderSettings.fog=true;
		RenderSettings.fogDensity = 0.2f;
		RenderSettings.fogColor = Color.black;
		Screen.lockCursor=true;
	}	
	void Update () {
		
	}
}
