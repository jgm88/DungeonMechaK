using UnityEngine;
using System.Collections;

public class BossAnimationController : MonoBehaviour
{
	private BossBehaviour bossBeha;
	private Animation bossAnim;
	private int numRand;
	private string nomAnimation;

	// parcheada al canto
	private bool waitting;

	private enum states{
		ATTACK,
		IDLE,
		RUN,
		HIT,
		STUN,
		DIE
	}

	states currentState;
	// Use this for initialization
	void Awake ()
	{
		bossBeha = GetComponent<BossBehaviour> ();
		bossAnim = transform.GetComponentInChildren<Animation> (); //GetComponent<Animation>();
//		Debug.Log (bossAnim.GetClipCount());
		bossBeha.isMoving = true;
		currentState = states.RUN;
		waitting = false;
	}

	public void setAttacking ()
	{
		Debug.Log("ESTADO ATACAR");

		// TODO mirar semaforo entre animaciones de ataque!
		if(!bossAnim.IsPlaying("attack_1") && !bossAnim.IsPlaying("Attack_2"))
		{
			numRand = Random.Range (0, 100);
			nomAnimation = (numRand < 50) ? "attack_1" : "attack_2";
			bossAnim.Play (nomAnimation, PlayMode.StopAll);
		}

//		if(bossBeha.isAttackCD && bossBeha.inCombat)
//			currentState = states.IDLE;
//		else if(bossBeha.isMoving)
//			currentState = states.RUN;
	}

	public void setRunning ()
	{
		Debug.Log("ESTADO CORRER");
		bossAnim.Play ("run", PlayMode.StopAll);

		if(bossBeha.inCombat)
			currentState = states.IDLE;
		else if(bossBeha.receiveDamage)
			currentState = states.HIT;
		else if(bossBeha.life < 0)
			currentState = states.DIE;
		else if(bossBeha.isStunned)
			currentState = states.STUN;
	}

	public void setStunned ()
	{
		Debug.Log("ESTADO STUN");
		bossAnim.Play ("stunned_idle", PlayMode.StopAll);
	}

	public void setIdle ()
	{
		Debug.Log("ESTADO IDLE");
		bossAnim.Play ("idle");

		if(!bossBeha.isAttackCD)
			currentState = states.ATTACK;
		if(bossBeha.isMoving)
			currentState = states.RUN;

	}

	private void setHit()
	{
		Debug.Log("ESTADO HIT");
		bossAnim.Play("hit",PlayMode.StopAll);


	}

	void Update ()
	{
		switch(currentState)
		{
			case states.RUN:
				setRunning();
				break;
			case states.IDLE:
				setIdle();
				break;
			case states.ATTACK:
				setAttacking();
				break;
			case states.STUN:
				setStunned();
				break;
			case states.HIT:
				setHit();
				break;
			case states.DIE:
				break;
		}



//		if(currentState == states.RUN)
//		{
//			setRunning();
//		}
//		else if(currentState == states.IDLE)
//		{
//			setIdle();
//		}
//		else if(currentState == states.ATTACK)
//		{
//			setAttacking();
//		}
//		else
//
//
//
//		///MAQUINA DE ESTADOS DEL BOSS
//		if (!bossAnim.isPlaying && bossBeha.isStunned) {
//			bossAnim.Play ("stunned_idle", PlayMode.StopAll);
//		} else if (!bossBeha.receiveDamage && bossBeha.inCombat) {
//			waitting = false;
//			if (!bossBeha.isAttackCD) {
//				numRand = Random.Range (0, 100);
//				nomAnimation = (numRand < 50) ? "attack_1" : "attack_2";
//				bossAnim.Play (nomAnimation, PlayMode.StopAll);
//			} else if (!bossAnim.isPlaying) 
//				bossAnim.Play ("idle", PlayMode.StopAll);	
//		} else if (!bossAnim.isPlaying && bossBeha.isMoving) {
//			waitting = false;
//			bossAnim.Play ("run", PlayMode.StopAll);
//			
////				bossAnim.Play("run",PlayMode.StopAll);	
//		} else if (!bossAnim.isPlaying && bossBeha.receiveDamage) {
//			
////					bossAnim.Play("hit",PlayMode.StopAll);	
//			bossAnim.PlayQueued ("hit", QueueMode.PlayNow);
//			
////					bossAnim.Play("idle",PlayMode.StopAll);
//			bossAnim.PlayQueued ("idle");
//			
////				waitting = true;
//		}
	}
}
