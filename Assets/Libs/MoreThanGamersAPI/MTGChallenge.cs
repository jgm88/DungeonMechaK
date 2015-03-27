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
using System.Collections.Generic;

namespace MoreThanGamers
{
	public class MTGChallenge
	{
		private int challengeId;
		private string challengeName;
		private string challengeInfo;
		private int challengeCount;
		private int challengeMax;
		private int corporationId = -1;
		private int ngoId = -1;
		private MTGApi api;

		public MTGChallenge (MTGApi api, JSONObject jChallenge)
		{
			this.api = api;


 			challengeId = int.Parse( jChallenge.GetField("id").str);
			challengeName = jChallenge.GetField("name").str.Trim();
			challengeInfo =  jChallenge.GetField("info").str.Trim();
			challengeCount = int.Parse(jChallenge.GetField("counter").str) ;
			challengeMax = int.Parse(jChallenge.GetField("max").str);
			if(!jChallenge.GetField("corporation_id").IsNull)
				corporationId = int.Parse( jChallenge.GetField("corporation_id").str);
			if(!jChallenge.GetField("ngo_id").IsNull)
				ngoId = int.Parse(jChallenge.GetField("ngo_id").str);
		}

		public int ID {get{ return challengeId;}}
		public string Name {get{ return challengeName;}}
		public string Info {get{ return challengeInfo;}}
		public int Count {get{ return challengeCount;}}
		public int Max {get{ return challengeMax;}}
		public int CorporationId {get{ return corporationId;}}
		public int NGOId {get{ return ngoId;}}
		public bool isFinish() 
		{
			return Count >= Max;
		}
	
		public RankingService getRankingService(int limit = 10, int page = 0)
		{
			RankingService ranking =  new RankingService(api,challengeId,limit,page);

			api.sendService(ranking);
			return ranking;
		}
	}
}

