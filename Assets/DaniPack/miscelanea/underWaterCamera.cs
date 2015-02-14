using UnityEngine;
using System.Collections;
 
public class underWaterCamera : MonoBehaviour {
 
	//This script enables underwater effects. Attach to main camera.
 
    //Define variable
    public Transform underwaterLevel;
 
    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;
	private float linearFogStart;
	private float nivelAgua;
 
    void Start () {
	    //Set the background color
	    camera.backgroundColor = new Color(0, 0.4f, 0.7f, 1);
		defaultFog = RenderSettings.fog;
		defaultFogColor = RenderSettings.fogColor;
		defaultFogDensity = RenderSettings.fogDensity;
		defaultSkybox = RenderSettings.skybox;
		linearFogStart = RenderSettings.fogStartDistance;
		nivelAgua = underwaterLevel.position.y;
    }
 
	void FixedUpdate(){
		nivelAgua = underwaterLevel.position.y;
	}
	
    void Update () {
        if (transform.position.y < nivelAgua)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 1f;
            RenderSettings.skybox = noSkybox;
			RenderSettings.fogStartDistance = -20f;
			RenderSettings.fogEndDistance = 10f;
        }
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
			RenderSettings.fogStartDistance = linearFogStart;
			RenderSettings.fogEndDistance = 100;
        }
    }
}