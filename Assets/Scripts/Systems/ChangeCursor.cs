using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {


	public Texture2D yourCursor;  // Your cursor texture
	public float cursorSizeX = 32f;  // Your cursor size x
	public float cursorSizeY = 32f;  // Your cursor size y
	Rect rectangle;
	float x;
	float y;

	// Use this for initialization
	void Start () {
		ShowCursor(false);
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		x = Event.current.mousePosition.x-cursorSizeX/2;
		y = Event.current.mousePosition.y-cursorSizeY/2;
		rectangle = new Rect(x,y,cursorSizeX, cursorSizeY);

		GUI.DrawTexture(rectangle, yourCursor);

	}
	public void ShowCursor(bool show)
	{
		Screen.showCursor = false;
	}
}
