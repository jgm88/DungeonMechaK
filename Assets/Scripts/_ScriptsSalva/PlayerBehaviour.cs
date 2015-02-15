using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {


	public int life;

	public bool receiveDamage;

	private manejadorAudioAnimado sounManajer;

	void Awake()
	{
		sounManajer = GetComponent<manejadorAudioAnimado>();
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
		}
		
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
}
