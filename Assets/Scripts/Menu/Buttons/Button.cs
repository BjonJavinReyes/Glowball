using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public string  Button_Name;
	
	[HideInInspector]
	public Vector2 UpperLeftPos;		// Upper left position of button. This is the position with pixelinset
	Vector2 center;
	
	public void ScaleaButtonSize()
	{
		center = new Vector2(Screen.width * (1-gameObject.transform.position.x), Screen.height * (1-gameObject.transform.position.y));
		UpperLeftPos = new Vector2(center.x + gameObject.guiTexture.pixelInset.x, center.y + gameObject.guiTexture.pixelInset.y);	
	}
}
