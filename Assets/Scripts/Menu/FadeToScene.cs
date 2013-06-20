using UnityEngine;
using System.Collections;

public class FadeToScene : MonoBehaviour 
{
	// Script Holders
	MenuSystem     sc_MenuSystem;
	ScriptHelper   sc_ScriptHelper;

	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_MenuSystem     = sc_ScriptHelper.sc_MenuSystem;
		
		// Create Black Fade texture (black texture that fills entire screen);
		gameObject.guiTexture.pixelInset = new Rect(0,0, Screen.width, Screen.height);
		gameObject.guiTexture.color = Color.clear;	// Set transparency to zero when start of application
	}
	
	public void FadeToNewScene(string sceneName)
	{
		StartCoroutine( FadeScene(sceneName) );		
	}
	
	public void FadeInScene()
	{
		StartCoroutine( FadeIn() );
	}
	
	IEnumerator FadeIn()
	{
		// Set object position to be above all gui
		gameObject.transform.position = new Vector3(0,0,1);
			
		// Fade Black to Transparent
		float time = 0;
		float fadeInTime = 1.5f;
		
		while (time < 1)
		{
			time += Time.deltaTime / fadeInTime;
			gameObject.guiTexture.color = Color.Lerp( Color.black, Color.clear, time);
			yield return null;
		}
		
		// Make sure fade is completely transparent
		gameObject.guiTexture.color = Color.clear;
		
		sc_MenuSystem.CanUseButton = true;
	}
	
	IEnumerator FadeScene(string sceneName)
	{	
		// Set object position to be behind some gui
		gameObject.transform.position = Vector3.zero;
		
		// Fade Transparent to Black
		float time = 0;
		float fadeOutTime = 1.5f;
		
		while (time < 1)
		{
			time += Time.deltaTime / fadeOutTime;
			gameObject.guiTexture.color = Color.Lerp( Color.clear, Color.black, time);
			yield return null;
		}
		
		// Make sure fade is completely black
		gameObject.guiTexture.color = Color.black;
		
		//Load new level/scene
		Application.LoadLevel(sceneName);
	}
}
