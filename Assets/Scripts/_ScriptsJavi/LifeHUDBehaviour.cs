using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeHUDBehaviour : MonoBehaviour {
	
	RectTransform maskSize;
	float MaxWidth;
	float maxAnchor;
	Vector2 anchorPosition;
	Rect internalRect;

	PlayerBehaviour pb;

	int timeToExplote;
	float currentWidth;
	float currentAnchor;

	// Use this for initialization
	void Start () {
	
		pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
		timeToExplote = pb.life;
		maskSize = this.gameObject.GetComponent<Mask> ().rectTransform;
		MaxWidth = maskSize.rect.width;
		maxAnchor = maskSize.anchoredPosition.x;
		anchorPosition = maskSize.anchoredPosition;
		internalRect = maskSize.rect;
	}
	
	// Update is called once per frame
	void Update () {


	}

	void LateUpdate()
	{
		currentWidth = timeToExplote * MaxWidth / pb.life;
		currentAnchor = timeToExplote * maxAnchor / pb.life;
		internalRect.width = currentWidth;
		anchorPosition.x = currentAnchor;
		
		maskSize.sizeDelta= internalRect.size;
		maskSize.anchoredPosition = anchorPosition;

	}
}
