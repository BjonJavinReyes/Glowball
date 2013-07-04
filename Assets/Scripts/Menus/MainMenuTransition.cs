using UnityEngine;
using System.Collections;

public class MainMenuTransition : MenuTransition 
{
	public override void Start()
	{
		// Call base class to get initial variable setup like scripts
		base.Start(); 
		
		if (gameObject.name == "Main Menu Transition")
			StartCoroutine( DelayOpening() );
	}
	
	IEnumerator DelayOpening()
	{
		// Wait for Fade in to complete
		yield return new WaitForSeconds(2.0f);
		Transition_to_MainMenu("main_entrance");
	}
	
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
	
	void Sensitivity_to_Settings()
	{
		// Deactivate the scene
		gameObject.GetComponentInChildren<SettingsController>().DeactivateBallSettings();
	}
	
	void Settings_to_Sensitivity()
	{
		// Activate the scene
		gameObject.GetComponentInChildren<SettingsController>().ActivateBallSettings();
	}
	
	void TransitionFromMenu(string transition, string clip_name)
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

}
