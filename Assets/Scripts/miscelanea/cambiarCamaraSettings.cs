using UnityEngine;
using System.Collections;

public class cambiarCamaraSettings : MonoBehaviour {

    private Material noSkybox;

	private bool vivo;
	public GameObject guitextMuerte;
 	private GameObject player;
    void Start () {
		player = GameObject.FindWithTag("Player").gameObject;
	    //Set the background color
	    //camera.backgroundColor = new Color(164f, 0 ,0);
    }
 
	void FixedUpdate(){
	}
	
    void LateUpdate () {
		vivo = player.GetComponent<PlayerBehaviour>().isVivo();
        if (!vivo)
        {
			camera.backgroundColor = new Color(164f, 0 ,0);
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.red;
            RenderSettings.fogDensity = 1f;
            RenderSettings.skybox = noSkybox;
			RenderSettings.fogStartDistance = -20f;
			RenderSettings.fogEndDistance = 10f;
        }
    }
}
