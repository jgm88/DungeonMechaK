using UnityEngine;
using System.Collections;

public class BossAnimationController : MonoBehaviour
{
	private BossBehaviour bossBeha;
	private int numRand;
	private string nomAnimation;
	private bool _ismuerto;

	public Animation bossAnim;

	// Use this for initialization
	void Awake ()
	{
		bossBeha = GetComponent<BossBehaviour> ();
		bossAnim = transform.GetComponentInChildren<Animation> ();
		bossBeha.isMoving = true;
		_ismuerto = false;
	}

	public void PlayAttack()
	{
		// TODO mirar semaforo entre animaciones de ataque!
		if(!bossAnim.IsPlaying("attack_1") && !bossAnim.IsPlaying("Attack_2"))
		{
			numRand = Random.Range (0, 100);
			nomAnimation = (numRand < 50) ? "attack_1" : "attack_2";
			bossAnim.Play (nomAnimation, PlayMode.StopAll);
		}
	}

	public void PlayDeath()
	{
		if(!bossAnim.IsPlaying("death") && !_ismuerto)
		{
			_ismuerto = true;
			bossAnim.Play("death",PlayMode.StopAll);
		}
	}

	public void PlayRun ()
	{

		bossAnim.Play ("run", PlayMode.StopAll);
	}

	public void PlayStun ()
	{

		bossAnim.Play ("stunned_idle", PlayMode.StopAll);
	}

	public void PlayIdle ()
	{
		if(!bossAnim.IsPlaying("attack_1") && !bossAnim.IsPlaying("Attack_2") && !bossAnim.IsPlaying("hit") && !bossAnim.IsPlaying("stunned_idle"))
			bossAnim.Play ("idle",PlayMode.StopAll);


	}

	public void PlayHit()
	{
		if(!bossAnim.IsPlaying("attack_1") && !bossAnim.IsPlaying("Attack_2"))
			bossAnim.Play("hit",PlayMode.StopAll);


	}

}
