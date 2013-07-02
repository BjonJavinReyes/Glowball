using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animation))]

public class MenuTransition : MonoBehaviour 
{
	// Script Holders
	AudioManager   sc_AudioManager;
	FadeToScene    sc_FadeToScene;
	GameController sc_GameController;
	ScoreTracker   sc_ScoreTracker;
	ScriptHelper   sc_ScriptHelper;
	
	public bool playMenuEntrance;
	
	Animation _Animator;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_AudioManager   = sc_ScriptHelper.sc_AudioManager;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		sc_GameController = sc_ScriptHelper.sc_GameController;
		sc_ScoreTracker   = sc_ScriptHelper.sc_ScoreTracker;
		
		_Animator = gameObject.GetComponent<Animation>();
		
		if (playMenuEntrance)
			Transition_to_MainMenu("main_entrance");
	}
	
	#region Animations
	public void PlayAnimation(string clip_name)
	{
		if (_Animator.isPlaying) return;
		
		PlaySoundEffect(AudioManager.SoundClips.MENU_SELECT);
		
		_Animator.animation[clip_name].speed = 1.0f;
		_Animator.Play(clip_name);
	}
	public void ReverseAnimation(string clip_name)
	{
		if (_Animator.isPlaying) return;
		
		PlaySoundEffect(AudioManager.SoundClips.MENU_SELECT);
		
		_Animator.animation[clip_name].normalizedTime = 1.0f;
		_Animator.animation[clip_name].speed = -1.0f;
		_Animator.Play(clip_name);
	}
	#endregion
	
	#region Main Menu Options
	void Menu_to_Game()
	{
		sc_FadeToScene.FadeToNewScene("Game", 1.5f);
	}
	void Menu_to_Leaderboards()
	{
		TransitionFromMenu("Leaderboards Transition", "leaderboards");
	}
	void Menu_to_Instructions()
	{
		TransitionFromMenu("Instructions Transition", "instructions");	
	}
	void Menu_to_Settings()
	{
		TransitionFromMenu("Settings Transition", "settings");	
	}
	void Menu_to_Information()
	{
		TransitionFromMenu("Information Transition", "information");	
	}
	
	
	private void TransitionFromMenu(string transition, string clip_name)
	{
		GameObject go = GameObject.Find(transition);
		Animation anim = go.GetComponent<Animation>();
		if (anim.isPlaying) return;
		
		anim.animation[clip_name].speed = 1.0f;
		anim.Play(clip_name);
	}
	void Transition_to_MainMenu(string clip_name)
	{
		GameObject go = GameObject.Find("Main Menu Transition");
		Animation anim = go.GetComponent<Animation>();
		if (anim.isPlaying) return;
		
		anim.animation[clip_name].normalizedTime = 1.0f;
		anim.animation[clip_name].speed = -1.0f;
		anim.Play(clip_name);
	}
	#endregion
	
	#region Game Menu Options
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
	#endregion
	
	void PlaySoundEffect(AudioManager.SoundClips clip_type)
	{
		sc_AudioManager.PlayAudioClip((int)clip_type);	
	}
	
	void PlaySoundtrack(AudioManager.Music track_type)
	{
		sc_AudioManager.PlayMusic((int)track_type);	
	}
	
}
