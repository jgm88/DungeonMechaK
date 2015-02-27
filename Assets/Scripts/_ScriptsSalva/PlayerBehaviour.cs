using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{


	public int maxLife = 100;
	public int life = 100;
	public int maxMana = 100;
	public int mana = 100;

	public bool receiveDamage;

	private HUDStatusBehaviour _lifeHUD;
	private HUDStatusBehaviour _manaHUD;
	private bool _isDead = false;
	private bool _isMoving = false;
	private bool _isPlayingMovementSound = false;
	private manejadorAudioAnimado _soundDeath;
	private manejadorAudioAnimado _soundMovement;
	private manejadorAudioAnimado _soundImpact;
	private manejadorAudioAnimado _soundAttack;
	private manejadorAudioAnimado _soundHitCry;
	private manejadorAudioAnimado _soundSpecial;

	void Awake ()
	{
		_lifeHUD = GameObject.Find ("LifeMask").GetComponent<HUDStatusBehaviour> ();
		_manaHUD = GameObject.Find ("ManaMask").GetComponent<HUDStatusBehaviour> ();
		_soundDeath = GameObject.Find("DeadSound").GetComponent<manejadorAudioAnimado>();
		_soundMovement = GameObject.Find("WalkSound").GetComponent<manejadorAudioAnimado>();
		_soundImpact = GameObject.Find("ImpactSound").GetComponent<manejadorAudioAnimado>();
		_soundAttack = GameObject.Find("AttackSound").GetComponent<manejadorAudioAnimado>();
		_soundHitCry = GameObject.Find("HitSound").GetComponent<manejadorAudioAnimado>();
		_soundSpecial = GameObject.Find("SpecialSound").GetComponent<manejadorAudioAnimado>();
	}

	void LateUpdate(){
		isMoving();
	}

	private void isMoving(){
		if(isPressingMovementKey()){
			_isMoving = true;
			if(!_isPlayingMovementSound)
				StartCoroutine(COSoundMoving());
		}
		else{
			_isMoving = false;
		}
	}

	private bool isPressingMovementKey(){
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
		        || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
	}

	IEnumerator COSoundMoving(){

		float duration = _soundMovement.reproducirMovimiento();
		_isPlayingMovementSound = true;
		yield return new WaitForSeconds(duration);
		_isPlayingMovementSound = false;
	}

	public void ReceiveDamage (int damage)
	{
		if (!receiveDamage) {
			life -= damage;
			if (life > 0) {
				_soundImpact.reproducirImpacto ();
				_soundHitCry.reproducirGolpeado ();
				receiveDamage = true;
				StartCoroutine (COHit (1.2f));
			} else if (!_isDead) {
				_isDead = true;
				muertePj ();
			}
			_lifeHUD.SetValue (life, maxLife);
		}
	}

	public void ReceiveHeal (int heal)
	{

		life += heal;
		if (life > maxLife)
			life = maxLife;

		_lifeHUD.SetValue (life, maxLife);

	}
	public void DeductMana (int amount)
	{

		if (mana - amount >= 0) {				
			mana -= amount;
			_manaHUD.SetValue (mana, maxMana);
		}				
	}
	public void ReceiveMana (int amount)
	{
		mana += amount;
		if (mana > maxMana)
			mana = maxMana;		
		_manaHUD.SetValue (mana, maxMana);		
	}

	private void muertePj ()
	{
		_soundDeath.reproducirMuerte ();	
		GameObject.FindWithTag ("MainCamera").GetComponent<moverCamaraMuerte> ().moverCamara ();
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

	public bool isVivo ()
	{
		if (life <= 0)
		{
			return false;
		}
		return true;
	}
}
