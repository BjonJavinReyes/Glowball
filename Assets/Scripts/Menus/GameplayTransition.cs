using UnityEngine;
using System.Collections;

public class GameplayTransition : MenuTransition
{
	void Game_to_StartGame()
	{
		sc_GameController.hasGameStarted = true;
		PlaySoundtrack(AudioManager.Music.GAMEPLAYMUSIC_ONE);
	}
	void GameOver_CountScore()
	{
		sc_ScoreTracker.GameOver_CountScore();
	}
	void GameOver_to_Retry()
	{
		sc_ScoreTracker.Hurry_GameOver_ScoreCount = true;
		sc_FadeToScene.FadeToNewScene("Game", 3.0f);
	}
	void GameOver_to_Quit()
	{
		sc_ScoreTracker.Hurry_GameOver_ScoreCount = true;
		sc_FadeToScene.FadeToNewScene("Menu", 3.0f);
	}
}
