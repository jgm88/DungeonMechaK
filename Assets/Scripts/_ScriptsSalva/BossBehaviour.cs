using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBehaviour : MonoBehaviour {

	public Material bossSkinMat;
	/// <summary>
	/// The death particles.
	/// </summary>
	public GameObject deathParticles;
	/// <summary>
	/// The initial damage.
	/// </summary>
	public int initialDamage = 20;
	/// <summary>
	/// The particles.
	/// </summary>
	public ParticleSystem particles;
	/// <summary>
	/// The final damage.
	/// </summary>
	public int finalDamage = 25;
	/// <summary>
	/// The final life.
	/// </summary>
	public int finalLife = 500;
	/// <summary>
	/// The initial life.
	/// </summary>
	public int initialLife = 200;
	/// <summary>
	/// Enemy is receiving damage.
	/// </summary>
	public bool receiveDamage;
	/// <summary>
	/// Enemy is moving
	/// </summary>
	public bool isMoving;
	/// <summary>
	/// The life of enemy.
	/// </summary>
	public int life;
	/// <summary>
	/// Attack is in cooldown.
	/// </summary>
	public bool isAttackCD;
	/// <summary>
	/// State is in combat.
	/// </summary>
	public bool inCombat;
	/// <summary>
	/// The attack Cooldown.
	/// </summary>
	public float attackCD = 3f;
	/// <summary>
	/// The impact force.
	/// </summary>
	public float impactForce=20f;
	/// <summary>
	/// The stun time.
	/// </summary>
	public float stunTime = 4f;
	/// <summary>
	/// The original color.
	/// </summary>
	private Color originalColor;
	/// <summary>
	/// The number states.
	/// </summary>
	private int numStates = 4;
	/// <summary>
	/// The current state.
	/// </summary>
	private int currentState =0;
	/// <summary>
	/// The controlador ataque pj.
	/// </summary>
	private controladorAtaqueCC playerAttackController;
	/// <summary>
	/// The current damage.
	/// </summary>
	private int currentDamage;
	/// <summary>
	/// The color states.
	/// </summary>
	private List<Color> colorStates = new List<Color>();
	/// <summary>
	/// The powers list.
	/// </summary>
	private List<string> powersList = new List<string>();
	/// <summary>
	/// The weaknes power of this boss.
	/// </summary>
	public string weaknesPower;
	/// <summary>
	/// The stun sprite.
	/// </summary>
	private GameObject stunSprite;
	/// <summary>
	/// The player.
	/// </summary>
	private GameObject player;
	/// <summary>
	/// The guitext victoria.
	/// </summary>
	private GameObject guitextVictoria;



	private manejadorAudioAnimado soundManajer;
	// Use this for initialization
	void Awake () {

		soundManajer = GetComponent<manejadorAudioAnimado>();
		currentDamage = initialDamage; 
		stunSprite = transform.Find("Stun").gameObject;
		stunSprite.SetActive(false);
		guitextVictoria = GameObject.Find("victoria");
		player = GameObject.FindWithTag("Player");
		life = initialLife;
		agregarColoresYPoderes();
		weaknesPower = powersList[currentState];
	}
	
	//	// Update is called once per frame
	//	void Update () {
	//	}
	//agrega los colores de cada fase
	private void agregarColoresYPoderes(){
		colorStates.Add(originalColor);
		colorStates.Add(Color.green);
		colorStates.Add(Color.red);
		colorStates.Add(Color.blue);
		colorStates.Add(Color.white);
		powersList.Add("original");
		powersList.Add("green");
		powersList.Add("red");
		powersList.Add("blue");
	}

	public void ReceiveDamage(int damage)
	{
		Debug.Log ("LLAMADA A RECIBIR DANO");
		if(damage > 100)
		{
			Debug.Log ("RECIBO DANYO");
		}
		else if(!receiveDamage) //if(playerAttackController.getPoderActual().Equals(weaknesPower))
		{
			Debug.Log ("RECIBO DANYO");
			life -= damage;
			if (life > 0)
			{
				soundManajer.reproducirImpacto();
				receiveDamage = true;
				StartCoroutine(COHit(2f));
			}
			else
			{
				currentState++;
				cambiarFases();	
			}
		}
	}
	//funcion que toma acciones segun la fase a laque cambia
	private void cambiarFases(){
		Debug.Log("cambio fase");
		if(currentState <= 3){
			weaknesPower = powersList[currentState];
			bossSkinMat.color = colorStates[currentState];
			life = initialLife;
			particles.startColor = colorStates[currentState];
			particles.Play();
		}
		else if (currentState == 4){
			bossSkinMat.color = colorStates[currentState];
			life = finalLife;
		}
		else
			muerteBoss();
	}

	private void stun(){
		isAttackCD = true;
		stunSprite.SetActive(true);
		StartCoroutine(COStunn());
	}
	
	IEnumerator COStunn(){
		yield return new WaitForSeconds(stunTime);
		isAttackCD = false;
		stunSprite.SetActive(false);
	}

	/// <summary>
	/// Muertes the boss.
	/// funcion que realiza las acciones necesarias al morir
	/// </summary>
	private void muerteBoss(){
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		
		//lanzo el evento de del itween y desactivo el conrol
		iTweenEvent.GetEvent(Camera.main.gameObject, "recorridoEndGame").Play();
		player.GetComponent<CharacterMotor>().canControl = false;
		
		//muestro guitext victoria
		guitextVictoria.guiText.enabled = true;
		iTweenEvent.GetEvent(guitextVictoria, "fadeInVictoria").Play();
		
		//destruyo el boss
		Destroy(this.gameObject, soundManajer.getDuracionAudioMuerte());
	}


	void OnTriggerStay(Collider other){
		if(!receiveDamage && !isAttackCD && other.tag == "Player" ){ // && vidaPlayer.isVivo()
			atacar (other.gameObject);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			inCombat = true;
			isMoving = false;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			inCombat = false;
			isMoving = true;
		}
	}
	
	//funcion para atacar, bloquea el spam de ataques y envia mensajes
	private void atacar(GameObject player){
		isAttackCD = true;
		StartCoroutine(COAtacar());
		soundManajer.reproducirAtacar(0.5f);
		player.GetComponent<PlayerBehaviour>().ReceiveDamage(initialDamage);
		//envio mensajes
		//		if(padre!=null){
		//			padre.GetComponent<LoadAnimIA>().attack();
		//		}
		//this.transform.parent.gameObject.SendMessage("reproducirAtacar", 0.2f, SendMessageOptions.DontRequireReceiver);
		//		player.SendMessage("restarVida", damage,SendMessageOptions.DontRequireReceiver);	
		//		player.SendMessage("reproducirGolpeado", SendMessageOptions.DontRequireReceiver);
		//		player.SendMessage("reproducirImpacto", SendMessageOptions.DontRequireReceiver);
	}
	
	//coroutina que bloquea el spam de ataques al tiempo deseado
	IEnumerator COAtacar(){
		yield return new WaitForSeconds(attackCD);
		isAttackCD = false;
	}
	/// <summary>
	/// Wait to finish the hit animation
	/// </summary>
	/// <returns>return the trigger </returns>
	/// <param name="time">Cooldown time </param>
	IEnumerator COHit(float time)
	{
		yield return new WaitForSeconds(time);
		receiveDamage = false;
	}
}
