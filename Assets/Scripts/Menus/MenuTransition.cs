using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animation))]

public abstract class MenuTransition : MonoBehaviour 
{
	// Script Holders
	[HideInInspector] public AudioManager   sc_AudioManager;
	[HideInInspector] public FadeToScene    sc_FadeToScene;
	[HideInInspector] public GameController sc_GameController;
	[HideInInspector] public ScoreTracker   sc_ScoreTracker;
	ScriptHelper   sc_ScriptHelper;
	
	private Animation _Animator;
	
	public virtual void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_AudioManager   = sc_ScriptHelper.sc_AudioManager;
		sc_FadeToScene    = sc_ScriptHelper.sc_FadeToScene;
		sc_GameController = sc_ScriptHelper.sc_GameController;
		sc_ScoreTracker   = sc_ScriptHelper.sc_ScoreTracker;
		
		_Animator = gameObject.GetComponent<Animation>();
	}
	
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
	
	public void PlaySoundEffect(AudioManager.SoundClips clip_type)
	{
		sc_AudioManager.PlayAudioClip((int)clip_type);	
	}
	
	public void PlaySoundtrack(AudioManager.Music track_type)
	{
		sc_AudioManager.PlayMusic((int)track_type);	
	}
	
}
