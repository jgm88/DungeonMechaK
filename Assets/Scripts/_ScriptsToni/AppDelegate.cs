using UnityEngine;
using System.Collections;

public class AppDelegate 
{
	#region Singleton

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static AppDelegate Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new AppDelegate ();
			}

			return _instance;
		}
	}

	private static AppDelegate _instance = null;

	private AppDelegate ()
	{
		// First scene to load
		NextLevelToLoad = "MainMenu";
	}

	#endregion

	#region Properties to pass to other scenes

	public string NextLevelToLoad;

	#endregion
}
