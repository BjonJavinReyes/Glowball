using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	// Script Holders
	LevelManager sc_LevelManager;
	MenuSystem   sc_MenuSystem;
	ScriptHelper sc_ScriptHelper;
	
	GUIText score_text;
	public float Game_Score = 0;
	
	private float game_time = 0;
	float time_per_point = 0.1f;
	
	int score_increment = 10;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptHelper>();
		sc_LevelManager = sc_ScriptHelper.sc_LevelManager; 
		sc_MenuSystem   = sc_ScriptHelper.sc_MenuSystem;
		
		// Initialize score text
		score_text = gameObject.guiText;
		score_text.text = "0";
	}
	
	void Update()
	{
		
		if (!sc_LevelManager.LevelIntermission &&
			sc_MenuSystem.Current_MenuScene == MenuSystem.MenuScene.GAME_PLAYING)
		{	
			
			if (game_time < 1)
				game_time += Time.deltaTime / time_per_point;
			else
			{
				Game_Score += score_increment;
				score_text.text = Game_Score.ToString();
				game_time = 0;
			}
			
		}
		
	}
	
	
}
