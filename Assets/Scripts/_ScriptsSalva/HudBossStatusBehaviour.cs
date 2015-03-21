using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudBossStatusBehaviour : MonoBehaviour {


	public RectTransform mask;
	public GameObject [] lifesBar;
	public int TotalBossLife = 1100;

	private RectTransform currentLifeBar;
	private int bossState;
	private int currentBossLife;
	private float maskSizeWidth;
	// Use this for initialization
	void Start () {
		maskSizeWidth = mask.rect.width;
		bossState = 0;
		currentLifeBar = lifesBar[0].GetComponent<RectTransform>();
		currentBossLife = TotalBossLife;
	}

	/// <summary>
	/// Sets the value to bar.
	/// </summary>
	/// <param name="damage">Damage taken to boss.</param>
	public void SetValue (int damage)
	{
		currentBossLife -= damage;
		Debug.Log(currentBossLife);
		// Comprobar Overdeath
		if (currentBossLife < 0) currentBossLife = 0;
		
		// Calcular posiciones
		float finalPosition = currentBossLife * maskSizeWidth / TotalBossLife;
		float deltaPosition = mask.anchoredPosition.x + maskSizeWidth - finalPosition;
		
		// Trasladar máscara a un lado e hijo al contrario
		mask.Translate (-deltaPosition, 0f, 0f);
		lifesBar[0].GetComponent<RectTransform>().Translate(deltaPosition,0f,0f);
		lifesBar[1].GetComponent<RectTransform>().Translate(deltaPosition,0f,0f);
		lifesBar[2].GetComponent<RectTransform>().Translate(deltaPosition,0f,0f);
		lifesBar[3].GetComponent<RectTransform>().Translate(deltaPosition,0f,0f);

	}
	/// <summary>
	/// Changes the life bar.
	/// </summary>
	/// <param name="state">Boss state to change</param>
	public void ChangeBar(int state)
	{
		currentLifeBar.GetComponent<Image>().enabled = false;
		currentLifeBar = lifesBar[state].GetComponent<RectTransform>();
		currentLifeBar.GetComponent<Image>().enabled = true;
		

	}
}

