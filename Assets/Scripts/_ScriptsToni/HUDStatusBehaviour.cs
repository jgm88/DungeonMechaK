using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Calcula y muestra la altura que deben tener las bolas de vida y maná.
/// </summary>
public class HUDStatusBehaviour : MonoBehaviour 
{
	public RectTransform mask;
	public RectTransform image;
	
	private float maskSizeHeight;
	
	// Use this for initialization
	void Start ()
	{
		maskSizeHeight = mask.rect.height;
	}
	
	/// <summary>
	/// Cambiar el valor de la bola para que se muestre correctamente.
	/// </summary>
	/// <param name="currentValue">Valor actual de la vida/maná.</param>
	/// <param name="maxValue">Valor máximo de la vida/maná.</param>
	public void SetValue (int currentValue, int maxValue)
	{
		// Comprobar Overheal
		if (currentValue > maxValue) currentValue = maxValue;
		// Comprobar Overdeath
		else if (currentValue < 0) currentValue = 0;

		// Calcular posiciones
		float finalPosition = currentValue * maskSizeHeight / maxValue;
		float deltaPosition = mask.anchoredPosition.y + maskSizeHeight - finalPosition;

		// Trasladar máscara a un lado e hijo al contrario
		mask.Translate (0f, -deltaPosition, 0f);
		image.Translate (0f, deltaPosition, 0f);
	}
}
