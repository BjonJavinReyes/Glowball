using UnityEngine;
using System.Collections;

public class FadeToScene : MonoBehaviour 
{
	// Script Holders
	AudioManager   sc_AudioManager;
	ScriptHelper   sc_ScriptHelper;

	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_AudioManager   = sc_ScriptHelper.sc_AudioManager;
		
		// Create Black Fade texture (black texture that fills entire screen);
		gameObject.guiTexture.pixelInset = new Rect(0,0, Screen.width, Screen.height);
		
		if (Application.loadedLevelName == "Menu")
			gameObject.guiTexture.color = Color.clear;	// Set transparency to zero when start of application
		if (Application.loadedLevelName == "Game")
			gameObject.guiTexture.color = Color.black;	// Set transparency to one when start of application
	}
	
	public void FadeToNewScene(string sceneName, float fadeOutTime)
	{
		StartCoroutine( FadeScene(sceneName, fadeOutTime) );		
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
			// Calc time with delay
			time += Time.deltaTime / fadeInTime;
			
			// Fade black texture, change alpha from 1 - 0
			gameObject.guiTexture.color = Color.Lerp( Color.black, Color.clear, time);
			yield return null;
		}
		
		// Make sure fade is completely transparent
		gameObject.guiTexture.color = Color.clear;
				
		// Set object position to be behind all gui
		gameObject.transform.position = new Vector3(0,0,-2);
	}
	
	IEnumerator FadeScene(string sceneName, float fadeOutTime)
	{	
		// Set object position to be above all gui
		gameObject.transform.position = new Vector3(0,0,1);
		
		// Fade Transparent to Black
		float time = 0;
		
		// Store master volume so we can fade it
		float masterVolume = sc_AudioManager.MasterVolume;
		
		while (time < 1)
		{
			// Calc time with delay
			time += Time.deltaTime / fadeOutTime;
			
			// Fade black texture, change alpha from 0 - 1
			gameObject.guiTexture.color = Color.Lerp( Color.clear, Color.black, time);
			
			// Fade volume from top notch to zero
			sc_AudioManager.MasterVolume = Mathf.Lerp(masterVolume, 0, time);

			yield return null;
		}
		
		// Make sure fade is completely black
		gameObject.guiTexture.color = Color.black;
		
		
		// Set object position to be behind all gui
		gameObject.transform.position = new Vector3(0,0,-2);
		
		//Load new level/scene
		Application.LoadLevel(sceneName);
	}
}
