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
	
	GameObject MainMenu;
	List<GameObject> MainMenuItems = new List<GameObject>();
	
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
	
	#region Buttons
	Button btn_start_game;
	#endregion
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_AudioManager   = sc_ScriptHelper.sc_AudioManager;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		sc_GameController = sc_ScriptHelper.sc_GameController;
		
		foreach ( Transform child in gameObject.transform)
		{
			if (child.name == "menu")
				MainMenu = child.gameObject;
		}
		
		foreach ( Transform child in MainMenu.transform)
			MainMenuItems.Add(child.gameObject);
		
		// Get all buttons in menu and add them to list
		foreach ( Button btn in MainMenu.GetComponentsInChildren<Button>())
		{
			if ( btn.Button_Name == "Start Game")
				btn_start_game = btn;
		}
		
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
				Button_Game_to_MenuScreen();
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
				CheckButtons_MainMenu();
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

	void CheckButtons_MainMenu ()
	{
		Button_Start();
	}
	
	void CheckButtons_GamePlaying ()
	{
		if (!sc_GameController.hasGameStarted)
			Button_StartGamePlay();
	}
	#region Buttons
	void Button_Start()
	{
		if (CanUseButton)
		{
			if ( GUI.Button( new Rect( btn_start_game.UpperLeftPos.x, btn_start_game.UpperLeftPos.y,
									   btn_start_game.guiTexture.pixelInset.width, btn_start_game.guiTexture.pixelInset.height), "") )
			{
				CanUseButton = false;
				StartCoroutine( AnimateObjects(true) );
				sc_FadeToScene.FadeToNewScene("Game");
			}
		}
	}
	
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
	
	void Button_Game_to_MenuScreen()
	{
		if (CanUseButton)
		{
			if ( GUI.Button( new Rect( 0, 0, Screen.width, Screen.height), "") )
			{
				StartCoroutine( AnimateObjects(false));
				Current_MenuScene = MenuScene.MAIN_MENU;
			}
		}
	}
	
	#endregion
	
	IEnumerator AnimateObjects(bool fadeToGame)
	{
		if (fadeToGame)
		{
			foreach (GameObject item in MainMenuItems)
			{
				item.GetComponent<MenuAnimation>().AnimationOut();
				yield return null;
			}
		}
		else
		{
			foreach (GameObject item in MainMenuItems)
			{
				item.GetComponent<MenuAnimation>().AnimationIn();
				yield return null;
			}
		}
			
	}
	
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
