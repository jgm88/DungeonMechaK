using UnityEngine;
using System.Collections;

public class LoadingDungeon : MonoBehaviour 
{
	public RectTransform mask;
	public RectTransform image;
	
	private float maskSizeWidth;

	private int PRUvalue = 0;
	
	// Use this for initialization
	void Start ()
	{
		maskSizeWidth = mask.rect.width;
	}

	// Update is called once per frame
	void Update () 
	{
		float loaded = Application.GetStreamProgressForLevel ("Dungeon1");
		if (loaded == 1)
		{
			Application.LoadLevel ("Dungeon1");
		}
		else
		{
			int percentLoaded = Mathf.RoundToInt (loaded * 100);
			Debug.Log ("percentLoaded = " + percentLoaded);
			SetValue (percentLoaded, 100);
		}
	}
	
	/// <summary>
	/// Cambiar el valor de la bola para que se muestre correctamente.
	/// </summary>
	/// <param name="currentValue">Valor actual de la vida/maná.</param>
	/// <param name="maxValue">Valor máximo de la vida/maná.</param>
	public void SetValue (int currentValue, int maxValue)
	{
		// Calcular posiciones
		float finalPosition = currentValue * maskSizeWidth / maxValue;
		float deltaPosition = mask.anchoredPosition.x + maskSizeWidth - finalPosition;
		
		// Trasladar máscara a un lado e hijo al contrario
		mask.Translate (-deltaPosition, 0f, 0f);
		image.Translate (deltaPosition, 0f, 0f);
	}
}
