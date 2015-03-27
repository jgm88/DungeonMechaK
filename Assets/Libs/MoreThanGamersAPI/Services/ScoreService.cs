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
using System.Net;
using System.Collections;

namespace MoreThanGamers
{
		public class ScoreService : MTGService
		{
				
			private MTGSocket socket;
			private double score;
			
			public string alias = "";
			public int count = 0;
			public int bestScore = 0;

			public ScoreService (MTGApi api, MTGSocket socket, double score) : base(api,"challenge")
			{
				this.socket = socket;
				this.score = score;
			}

			protected sealed override IEnumerator callService()
			{

				TokenService tokenService = new TokenService(this.api,socket.socketId, socket.password);
				
				yield return api.StartCoroutine(tokenService.exectureService());
				
				if(tokenService.isError)
				{
					setError(tokenService.getStatus,tokenService.getErrorMessage);
					yield break;
				}
				
		
				WWWForm form3 = new WWWForm();
				form3.AddField("gameId", api.GameId);
				form3.AddField("socketId", socket.socketId);
				form3.AddField("score",score.ToString());
				form3.AddField("token", tokenService.Token);
				form3.AddField("sessionId", api.getSessionCode());
				WWW w = new WWW(this.connectionUrl,form3);
				
				yield return w;
				yield return new WaitForSeconds(1f); // Add a wait to make sure we have the definitions
				if (!String.IsNullOrEmpty(w.error))
				{
					this.setError(MTGService.EXTERNAL_ERROR,w.error);
					yield break;
				}

				JSONObject j = new JSONObject(w.text.Trim());

				if(!this.checkstatus(j))
					yield break;

				JSONObject jChallenge = j.GetField("challenge");
				alias = jChallenge.GetField("alias").str;
				count = int.Parse( jChallenge.GetField("count").str);
				bestScore = int.Parse( jChallenge.GetField("bestscore").str);
				//if(status==1)
				//	enviado_challenge=true;
				
		

			}
		}
}

