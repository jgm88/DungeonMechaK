using UnityEngine;
using System.Collections;

public class BossAnimationController : MonoBehaviour {


	private EnemyBehaviour bossBeha;
	private Animation bossAnim;
	private int numRand;
	private string nomAnimation;
	// Use this for initialization
	void Awake()
	{
		bossBeha = transform.parent.GetComponent<EnemyBehaviour>();
		bossAnim = GetComponent<Animation>();
//		Debug.Log (bossAnim.GetClipCount());
		bossBeha.isMoving = true;
	}


	void LateUpdate()
	{
		///MAQUINA DE ESTADOS DEL BOSS

			if(bossBeha.inCombat)
			{
				if(!bossBeha.isAttackCD)
				{
					numRand = Random.Range(0,100);
					nomAnimation = (numRand < 50 ) ? "attack_1" : "attack_2";
					bossAnim.Play(nomAnimation,PlayMode.StopAll);
				}
				else 
					bossAnim.Play("idle",PlayMode.StopAll);	
//					nomAnimation = "idle";
			}
			else if(bossBeha.isMoving)
			{
				bossAnim.Play("run",PlayMode.StopAll);	
			}
			else if(bossBeha.receiveDamage)
				bossAnim.Play("hit",PlayMode.StopAll);	
//				nomAnimation = "hit";

			

	}
}
