using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour 
{
	
	public static readonly float DEFAULT_WIDTH = 480;
	public static readonly float DEFAULT_HEIGHT = 800;

	LevelManager level_man;
	BallController glow_ball;	
	
	public bool hasGameStarted = false;
	public bool hasGamePlayStarted = false;
	[HideInInspector]
	public Vector3 Accelerometer;
	private float accel_round = 0.01f;
	
	
	void Awake()
	{
		// Find items in heirarchy
		level_man = gameObject.GetComponent<LevelManager>();
		glow_ball = GameObject.Find("glow_ball").GetComponent<BallController>();
		
		// Tell screen to not dim
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update()
	{		
		// Get Applications rotational value (accelerometer)
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
}
