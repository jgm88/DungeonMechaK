using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{


	public int maxLife = 100;
	public int life = 100;
	public int maxMana = 100;
	public int mana = 100;

	public bool receiveDamage;

	private manejadorAudioAnimado sounManajer;
	private HUDStatusBehaviour _lifeHUD;
	private HUDStatusBehaviour _manaHUD;

	void Awake ()
	{
		sounManajer = GetComponent<manejadorAudioAnimado> ();
		_lifeHUD = GameObject.Find ("LifeMask").GetComponent<HUDStatusBehaviour> ();
		_manaHUD = GameObject.Find ("ManaMask").GetComponent<HUDStatusBehaviour> ();
	}

	public void ReceiveDamage (int damage)
	{
		if (!receiveDamage) {
			life -= damage;
			if (life > 0) {
				sounManajer.reproducirGolpeado ();
				receiveDamage = true;
				StartCoroutine (COHit (1.2f));
			} else
				muertePj ();
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
		//desactivamos la ia de los enemigos y que sigan corriendo
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			enemy.GetComponent<AIPath> ().enabled = false;
			enemy.GetComponent<Animator> ().SetBool ("inCombat", true);
		}
		//desactivamos el spawner
		GameObject.Find ("EnemySpawns").GetComponent<controladorSpawn> ().enabled = false;

		sounManajer.reproducirMuerte ();	
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
		if (life < 0)
			return false;
		return true;
	}
}
