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
using System.Net;


namespace MoreThanGamers
{
	public class RankingService : MTGService
	{


		public List<MTGRankingRow> playerList;
		private int page;
		private int limit;
		private int challengeId;
		public RankingService (MTGApi api,int challengeId, int limit = 10,int page = 0) : base(api,"challengelist")
		{
			this.playerList = new List<MTGRankingRow>();
			this.page = page;
			this.limit = limit;
			this.challengeId = challengeId;
		}

		protected sealed override IEnumerator callService ()
		{
			Hashtable headers = new Hashtable();
			headers.Add("challengeId",challengeId);
			headers.Add("page",page);
			headers.Add("limit",limit);
			WWW w = new WWW(this.connectionUrl+MTGService.HastableToUrlGetParametter(headers));
			
			yield return w; yield return new WaitForSeconds(1f);
			
			if (!String.IsNullOrEmpty(w.error))
			{
				this.setError(MTGService.EXTERNAL_ERROR,w.error);
				yield break ;
			}


			JSONObject j = new JSONObject(w.text.Trim());
			if(!this.checkstatus(j))
				yield break;
			playerList.Clear();
			JSONObject jResult = j.GetField("result");
			foreach(JSONObject jPlayer in jResult.list){
				MTGRankingRow pData; 
				pData.alias = jPlayer.GetField("alias").str;
				pData.count = int.Parse(jPlayer.GetField("count").str);
				pData.score = int.Parse(  jPlayer.GetField("bestscore").str);
				playerList.Add(pData);
			}

		}


	}
}
