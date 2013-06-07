using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	GameController game_controller;
	CameraController camera_controller;
	BoundaryManager boundary_man;
	RowManager row_man;
	BallController glow_ball;
	
	public int CurrentLevel = 0;
	public bool LevelIntermission = true;
	
	[HideInInspector]
	public bool LevelBoundaryShow = false;
	[HideInInspector]
	public int  BallIntermissionHeight;
	public float LevelSpeed;
	public float Time_Between_Intermission;
	
	public float[] IntermissionTimes = { 1.0f, 2.0f };
	
	void Awake()
	{
		// Attach all scripts
		boundary_man = GameObject.Find("Boundaries").GetComponent<BoundaryManager>();
		glow_ball = GameObject.Find("glow_ball").GetComponent<BallController>();
		game_controller = gameObject.GetComponent<GameController>();
		camera_controller = gameObject.GetComponent<CameraController>();
		row_man = gameObject.GetComponent<RowManager>();
		
		// Set intermission time intertval
		Time_Between_Intermission = 5.0f;
		
		// Set intermission height
		BallIntermissionHeight = 65;
		
		// Sey speed at which rows move up
		LevelSpeed = 2.0f;
		row_man.SET_RowSpeed(LevelSpeed);
	}
	
	void Start()
	{
		StartCoroutine( StartInitermission() );	
	}
	
	IEnumerator StartInitermission()
	{
		while (!game_controller.hasGamePlayStarted)
		{
			yield return null;
		}
		
		glow_ball.SET_BallToGameField();
		
		boundary_man.Open_TopBoundary();
		
		float time = 0;
		while (time < 1)
		{
			time += Time.deltaTime / IntermissionTimes[CurrentLevel];	
			yield return null;
		}
		
		LevelIntermission = false;
		
		// Spawn a new row
		row_man.StartCoroutine( "CreateRow" );
	}
}
