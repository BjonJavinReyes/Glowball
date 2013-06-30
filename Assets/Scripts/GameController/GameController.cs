using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour 
{
	public static readonly float DEFAULT_WIDTH = 480;
	public static readonly float DEFAULT_HEIGHT = 800;
		
	// Script Holders
	BallController sc_BallController;
	FadeToScene    sc_FadeToScene;
	LevelManager   sc_LevelManager;
	MenuSystem     sc_MenuSystem;
	ScoreTracker   sc_ScoreTracker;
	ScriptHelper   sc_ScriptHelper;
	
	[HideInInspector] public Vector3 Accelerometer;
	
	public bool hasGameStarted = false;
	public bool useGameInput = false;
	private float accel_round = 0.01f;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_BallController = sc_ScriptHelper.sc_BallController;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		sc_MenuSystem     = sc_ScriptHelper.sc_MenuSystem;
		sc_LevelManager   = sc_ScriptHelper.sc_LevelManager; 
		sc_ScoreTracker   = sc_ScriptHelper.sc_ScoreTracker;
		
		// Tell screen to not dim
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		// Make screen fade in
		if (Application.loadedLevelName == "Game")
			sc_FadeToScene.FadeInScene();	
	}
	
	void Update()
	{		
		// Get Applications rotational value (accelerometer)
		if ( useGameInput )
			GET_Accelermeter();
		
		// Get Application Input
		ApplicationInput();
	}
	
	// Get Applications Input
	void ApplicationInput()
	{
		// Application back button
		if ( Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
	
	// Get Phones Accelerometer details
	void GET_Accelermeter()
	{
		// Store applications accelerometer to public vector
		Accelerometer.x = RoundValue( Input.acceleration.x, accel_round);
		Accelerometer.y = RoundValue( Input.acceleration.y, accel_round);
		Accelerometer.z = RoundValue( Input.acceleration.z, accel_round);
	}
	
	// Round float value
	float RoundValue(float what, float to)
	{
		return to * Mathf.Round(what/to);
	}
	
	public void GameOver()
	{
		hasGameStarted = false;	
		
		sc_BallController.FreezeBall();	
		sc_MenuSystem.Current_MenuScene = MenuSystem.MenuScene.GAME_OVER;
	}
}
