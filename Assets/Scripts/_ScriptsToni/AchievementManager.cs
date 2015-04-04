using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
	#region Public members

	/// <summary>
	/// Popup to show achievements.
	/// </summary>
	public GameObject Popup = null;

	#endregion


	#region Private members
	/// <summary>
	/// Boolean to determine if we can show the achievement or we must to wait.
	/// </summary>
	private bool _canShow;

	#endregion

	#region Achievement definition
	
	/// <summary>
	/// List of achievements to unlock.
	/// </summary>
	private enum AchievementsEnum 
	{
		// Play&Care
		FirstFlame,			// Get your first flame
		FacingBoss,			// Facing the boss
		DefeatBoss,			// Defeat the boss
		
		// Dynamite power
		DynamiteSuicide,	// Dying using dynamite
		
		// About heal
		DoubleHeal, 		// Heal double max life player
		NoHealInDungeon, 	// No use heal during "mecha" searching
		NoHealInBoss, 		// No use heal during boss combat
		NoHealInAllGame, 	// No use heal in all game play
		
		// About damage
		NoDamageInDungeon,	// Dont receive damage during "mecha" searching
		NoDamageInBoss,		// Dont receive damage during boss combat
		NoDamageInAllGame,	// Dont receive damage in all game playe
	};

	/// <summary>
	/// Initializes the achievements to unlock.
	/// </summary>
	private void InitializeAchievements ()
	{
		// Recoge la llama (consume tu primera mecha)
		_achievements.Add (AchievementsEnum.FirstFlame, 
		                   new Achievement ("Recoge la llama", 
		                 					"Consume tu primera mecha."));
		
		// Enfrentate al Mal (llega hasta el boss)
		_achievements.Add (AchievementsEnum.FacingBoss, 
		                   new Achievement ("Enfréntate al Mal", 
		                 					"Consigue llegar hasta el jefe final."));
		
		// Sangre y Fuego (derrota al boss)
		_achievements.Add (AchievementsEnum.DefeatBoss, 
		                   new Achievement ("Sangre y Fuego", 
		                 					"Consigue derrotar al jefe final."));
		
		// ¿Curarse?, eso es de débiles (no te cures hasta llegar al boss)
		_achievements.Add (AchievementsEnum.NoHealInDungeon, 
		                   new Achievement ("¿Curarse? Eso es de débiles", 
		                 					"No te cures hasta enfrentarte con el jefe final."));
	}

	#endregion

	#region Public fast access, and Start() method
	
	public static readonly string GAMEOBJECT_NAME = "/AchievementManager";
	
	private static AchievementManager _instance = null;
	public static AchievementManager Instance
	{
		get
		{
			if (_instance == null) 
				_instance = GameObject.Find (GAMEOBJECT_NAME).GetComponent <AchievementManager> ();
			
			return _instance;
		}
	}
	
	
	void Start ()
	{
		InitializeAchievements ();
		Popup.SetActive (false);
		_canShow = true;
	}
	
	#endregion

	#region Achievements members
	
	/// <summary>
	/// Class to describe achievements by title and description.
	/// </summary>
	private class Achievement
	{
		/// <summary>
		/// Creates an achievement by title and description.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="description">Description.</param>
		public Achievement (string title, string description)
		{
			this.Title = title;
			this.Description = description;
		}

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; private set; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; private set; }
	}

	/// <summary>
	/// Log achievements unlocked.
	/// </summary>
	private HashSet <AchievementsEnum> _achievementsUnlocked = new HashSet <AchievementsEnum> ();

	/// <summary>
	/// The achievements title and description.
	/// </summary>
	private IDictionary <AchievementsEnum, Achievement> _achievements = new Dictionary <AchievementsEnum, Achievement> ();

	#endregion

	#region Achievements notifications

	// About flames
	private bool playerGetsFirstFlame = false;

	// About heal
	private bool playerUsedHealOnDungeon = false;
	private bool playerUsedHealOnBoss = false;
	private int playerHealedAmount = 0;

	// About receive damage
	private bool playerReceivedDamageOnDungeon = false;
	private bool playerReceivedDamageOnBoss = false;

	// About boss progress
	private bool bossInit = false;
