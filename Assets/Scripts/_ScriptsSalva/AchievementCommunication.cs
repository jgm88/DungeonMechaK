using UnityEngine;
using System.Collections;

public class AchievementCommunication : MonoBehaviour {

	#region private members
	private MoreThanGamers.MTGApi mtgApi;
	private static int 
		_LOGROLLAMA = 17,
		_LOGROPUERTA = 0,
		_LOGROWIN = 0,
		_LOGRONOHEAL = 0;

	private MoreThanGamers.ChallengeInfoService infoService;
	#endregion
	#region public members
	public int idLogroLLama, idLogroPuerta, idLogroWin, idLogroNoHeal;
	#endregion


	void Awake()
	{
		_LOGROLLAMA = idLogroLLama;
		_LOGROPUERTA = idLogroPuerta;
		_LOGROWIN = idLogroWin;
		_LOGRONOHEAL = idLogroNoHeal;
	}
	// Use this for initialization
	void Start () {
		mtgApi = GameObject.FindObjectOfType(typeof(MoreThanGamers.MTGApi)) as MoreThanGamers.MTGApi;

		infoService = mtgApi.getSocketWithID(_LOGROLLAMA).createChallengeInfoService();

		infoService.endCorrectListener += loadChallenge1Success;
		
	}

	private void loadChallenge1Success(MoreThanGamers.MTGService s)
	{
		MoreThanGamers.ChallengeInfoService challengeInfo = s as MoreThanGamers.ChallengeInfoService;
		
		/**
		 * No Challenge
		 */
		if(challengeInfo.challenge == null)
		{
			Debug.Log("No Hay reto solidario");
//			quitError("Level"+OpenLevel.CURRENT_LEVEL);
			return;
		}
//		
//		/** Challenge Finish **/
		if(challengeInfo.challenge.isFinish())
		{
			Debug.Log("Reto finalizado");
			return;
		}
//		/**
// 		* Challenge
// 		*/ 

		 //.challengeText.text = "Progresion del Reto ";
//			+MoreThanGamers.challengeInfo.challenge
//				.Count+"/"+challengeInfo.
//				challenge
//				.Max+" \n"+ 
//				challengeInfo.
//				challenge
//				.Info;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
