using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {


	public int maxLife = 100;
	public int life;

	public bool receiveDamage;

	private manejadorAudioAnimado sounManajer;
	private HUDStatusBehaviour _hsb;

	void Awake()
	{
		sounManajer = GetComponent<manejadorAudioAnimado>();
		_hsb = GameObject.Find("LifeMask").GetComponent<HUDStatusBehaviour>();
	}

	public void ReceiveDamage(int damage)
	{
		if(!receiveDamage)
		{
			life -= damage;
			if (life > 0)
			{
				sounManajer.reproducirGolpeado();
				receiveDamage = true;
				StartCoroutine(COHit(1.2f));
			}
			else
				muertePj();
			_hsb.SetValue(life, maxLife);
		}
		
	}
	public void ReceiveHeal(int heal)
	{

		life += heal;
		if(life > maxLife)
			life = maxLife;

		_hsb.SetValue(life, maxLife);

	}

	private void muertePj(){
		sounManajer.reproducirMuerte();	
		GameObject.FindWithTag("MainCamera").GetComponent<moverCamaraMuerte>().moverCamara();
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

	public bool isVivo()
	{
		if(life < 0)
			return false;
		return true;
	}
}
