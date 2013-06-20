using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	// Script Holders
	BallController   sc_BallController;
	BoundaryManager  sc_BoundaryManager;
	CameraController sc_CameraController;
	GameController   sc_GameController;
	RowManager       sc_RowManager;
	ScriptHelper     sc_ScriptHelper;
	
	public int CurrentLevel = 0;
	public bool LevelIntermission = true;
	
	[HideInInspector]
	public bool LevelBoundaryShow = false;
	[HideInInspector]
	public int  BallIntermissionHeight;
	public float LevelSpeed;
	public float Time_Between_Intermission;
	
	public float[] IntermissionTimes = { 1.0f, 2.0f };
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper     = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_BallController   = sc_ScriptHelper.sc_BallController;
		sc_BoundaryManager  = sc_ScriptHelper.sc_BoundaryManager;
		sc_CameraController = sc_ScriptHelper.sc_CameraController; 
		sc_GameController   = sc_ScriptHelper.sc_GameController; 
		sc_RowManager       = sc_ScriptHelper.sc_RowManager;
		
		// Set intermission time intertval
		Time_Between_Intermission = 5.0f;
		
		// Set intermission height
		BallIntermissionHeight = 65;
		
		// Set speed at which rows move up
		LevelSpeed = 2.0f;
		sc_RowManager.SET_RowSpeed(LevelSpeed);
		
		StartCoroutine( StartInitermission() );	
	}
	
	IEnumerator StartInitermission()
	{
		while (!sc_GameController.hasGameStarted)
		{
			yield return null;
		}
		
		sc_BallController.SET_BallToGameField();
		
		sc_BoundaryManager.Open_TopBoundary();
		
		float time = 0;
		while (time < 1)
		{
			time += Time.deltaTime / IntermissionTimes[CurrentLevel];	
			yield return null;
		}
		
		LevelIntermission = false;
		
		// Spawn a new row
		sc_RowManager.StartCoroutine( "CreateRow" );
	}
}
