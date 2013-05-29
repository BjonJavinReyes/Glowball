using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	LevelManager level_man;
	BallController glow_ball;
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
		
		//DisplayGameTime();
	}
	
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
	
	float oldtime = 0;
	void DisplayGameTime()
	{
		float time = Mathf.Round(Time.timeSinceLevelLoad / 1.0f);
		if (oldtime != time)
			Debug.Log("Time: " + Mathf.Round(Time.timeSinceLevelLoad / 1.0f));
		oldtime = time;
	}
	
	// Round float value
	float RoundValue(float what, float to)
	{
		return to * Mathf.Round(what/to);
	}
}
