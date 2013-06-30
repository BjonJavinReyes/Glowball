using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour 
{	

	// Script Holders
	AudioManager   sc_AudioManager;
	FadeToScene    sc_FadeToScene;
	GameController sc_GameController;
	ScriptHelper   sc_ScriptHelper;
	
	
	[HideInInspector] public bool CanUseButton = true;
	[HideInInspector] public MenuScene Current_MenuScene;
	[HideInInspector] public enum MenuScene
	{
		GAME_PLAYING = 0,
		GAME_OVER,
		MAIN_MENU,
		PAUSE_GAME,
		HIGHSCORES,
		INSTRUCTIONS,
		OPTIONS,
		CREDITS
	};
	
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_AudioManager   = sc_ScriptHelper.sc_AudioManager;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		sc_GameController = sc_ScriptHelper.sc_GameController;
	
		
		// Set current scene to main menu
		if (Application.loadedLevelName == "Menu")
			Current_MenuScene = MenuScene.MAIN_MENU;
		else
			Current_MenuScene = MenuScene.GAME_PLAYING;
		
	}
	
	void OnGUI()
	{
		// Make all button backgrounds transparent, we only want to see the textures on them
		GUI.backgroundColor = new Color(0,0,0,0);
		
		switch(Current_MenuScene)
		{
			// Game Related
			case MenuScene.GAME_PLAYING:
			{
				CheckButtons_GamePlaying();
				break;
			}
			case MenuScene.GAME_OVER:
			{
				break;
			}
			case MenuScene.PAUSE_GAME:
			{
				break;
			}
			case MenuScene.OPTIONS:
			{
				break;
			}
			
			// Menu Related
			case MenuScene.MAIN_MENU:
			{
				break;
			}
			case MenuScene.INSTRUCTIONS:
			{
				break;
			}
			case MenuScene.CREDITS:
			{
				break;
			}
			case MenuScene.HIGHSCORES:
			{
				break;
			}
			
		}
	}

	void CheckButtons_GamePlaying ()
	{
		if (!sc_GameController.hasGameStarted)
			Button_StartGamePlay();
	}
	
	
	#region Buttons
	
	void Button_StartGamePlay()
	{
		if (CanUseButton)
		{
			if ( GUI.Button( new Rect( 0, 0, Screen.width, Screen.height), "") )
			{
				sc_GameController.useGameInput = true;
				sc_GameController.hasGameStarted = true;
				StartCoroutine( StartGameplay());
			}
		}
	}
	#endregion
	
	IEnumerator StartGameplay()
	{
		float time = 0;
		
		for (int i = 0; i < 4; i++)
		{
			while (time < 1)
			{
				time += Time.deltaTime;
				yield return null;
			}
			time = 0;
			if (i < 3)
			{
				//Debug.Log("Game starts in: " + (3-i));
				sc_AudioManager.PlayAudioClip((int)AudioManager.SoundClips.COUNTDOWN);
			}
			else
				sc_AudioManager.PlayMusic((int)AudioManager.Music.GAMEPLAYMUSIC_ONE);
			
			yield return null;
		}
		
		yield return null;
	}	
	
	public bool isGamePlayMoving()
	{
		if (Current_MenuScene == MenuSystem.MenuScene.GAME_PLAYING ||
			Current_MenuScene == MenuSystem.MenuScene.PAUSE_GAME)
			return true;
		else return false;
	}
	
}
