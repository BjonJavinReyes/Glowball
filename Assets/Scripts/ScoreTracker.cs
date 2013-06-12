using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	GameObject theGameController;
	LevelManager level_man;
	MenuSystem  menu;
	GUIText score_text;
	float game_score = 0;
	
	private float game_time = 0;
	float time_per_point = 0.1f;
	
	int score_increment = 10;
	
	void Awake()
	{
		// Find items in heirarchy
		theGameController = GameObject.Find("game_controller");
		menu = GameObject.Find("Menu").GetComponent<MenuSystem>();
		level_man =  theGameController.GetComponent<LevelManager>();
		score_text = gameObject.guiText;
		score_text.text = "0";
	}
	
	void Update()
	{
		if (menu.Current_MenuScene == MenuSystem.MenuScene.GAME_PLAYING)
		{
			if (!level_man.LevelIntermission)
			{	
				
				if (game_time < 1)
				{
					game_time += Time.deltaTime / time_per_point;
				}
				else
				{
					game_score += score_increment;
					score_text.text = game_score.ToString();
					game_time = 0;
				}
				
			}
		}
	}
	
	
}
