using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Borrame : MonoBehaviour 
{
	/*
	public float timeToExplote;
	public float timeLife;

	public GameObject timeBar;
	private RectTransform maskSize;
	private float MaxWidth;
	private float maxAnchor;
	private Vector2 anchorPosition;
	private Rect internalRect;
	*/

	public float kVELOCIDAD = 0.01f;
	
	public RectTransform padre;
	public RectTransform hijo;

	// Use this for initialization
	/*
	void Start () {
		// Recogemos cosas del canvas///
		maskSize = timeBar.GetComponent<Mask> ().rectTransform;
		MaxWidth = maskSize.rect.width;
		maxAnchor = maskSize.anchoredPosition.x;
		anchorPosition = maskSize.anchoredPosition;
		internalRect = maskSize.rect;
	}
	
	void LateUpdate()
	{
		Debug.Log (MaxWidth);


		float currentWidth = timeToExplote * MaxWidth / timeLife;
		float currentAnchor = timeToExplote * maxAnchor / timeLife;
		internalRect.width = currentWidth;
		anchorPosition.x = currentAnchor;

		maskSize.sizeDelta= internalRect.size;
		maskSize.anchoredPosition = anchorPosition;
	}
	*/

	void LateUpdate ()
	{
		padre.Translate (0f, -kVELOCIDAD, 0f);
		hijo.Translate (0f, kVELOCIDAD, 0f);

		/*
		padreRect.height -= kVELOCIDAD;
		padreAnchor.y -= kVELOCIDAD;
		
		hijoRect.height -= kVELOCIDAD;
		hijoAnchor.y -= kVELOCIDAD;

		
		padre.sizeDelta = hijoRect.size;
		padre.anchoredPosition = padreAnchor;
		*/

		
	}
}
