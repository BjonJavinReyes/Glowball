using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	#region Audio Clips
	[SerializeField] AudioClip GamplayMusic_One;
	[SerializeField] AudioClip CountDown;
	[SerializeField] AudioClip CountDown_Go;
	#endregion
	
	public Dictionary<int, AudioClip> soundDictionary;
	public Dictionary<int, AudioClip> musicDictionary;
	
	public enum SoundClips
	{
		COUNTDOWN = 0,
		COUNTDOWN_GO
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
		soundDictionary.Add((int)SoundClips.COUNTDOWN_GO, CountDown_Go);
	}
	
	public void PlayAudioClip(int key)
	{
		if( soundDictionary.ContainsKey(key) && soundDictionary[key])
			audio.PlayOneShot(soundDictionary[key], 0.7f);
	}
	
	public void PlayMusic(int key)
	{
		if( musicDictionary.ContainsKey(key) && musicDictionary[key])
			audio.PlayOneShot(musicDictionary[key], 1.0f);
		
		
	}
}
