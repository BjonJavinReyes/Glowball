using UnityEngine;
using System.Collections;

public class ToggleSFX : ToggleButton 
{
	TextMesh SFXVolume;
	
	public override void Start()
	{
		base.Start();
		
		SFXVolume = gameObject.GetComponent<TextMesh>();
		
		if (sc_AudioManager.isSFXOn)
			SetSFXVolumeOn();
		else
			SetSFXVolumeOff();
	}
	
	public override void ExecuteToggle ()
	{
		base.ExecuteToggle ();
		
		if (toggleOn)
		{
			SetSFXVolumeOn();
			sc_AudioManager.Toggle_SFX_Volume( true );	
		}
		else
		{
			SetSFXVolumeOff();
			sc_AudioManager.Toggle_SFX_Volume( false );	
		}
	}
	
	void SetSFXVolumeOn()
	{
		toggleOn = true;
		SFXVolume.text = "On";
	}
	void SetSFXVolumeOff()
	{
		toggleOn = false;
		SFXVolume.text = "Off";
	}
}