//	private bool bossEnd = false;


	public void NotifyPlayerGetFlame ()
	{
		// If player not got any flame yet
		if (!playerGetsFirstFlame)
		{
			playerGetsFirstFlame = true;
			UnlockAchievement (AchievementsEnum.FirstFlame);
		}
	}

	public void NotifyPlayerUseHeal (int healAmount, int playerMaxLife)
	{
		Debug.Log ("[AchievementManager] - Player use heal");

		// Save state
		playerHealedAmount += healAmount;

		if (bossInit) playerUsedHealOnBoss = true;
		else playerUsedHealOnDungeon = true;

		// Check to unlock an achievement
		if (playerHealedAmount > playerMaxLife) UnlockAchievement (AchievementsEnum.DoubleHeal);
	}

	public void NotifyPlayerReceiveDamage ()
	{
		// Save state
		if (bossInit) playerReceivedDamageOnBoss = true;
		else playerReceivedDamageOnDungeon = true;
	}

	public void NotifyBossInit ()
	{
		Debug.Log ("[AchievementManager] - Boss init");

		// Save state
		bossInit = true;
		UnlockAchievement ( AchievementsEnum.FacingBoss);

		// Check to unlock an achievement
		if (!playerUsedHealOnDungeon) UnlockAchievement (AchievementsEnum.NoHealInDungeon);

//		if (!playerReceivedDamageOnDungeon) UnlockAchievement (AchievementsEnum.NoDamageInDungeon);
//		Debug.Log("MUESTRO NO DAÑO");
	}

	public void NotifyBossEnd ()
	{
		Debug.Log ("[AchievementManager] - Boss end");

		// Save state
//		bossEnd = true;

		UnlockAchievement(AchievementsEnum.DefeatBoss);
		// Check to unlock an achievement about HEAL
		if (!playerUsedHealOnBoss) UnlockAchievement (AchievementsEnum.NoHealInBoss);
		if (!playerUsedHealOnBoss && !playerUsedHealOnDungeon) UnlockAchievement (AchievementsEnum.NoHealInAllGame);

		// Check to unlock an achievement about DAMAGE
		if (!playerReceivedDamageOnBoss) UnlockAchievement (AchievementsEnum.NoDamageInBoss);
		if (!playerReceivedDamageOnBoss && !playerReceivedDamageOnDungeon) UnlockAchievement (AchievementsEnum.NoDamageInAllGame);
	}
	
	public void NotifyDynamiteKillEnemy ()
	{
		Debug.Log ("[AchievementManager] - Dynamite kill enemy");

		// TODO
	}
	
	public void NotifyDynamiteKillPlayer ()
	{
		Debug.Log ("[AchievementManager] - Dynamite kill player");

		// Unlock an achievement
		UnlockAchievement (AchievementsEnum.DynamiteSuicide);
	}

	#endregion

	#region Unlock achievements and canvas print

	private void UnlockAchievement (AchievementsEnum achievement)
	{
		if (!_achievementsUnlocked.Contains (achievement))
		{
			Debug.Log ("[AchievementManager] - Unlocked '" + achievement + "'");

			// Gets info for the achievement unlocked
			Achievement achievementInfo;
			bool achievementExists = _achievements.TryGetValue (achievement, out achievementInfo);

			if (achievementExists)
			{
				_achievementsUnlocked.Add (achievement);
				
				// Print on a canvas
				// TODO Apilar si se desbloquean varios a la vez
				Popup.SetActive (true);

				StartCoroutine (COWaitForOther (Popup.GetComponent <Animator> (),achievementInfo));
				
				// TODO Notify to MoreThanGamers API
			}
			else
			{
				Debug.LogError ("[AchievementManager] - ERROR: No information for achievement " + achievement);
			}
		}
	}
	private IEnumerator COWaitForOther( Animator animator, Achievement achievementInfo )
	{
		if(!_canShow)
			yield return new WaitForSeconds (4.2f);
		StartCoroutine(COAnimAchievement(animator,achievementInfo));
	}

	private IEnumerator COAnimAchievement (Animator animator,Achievement achievementInfo)
	{
		_canShow = false;
		Popup.transform.FindChild ("AchievementInfo").GetComponent <Text> ().text 
			= achievementInfo.Title + "\n" + achievementInfo.Description;
		// Appear
		animator.SetBool ("isShowing", true);
		yield return new WaitForSeconds (3.0f); // 1sec anim appear + 2sec still showing

		// Disappear
		animator.SetBool ("isShowing", false);
		yield return new WaitForSeconds (1.0f);
		_canShow = true;
	}

	#endregion
}
