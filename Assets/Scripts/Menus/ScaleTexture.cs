using UnityEngine;
using System.Collections;

public class ScaleTexture : MonoBehaviour 
{
	float ScreenRatio;
	
	void Start()
	{
		// Scale Texture Size
		ScaleImagesToResolution();
		
		// If texture is also a button scale button size as well
		if (gameObject.GetComponent<Button>())
			gameObject.GetComponent<Button>().ScaleaButtonSize();
	}
	
	void ScaleImagesToResolution()
	{
		// If object does not contain a guitexture disregard
		if (!gameObject.GetComponent<GUITexture>()) return;
		
		// Get ratio of scree width and screen height
		float screen_width = (float)Screen.width / GameController.DEFAULT_WIDTH;
		float screen_height = (float)Screen.height / GameController.DEFAULT_HEIGHT;
		
		// If object is a guitexture resize the guitexture		
		Rect pInset = gameObject.guiTexture.pixelInset;
		Vector2 dim =  new Vector2( pInset.width, pInset.height);
		
		if (screen_width != 1)
		{
			dim.x *= screen_width;
			pInset.width = (int)dim.x;
			pInset.x = - (dim.x/2);
		}
		
		if (screen_height != 1)
		{
			dim.y *= screen_height;
			pInset.height = (int)dim.y;
			pInset.y = - (dim.y/2);
		}
				
		// Resize image to new pixel inset dimensions
		gameObject.guiTexture.pixelInset = pInset;
		
	}
	
}
