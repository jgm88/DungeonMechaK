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
	// Use this for initialization
	void Awake ()
	{
		bossBeha = GetComponent<BossBehaviour> ();
		bossAnim = transform.GetComponentInChildren<Animation> (); //GetComponent<Animation>();
//		Debug.Log (bossAnim.GetClipCount());
		bossBeha.isMoving = true;
		waitting = false;
	}

	public void setAttacking ()
	{
		numRand = Random.Range (0, 100);
		nomAnimation = (numRand < 50) ? "attack_1" : "attack_2";
		bossAnim.Play (nomAnimation, PlayMode.StopAll);
	}

	public void setRunning ()
	{
		bossAnim.Play ("run", PlayMode.StopAll);
	}

	public void setStunned ()
	{
		bossAnim.Play ("stunned_idle", PlayMode.StopAll);
	}

	public void setIdle ()
	{
		bossAnim.Play ("idle", PlayMode.StopAll);	
	}
	void Update ()
	{
		///MAQUINA DE ESTADOS DEL BOSS
		if (!bossAnim.isPlaying && bossBeha.isStunned) {
			bossAnim.Play ("stunned_idle", PlayMode.StopAll);
		} else if (!bossBeha.receiveDamage && bossBeha.inCombat) {
			waitting = false;
			if (!bossBeha.isAttackCD) {
				numRand = Random.Range (0, 100);
				nomAnimation = (numRand < 50) ? "attack_1" : "attack_2";
				bossAnim.Play (nomAnimation, PlayMode.StopAll);
			} else if (!bossAnim.isPlaying) 
				bossAnim.Play ("idle", PlayMode.StopAll);	
		} else if (!bossAnim.isPlaying && bossBeha.isMoving) {
			waitting = false;
			bossAnim.Play ("run", PlayMode.StopAll);
			
//				bossAnim.Play("run",PlayMode.StopAll);	
		} else if (!bossAnim.isPlaying && bossBeha.receiveDamage) {
			
//					bossAnim.Play("hit",PlayMode.StopAll);	
			bossAnim.PlayQueued ("hit", QueueMode.PlayNow);
			
//					bossAnim.Play("idle",PlayMode.StopAll);
			bossAnim.PlayQueued ("idle");
			
//				waitting = true;
		}
	}
}
