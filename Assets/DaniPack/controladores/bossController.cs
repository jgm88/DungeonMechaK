using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (manejadorAudioAnimado))]
public class bossController : MonoBehaviour {
	
	public float vidaFase = 200;
	private int numeroFases = 4;
	public float vidaFinal = 500;
	public float damageFasesIniciales = 20;
	public float damageFaseFinal = 25;
	public float cadenciaAtaque = 5f;
	private bool atacando = false;
	private GameObject player;
	private float damageActual;
	private float vidaActual;
	private int faseActual=0;
	private manejadorAudioAnimado manejadorAudio;
	private Color colorOriginal;
	private List<Color> coloresFase = new List<Color>();
	private controladorAtaqueCC controladorAtaquePj;
	private List<string> listaPoderes = new List<string>();
	private string debilidadPoder;
	public ParticleSystem particulas;
	public GameObject areaAtaque;
	public float tiempoStunDinamita = 3f;
	public GameObject particulasMuerte;
	private GameObject guitextVictoria;
	private GameObject stunSprite;
	
	// Use this for initialization
	void Start () {
		
		stunSprite=transform.FindChild("Stun").gameObject;
		stunSprite.SetActive(false);
		guitextVictoria = GameObject.Find("victoria");
		player = GameObject.FindWithTag("Player");
		damageActual = damageFasesIniciales;
		vidaActual = vidaFase;
		manejadorAudio = GetComponent<manejadorAudioAnimado>();
		agregarColoresYPoderes();
		controladorAtaquePj = player.GetComponent<controladorAtaqueCC>();
		debilidadPoder = listaPoderes[faseActual];
	}
	
	//agrega los colores de cada fase
	private void agregarColoresYPoderes(){
		coloresFase.Add(colorOriginal);
		coloresFase.Add(Color.green);
		coloresFase.Add(Color.red);
		coloresFase.Add(Color.blue);
		coloresFase.Add(Color.white);
		listaPoderes.Add("original");
		listaPoderes.Add("green");
		listaPoderes.Add("red");
		listaPoderes.Add("blue");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//funcion que realiza las acciones necesarias al morir
	private void muerteBoss(){
		Instantiate(particulasMuerte, transform.position, Quaternion.identity);
		
		//lanzo el evento de del itween y desactivo el conrol
		iTweenEvent.GetEvent(Camera.main.gameObject, "recorridoEndGame").Play();
		player.GetComponent<CharacterMotor>().canControl = false;
		
		//muestro guitext victoria
		guitextVictoria.guiText.enabled = true;
		iTweenEvent.GetEvent(guitextVictoria, "fadeInVictoria").Play();
		
		//destruyo el boss
		Destroy(this.gameObject, manejadorAudio.getDuracionAudioMuerte());
	}
	
	//funcion que toma acciones segun la fase a laque cambia
	private void cambiarFases(){
		Debug.Log("cambio fase");
		if(faseActual <= 3){
			debilidadPoder = listaPoderes[faseActual];
			transform.FindChild("BossSprite").GetComponent<OTAnimatingSprite>().tintColor = coloresFase[faseActual];
			vidaActual = vidaFase;
			particulas.startColor = coloresFase[faseActual];
			particulas.Play();
		}
		else if (faseActual == 4){
			this.transform.renderer.material.color = coloresFase[faseActual];
			vidaActual = vidaFinal;
		}
		else
			muerteBoss();
	}
	
	//resto vida y si es <= 0 reproduzco sonidos y destruyo el objeto "player"
	public void restarVidaEnemigo(float cantidad){
		if(cantidad >= 100)
			stunn ();
		else if(checkTipoDañoArma()){
			vidaActual -= cantidad;
			if(vidaActual <= 0){
				faseActual++;
				cambiarFases();
			}
			Debug.Log(vidaActual);
		}
	}
	
	private void stunn(){
		areaAtaque.collider.enabled = false;
		stunSprite.SetActive(true);
		StartCoroutine(COStunn());
	}
	
	IEnumerator COStunn(){
		yield return new WaitForSeconds(tiempoStunDinamita);
		areaAtaque.collider.enabled = true;
		stunSprite.SetActive(false);
	}
	
	//funcion que checkea si el poder del arma se corresponde a la debilidad del boss
	private bool checkTipoDañoArma(){
		if(controladorAtaquePj.getPoderActual() == debilidadPoder){
			return true;
		}
		return false;
	}
}
