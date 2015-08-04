using UnityEngine;
using System.Collections;

public class AchievementCommunication : MonoBehaviour {

	#region private members
	/// <summary>
	/// The mtg API.
	/// </summary>
//	private MoreThanGamers.MTGApi mtgApi;
	/// <summary>
	/// The socktes id.
	/// </summary>
	private static int _LOGROLLAMA = 22;
	private static int _LOGROPUERTA = 19;
	private static int _LOGROWIN = 20;
	private static int _LOGRONOHEAL = 21;
	/// <summary>
	/// The info service.
	/// </summary>
//	private MoreThanGamers.ChallengeInfoService infoService;
	#endregion
	#region public members
	/// <summary>
	/// The sockets identifier.
	/// </summary>
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
//		mtgApi = GameObject.FindObjectOfType(typeof(MoreThanGamers.MTGApi)) as MoreThanGamers.MTGApi;
//
//		infoService = mtgApi.getSocketWithID(_LOGRONOHEAL).createChallengeInfoService();
//		infoService.endCorrectListener += loadChallenge1Success;
//		infoService.errorListener += loadChallengeError;
//		infoService = mtgApi.getSocketWithID(_LOGROLLAMA).createChallengeInfoService();
//		infoService.endCorrectListener += loadChallenge1Success;
//		infoService.errorListener += loadChallengeError;
//		infoService = mtgApi.getSocketWithID(_LOGROPUERTA).createChallengeInfoService();
//		infoService.endCorrectListener += loadChallenge1Success;
//		infoService.errorListener += loadChallengeError;
//		infoService = mtgApi.getSocketWithID(_LOGROWIN).createChallengeInfoService();
//		infoService.endCorrectListener += loadChallenge1Success;
//		infoService.errorListener += loadChallengeError;
		
	}
	#region private callbacks
//	private void loadChallengeError(MoreThanGamers.MTGService s)
//	{
//		MoreThanGamers.ChallengeInfoService challengeIngo = s as MoreThanGamers.ChallengeInfoService;
//
//		if(challengeIngo == null)
//		{
//			Debug.Log("No hay reto solidario");
//			return;
//		}
//
//		Debug.Log(challengeIngo.getErrorMessage);
//	}
//	private void loadChallenge1Success(MoreThanGamers.MTGService s)
//	{
//		MoreThanGamers.ChallengeInfoService challengeInfo = s as MoreThanGamers.ChallengeInfoService;
//		
//		/**
//		 * No Challenge
//		 */
//		if(challengeInfo.challenge == null)
//		{
//			Debug.Log("No Hay reto solidario");
////			quitError("Level"+OpenLevel.CURRENT_LEVEL);
//			return;
//		}
//		
//		/** Challenge Finish **/
//		if(challengeInfo.challenge.isFinish())
//		{
//			Debug.Log("Reto finalizado");
//			return;
//		}
//		/**
// 		* Challenge
// 		*/ 
//		Debug.Log(challengeInfo.challenge.Info);
//		 //.challengeText.text = "Progresion del Reto ";
////			+MoreThanGamers.challengeInfo.challenge
////				.Count+"/"+challengeInfo.
////				challenge
////				.Max+" \n"+ 
////				challengeInfo.
////				challenge
////				.Info;
//	}
//	private void sendedCallback(MoreThanGamers.MTGService s)
//	{
//		MoreThanGamers.AnonymusScoreService scoreServ = s as MoreThanGamers.AnonymusScoreService;
//
//		if(scoreServ == null)
//		{
//			Debug.Log("No Hay reto solidario");
//			return;
//		}
//
//		Debug.Log(scoreServ.getStatus);
//	}
	#endregion

	#region public methods
	public void sendGetFlame()
	{
//		MoreThanGamers.AnonymusScoreService service = mtgApi.getSocketWithID(_LOGROLLAMA).createAnonymusScoreService();
//
//		service.endCorrectListener+= sendedCallback;

	}

	public void sendDoorOpen()
	{
//		MoreThanGamers.AnonymusScoreService service = mtgApi.getSocketWithID(_LOGROPUERTA).createAnonymusScoreService();
//		
//		service.endCorrectListener+= sendedCallback;
	}

	public void sendEndGame()
	{
//		MoreThanGamers.AnonymusScoreService service = mtgApi.getSocketWithID(_LOGROWIN).createAnonymusScoreService();
//		
//		service.endCorrectListener+= sendedCallback;
	}

	public void sendNoHeal()
	{
//		MoreThanGamers.AnonymusScoreService service = mtgApi.getSocketWithID(_LOGRONOHEAL).createAnonymusScoreService();
//		
//		service.endCorrectListener+= sendedCallback;
	}


	#endregion
}
