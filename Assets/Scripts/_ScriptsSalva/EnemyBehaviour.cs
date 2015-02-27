using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
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
	/// First Attack random delay, maximum this value.
	/// </summary>
	public float firstAttackMaxDelay = 3f;
	/// <summary>
	/// The impact force.
	/// </summary>
	public float impactForce = 20f;
	/// <summary>
	/// The attack damage.
	/// </summary>
	public int damage = 10;
	/// <summary>
	/// The impact force.
	/// </summary>
	public float force = 20f;
	/// <summary>
	/// Internal animator state machine.
	/// </summary>
	protected Animator animator;

	private manejadorAudioAnimado soundManajer;

	private bool firstAttack = false;
	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		soundManajer = GetComponent<manejadorAudioAnimado> ();

	}
	
//	// Update is called once per frame
//	void Update () {
//	}

	void LateUpdate ()
	{
		if (animator) {
			animator.SetBool ("receiveDamage", receiveDamage);
			animator.SetBool ("inCombat", inCombat);
			animator.SetBool ("isAttackCooldown", isAttackCD);
			animator.SetInteger ("life", life);
		}

	}

	/// <summary>
	/// Receives the damage and destroys the object if lif <= 0
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void ReceiveDamage (int damage)
	{
		if (animator && life > 0) {
			life -= damage;
			if (life > 0) {
				soundManajer.reproducirImpacto ();
				receiveDamage = true;
				StartCoroutine (COHit (1.2f));
			} else {
				GetComponent<CharacterController> ().enabled = false;
				GetComponent<AIPath> ().enabled = false;
				collider.enabled = false;
				soundManajer.reproducirMuerte ();
				GameObject.Find ("EnemySpawns").GetComponent<controladorSpawn> ().enemiesInGame--;
				Destroy (gameObject, 3.5f);	
			}
		}
	}
	
	void OnTriggerStay (Collider other)
	{
		if (!receiveDamage && !isAttackCD && other.tag == "Player") {
			//desactivamos el seeker para que no esten "corriendo" mientras te pegan
			GetComponent<AIPath> ().enabled = false;
			atacar (other.gameObject);
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player"))
			inCombat = true;
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Player")) {
			//activamos el seeker para que vuelvan a correr cuando no estenatacando
			GetComponent<AIPath> ().enabled = true;
			inCombat = false;
		}
			
	}
	
	/// <summary>
	/// AMakes an attack to the player. Fires attacking sound event
	/// </summary>
	/// <param name="player">Player.</param>
	private void atacar (GameObject player)
	{

		StartCoroutine (COAtacar (player));

	}

	/// <summary>
	/// Coroutine That controls attacking spam waitting time.
	/// </summary>
	/// <returns>Void</returns>
	IEnumerator COAtacar (GameObject player)
	{
		yield return new WaitForEndOfFrame();
		isAttackCD = true;
		yield return new WaitForSeconds(0.5f);
		if(life>0)
		{
			soundManajer.reproducirAtacar (0.5f);
			if(inCombat)
			{
				player.GetComponent<PlayerBehaviour> ().ReceiveDamage (damage);
			}

		}


		yield return new WaitForSeconds (attackCD);
		isAttackCD = false;
	}
	/// <summary>
	/// Wait to finish the hit animation
	/// </summary>
	/// <returns>return the trigger </returns>
	/// <param name="time">Cooldown time </param>
	IEnumerator COHit (float time)
	{
		yield return new WaitForSeconds (time);
		receiveDamage = false;
	}
}
