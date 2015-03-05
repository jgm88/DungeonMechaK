using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
	#region Public fast access
	
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
	
	#endregion

	#region Achievements

	/// <summary>
	/// List of achievements to unlock.
	/// </summary>
	private enum Achievements 
	{
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
	/// Log achievements unlocked.
	/// </summary>
	private HashSet <Achievements> _achievementsUnlocked = new HashSet <Achievements> ();

	void Start ()
	{
		// TODO Initialize a dictionary or similar, with title and description of achievements
	}

	#endregion

	#region Achievements manager

	// About heal
	private bool playerUsedHealOnDungeon = false;
	private bool playerUsedHealOnBoss = false;
	private int playerHealedAmount = 0;

	// About receive damage
	private bool playerReceivedDamageOnDungeon = false;
	private bool playerReceivedDamageOnBoss = false;

	// About game progress
	private bool bossInit = false;
	private bool bossEnd = false;


	public void NotifyPlayerUseHeal (int healAmount, int playerMaxLife)
	{
		Debug.Log ("[AchievementManager] - Player use heal");

		// Save state
		playerHealedAmount += healAmount;

		if (bossInit) playerUsedHealOnBoss = true;
		else playerUsedHealOnDungeon = true;

		// Check to unlock an achievement
		if (playerHealedAmount > playerMaxLife) UnlockAchievement (Achievements.DoubleHeal);
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

		// Check to unlock an achievement
		if (!playerUsedHealOnDungeon) UnlockAchievement (Achievements.NoHealInDungeon);
		if (!playerReceivedDamageOnDungeon) UnlockAchievement (Achievements.NoDamageInDungeon);
	}

	public void NotifyBossEnd ()
	{
		Debug.Log ("[AchievementManager] - Boss end");

		// Save state
		bossEnd = true;

		// Check to unlock an achievement about HEAL
		if (!playerUsedHealOnBoss) UnlockAchievement (Achievements.NoHealInBoss);
		if (!playerUsedHealOnBoss && !playerUsedHealOnDungeon) UnlockAchievement (Achievements.NoHealInAllGame);

		// Check to unlock an achievement about DAMAGE
		if (!playerReceivedDamageOnBoss) UnlockAchievement (Achievements.NoDamageInBoss);
		if (!playerReceivedDamageOnBoss && !playerReceivedDamageOnDungeon) UnlockAchievement (Achievements.NoDamageInAllGame);
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
		UnlockAchievement (Achievements.DynamiteSuicide);
	}

	#endregion

	#region Unlock achievements and canvas print

	private void UnlockAchievement (Achievements achievement)
	{
		if (!_achievementsUnlocked.Contains (achievement))
		{
			Debug.Log ("[AchievementManager] - Unlocked '" + achievement + "'");

			_achievementsUnlocked.Add (achievement);
			// TODO Print on a canvas
			// TODO Notify to MoreThanGamers API
		}
	}

	#endregion
}
