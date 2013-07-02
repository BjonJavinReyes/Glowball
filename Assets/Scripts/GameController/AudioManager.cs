using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	#region Audio Clips
	//////////////////////////////////////////
	// Music Tracks
	[SerializeField] AudioClip GamplayMusic_One;
	
	//////////////////////////////////////////
	// Sound Effects
	[SerializeField] AudioClip CountDown;
	[SerializeField] AudioClip Menu_Select;
	[SerializeField] AudioClip Menu_Transition_In;
	[SerializeField] AudioClip Menu_Transition_Out;
	#endregion
	
	public bool  SoundEffects_On;
	public bool  Music_On;
	
	public float MasterVolume;
	
	public Dictionary<int, AudioClip> soundDictionary;
	public Dictionary<int, AudioClip> musicDictionary;
	
	public enum SoundClips
	{
		COUNTDOWN = 0,
		MENU_SELECT,
		MENU_TRANSITION_IN,
		MENU_TRANSITION_OUT
	}
	
	public enum Music
	{
		GAMEPLAYMUSIC_ONE = 0,
	}
	
	void Start()
	{
		// Set up sound dictionary
		soundDictionary = new Dictionary<int, AudioClip>();
		musicDictionary = new Dictionary<int, AudioClip>();
		
		// Add music clips to dictionary
		musicDictionary.Add((int)Music.GAMEPLAYMUSIC_ONE, GamplayMusic_One);
		
		// Add audio clips to dictionary
		soundDictionary.Add((int)SoundClips.COUNTDOWN, CountDown);
		soundDictionary.Add((int)SoundClips.MENU_SELECT, Menu_Select);
		soundDictionary.Add((int)SoundClips.MENU_TRANSITION_IN, Menu_Transition_In);
		soundDictionary.Add((int)SoundClips.MENU_TRANSITION_OUT, Menu_Transition_Out);
		
		// Set Audio settings to on for both soundeffects and music
		SoundEffects_On = true;
		Music_On = true;
		
		// Set Master Volume
		MasterVolume = gameObject.audio.volume;
	}
	
	void Update()
	{
		gameObject.audio.volume = MasterVolume;	
	}
	
	public void PlayAudioClip(int key)
	{
		//Debug.Log("Playing Audio Clip:  " + soundDictionary[key].ToString());
		
		// If sound effects is turned off, don't play anything
		if (!SoundEffects_On) return;
		
		// If dictionary contains sound clip play it
		if( soundDictionary.ContainsKey(key) && soundDictionary[key])
			audio.PlayOneShot(soundDictionary[key], 0.5f);
		
		//Debug.Log(soundDictionary[key].ToString() + " has been played!");
	}
	
	public void PlayMusic(int key)
	{
		// If music is turned off, don't play anything
		if (!Music_On) return;
		
		// If dictionary contains audio track play it
		if( musicDictionary.ContainsKey(key) && musicDictionary[key])
			audio.PlayOneShot(musicDictionary[key], 0.8f);
		
	}
}
