using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour 
{	
	GameObject theGameController;
	GameController game_controller;
	AudioManager audio_man;
	
	GameObject MainMenu;
	[SerializeField] GameObject Fade_Scenes;
	List<GameObject> MainMenuItems = new List<GameObject>();
	
	[HideInInspector]
	public MenuScene Current_MenuScene;
	public enum MenuScene
	{
		GAME_PLAYING = 0,
		MAIN_MENU,
		PAUSE_GAME,
		HIGHSCORES,
		INSTRUCTIONS,
		OPTIONS,
		CREDITS
	};
	
	bool canClick = true;
	
	#region Buttons
	Button btn_start_game;
	#endregion
	
	void Awake()
	{
		// Attach Scripts
		theGameController = GameObject.Find("game_controller");
		game_controller = theGameController.GetComponent<GameController>();
		audio_man = theGameController.GetComponent<AudioManager>();
		
		foreach ( Transform child in gameObject.transform)
		{
			if (child.name == "Main Menu")
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
		Current_MenuScene = MenuScene.MAIN_MENU;
		
		// Create Black Fade texture (black texture that fills entire screen);
		Fade_Scenes.guiTexture.pixelInset = new Rect(0,0, Screen.width, Screen.height);
		Fade_Scenes.guiTexture.color = Color.clear;	// Set transparency to zero when start of application
		
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
		if (!game_controller.hasGamePlayStarted)
			Button_StartGamePlay();
	}
	
	#region Buttons
	void Button_Start()
	{
		if (canClick)
		{
			if ( GUI.Button( new Rect( btn_start_game.UpperLeftPos.x, btn_start_game.UpperLeftPos.y,
									   btn_start_game.guiTexture.pixelInset.width, btn_start_game.guiTexture.pixelInset.height), "") )
			{
				game_controller.hasGameStarted = true;
				canClick = false;
				StartGame();
			}
		}
	}
	
	void Button_StartGamePlay()
	{
		if (canClick)
		{
			if ( GUI.Button( new Rect( 0, 0, Screen.width, Screen.height), "") )
			{
				game_controller.hasGamePlayStarted = true;
				StartCoroutine( StartGameplay());
			}
		}
	}
	#endregion

	void StartGame ()
	{
		StartCoroutine( FadeMenuScene(true) );
		StartCoroutine( FadeToScene() );
		
		// Set current scene to game playing
		Current_MenuScene = MenuScene.GAME_PLAYING;
	}
	
	IEnumerator FadeToScene()
	{	
		/////////////////////////////
		// Fade Transparent to Black
		float fadeOutTime = 1.0f;
		float time = 0;
		
		while (time < 1)
		{
			time += Time.deltaTime / fadeOutTime;
			Fade_Scenes.guiTexture.color = Color.Lerp( Color.clear, Color.black, time);
			yield return null;
		}
		
		// Make sure fade is completely black
		Fade_Scenes.guiTexture.color = Color.black;
		
		/////////////////////////////
		// Fade Delay
		time = 0;
		float fadeDelay = 0.5f;
		
		while (time < 1)
		{
			time += Time.deltaTime / fadeDelay;
			yield return null;
		}	
		
		/////////////////////////////
		// Fade Black to Transparent
		time = 0;
		float fadeInTime = 1.0f;
		
		while (time < 1)
		{
			time += Time.deltaTime / fadeInTime;
			Fade_Scenes.guiTexture.color = Color.Lerp( Color.black, Color.clear, time);
			yield return null;
		}
		
		// Make sure fade is completely transparent
		Fade_Scenes.guiTexture.color = Color.clear;
		
		canClick = true;
	}
	
	IEnumerator FadeMenuScene(bool fadeToGame)
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
				audio_man.PlayAudioClip((int)AudioManager.SoundClips.COUNTDOWN);
			}
			else
				audio_man.PlayMusic((int)AudioManager.Music.GAMEPLAYMUSIC_ONE);
			
			yield return null;
		}
		
		yield return null;
	}	
	
	
}
