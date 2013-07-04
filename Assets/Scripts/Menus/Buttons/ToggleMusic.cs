using UnityEngine;
using System.Collections;

public class ToggleMusic : ToggleButton 
{
	TextMesh MusicVolume;
	
	public override void Start()
	{
		base.Start();
		
		MusicVolume = gameObject.GetComponent<TextMesh>();
		
		if (sc_AudioManager.isMusicOn)
			SetMusicVolumeOn();
		else
			SetMusicVolumeOff();
	}
	
	public override void ExecuteToggle ()
	{
		base.ExecuteToggle ();
		
		if (toggleOn)
		{
			SetMusicVolumeOn();
			sc_AudioManager.Toggle_Music_Volume( true );	
		}
		else
		{
			SetMusicVolumeOff();
			sc_AudioManager.Toggle_Music_Volume( false );	
		}
	}
	
	void SetMusicVolumeOn()
	{
		toggleOn = true;
		MusicVolume.text = "On";
	}
	void SetMusicVolumeOff()
	{
		toggleOn = false;
		MusicVolume.text = "Off";
	}
}