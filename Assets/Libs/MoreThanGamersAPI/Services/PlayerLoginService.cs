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
	public class PlayerLoginService : MTGService
	{
		public MTGPlayer player;
		public Boolean gameHasPlayerLogged;	//False if is not loaded also!
		public PlayerLoginService (MTGApi api) :base(api,"player")
		{
			player = null;
			gameHasPlayerLogged = false;
		}

		protected sealed override IEnumerator callService ()
		{
			WWWForm form2 = new WWWForm();

			form2.AddField("sessionId",api.getSessionCode());
			form2.AddField("secretCode", api.getSecretCode());
			WWW w = new WWW(this.connectionUrl,form2);
			
			yield return w; yield return new WaitForSeconds(1f);
			if (!String.IsNullOrEmpty(w.error))
			{
				this.setError(MTGService.EXTERNAL_ERROR,w.error);
				yield break ;
			}
			


			//	input=prueba.Substring(1,prueba.Length-2);
			JSONObject j = new JSONObject(w.text.Trim());
			if(!this.checkstatus(j))
			{
				yield break;
			}
			gameHasPlayerLogged =! (j.GetField("user").ToString() == "\"NULL\"");
			if(gameHasPlayerLogged)
				player = new MTGPlayer(api,j.GetField("user"));

		}



	}


}

