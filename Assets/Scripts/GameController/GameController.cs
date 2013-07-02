using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;


public class GameController : MonoBehaviour 
{
	public static readonly float DEFAULT_WIDTH = 480;
	public static readonly float DEFAULT_HEIGHT = 800;
		
	// Script Holders
	BallController sc_BallController;
	FadeToScene    sc_FadeToScene;
	ScriptHelper   sc_ScriptHelper;
	
	[HideInInspector] public Vector3 Accelerometer;
	
	public  bool isGamePaused;
	public  bool isGameOver;
	private bool gameOver_Once;
	public  bool hasGameStarted;
	public  bool useGameInput;
	private float accel_round = 0.01f;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_BallController = sc_ScriptHelper.sc_BallController;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		
		// Tell screen to not dim
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		// Set booleans
		isGamePaused   = false;
		isGameOver     = false;
		gameOver_Once  = false;
		hasGameStarted = false;
		useGameInput   = true;
		
		// Make screen fade in
		if (Application.loadedLevelName == "Game")
			sc_FadeToScene.FadeInScene(); 
	}
	
	void Update()
	{		
		if (isGameOver)
			Execute_GameOver();
		
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
		
		/////////////////////////////////////////////////
		// DEBUG LOG, ONLY USED FOR TESTING!
		/////////////////////////////////////////////////
		if ( Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("--  FORCED GAME OVER  --");
			isGameOver = true;
		}
	}
	
	public void OnApplicationQuit()
	{
		PlayerPrefs.Flush();
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
	
	void Execute_GameOver()
	{
		// Only produce game over once
		if (gameOver_Once) return;
		gameOver_Once = true;
		
		// Freeze ball movement
		sc_BallController.FreezeBall();	
		
		// Play animation for game over
		Animation anim = GameObject.Find("Game Over Transition").GetComponent<Animation>();
		anim.Play("game_over");
	}
	
	public bool isGameRunning()
	{
		//Debug.Log("gp: " + isGamePaused + "  go: " + isGameOver + "  gs: " + hasGameStarted);
		
		if (!isGamePaused &&
			!isGameOver   && 
			hasGameStarted)
			return true;
		else return false;
	}
}
