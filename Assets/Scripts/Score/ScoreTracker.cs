using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	// Script Holders
	GameController   sc_GameController;
	HighScoreManager sc_HighScoreManager;
	ScriptHelper     sc_ScriptHelper;
	
	[HideInInspector] public int Game_Score = 0;		// Players game score
	
	// Score text
	[SerializeField] TextMesh GameOver_Score;
	TextMesh score_text;
	
	[HideInInspector] public bool Hurry_GameOver_ScoreCount;
	private float time_calc_score = 3.0f;	// Time for game over score calc
	
	private float game_time = 0;		// Used to hold the current game time for delaying score count
	
	// Score Calculations
	float time_per_point = 0.1f;
	int score_increment = 10;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper     = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptHelper>();
		sc_GameController   = sc_ScriptHelper.sc_GameController;
		sc_HighScoreManager = sc_ScriptHelper.sc_HighScoreManager;
		
		// Initialize score text
		score_text = gameObject.GetComponent<TextMesh>();
		score_text.text = "0";
		
		// If user hit retry or quit, hurry score count
		Hurry_GameOver_ScoreCount = false;
	}
	
	void Update()
	{	
		// If game is not running, do not calc score,
		// Game maybe did not start, is paused, or is over
		if (!sc_GameController.isGameRunning()) return;
			
		// Delay between each score calc
		// Score is added by incements after each delay time is up
		// DEFAULT set to 10 points every tenth of a second, 100 points a second
		if (game_time < 1)
			game_time += Time.deltaTime / time_per_point;
		else
		{
			// Add new score up
			Game_Score += score_increment;
			score_text.text = Game_Score.ToString();
			game_time = 0;
		}
	}
	
	public void GameOver_CountScore()
	{		
		// Calculate end game score
		StartCoroutine( Count_GameOverScore() );
	}
	
	// Used for Game Over scene, reduces score uptop, and adds score in game over view
	IEnumerator Count_GameOverScore()
	{
		// Save score if made leaderboard
		sc_HighScoreManager.SaveScore(Game_Score);
		
		// Will calculate until completed
		bool completed_count = false;
		float time = 0;
		
		// Have temporary variables to store score
		float temp_top_score = Game_Score;
		float temp_bottom_score = 0;
		
		while (!completed_count)
		{
			// If user hit quit or retry before score count completed,
			// rush it by just showing the total score, 0 time
			if (Hurry_GameOver_ScoreCount)
			{
				score_text.text = "0";
				GameOver_Score.text = Game_Score.ToString();
				break;	
			}
		
			// If time is less then time_calc_score calc score
			// else break while loop, completed calculating the score
			if (time < 1)
			{
				// Calc time with delay
				time += Time.deltaTime / time_calc_score;
				
				// Lerp time calc, one goes down, one goes up
				temp_top_score = Mathf.Lerp(Game_Score, 0, time);
				temp_bottom_score = Mathf.Lerp(0, Game_Score, time);
					
				// Set text values for top and bottom, convert float to int so no decimals are displayed
				score_text.text     = Mathf.CeilToInt(temp_top_score).ToString();
				GameOver_Score.text = Mathf.CeilToInt(temp_bottom_score).ToString();
			}
			else completed_count = true;
			
			yield return null;
		}
		
		// Check if player has a highscore
		CheckScore_With_Leaderboards();
	}
	
	void CheckScore_With_Leaderboards()
	{
		
		// Player has highscore, save the players score
		SavePlayerScore();
	}
	
	void SavePlayerScore()
	{
		
	}
	
	
}
