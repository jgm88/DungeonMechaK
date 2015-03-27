//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

namespace MoreThanGamers
{
	[Serializable()]
	public class MTGSocket :MonoBehaviour
	{

		public int socketId;
		public string password;
		public bool anonymusScoreAllowed = false; 
		private MTGApi api;
		private int current_challenge_id;


		public void Awake()
		{
			this.current_challenge_id = -1;

			this.api =  GameObject.FindObjectOfType<MTGApi>();
		}

		public ScoreService createSendScoreService(double score)
		{
			ScoreService s = new  ScoreService(api,this,score);
			api.sendService(s);
			return s;

		}
		public ChallengeInfoService createChallengeInfoService()
		{
			ChallengeInfoService s = new ChallengeInfoService(api,socketId,password	);
			api.sendService(s);
			s.endCorrectListener += challengeIdRecived;
			return s;

		}
		private void challengeIdRecived(MTGService s)
		{
			MTGChallenge ch = (s as ChallengeInfoService).challenge;
			if(ch != null)
				current_challenge_id = ch.ID;
		}

		public AnonymusScoreService createAnonymusScoreService()
		{
			AnonymusScoreService s = new AnonymusScoreService(api,this);
			api.sendService(s);
			return s;
		}

		public int CurrentChallengeId { get {
				return current_challenge_id;	
		}}
	}
}

