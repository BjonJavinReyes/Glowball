using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	GameObject GameController;
	CameraController camera_controller;
	RowManager row_man;
	
	public int CurrentLevel = 1;
	public bool LevelCameraMove = false;
	public bool LevelIntermission = true;
	public float LevelSpeed;
	
	
	public float Time_Between_Intermission;
	
	void Awake()
	{
		GameController = GameObject.Find("game_controller");
		camera_controller = GameController.GetComponent<CameraController>();
		row_man = GameController.GetComponent<RowManager>();
		
		Time_Between_Intermission = 5.0f;
		LevelSpeed = 1.0f;
		
		camera_controller.SET_CameraSpeed(LevelSpeed);
		row_man.SET_RowSpeed(LevelSpeed);
	}
	
	void Start()
	{
		StartCoroutine( StartInitermission() );	
	}
	
	IEnumerator StartInitermission()
	{
		float time = 0;
		while (time < 1)
		{
			time += Time.deltaTime / Time_Between_Intermission;	
			yield return null;
		}
		
		LevelIntermission = false;
		
		// Spawn a new row
		row_man.StartCoroutine( "CreateRow" );
	}
}
