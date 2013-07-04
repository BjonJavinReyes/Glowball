using UnityEngine;
using System.Collections;

public abstract class ToggleButton : MonoBehaviour 
{
	// Script Holders
	[HideInInspector] public AudioManager sc_AudioManager;
	[HideInInspector] public ScriptHelper sc_ScriptHelper;
	
	public bool OneTimeUse;
	public bool toggleOn;
	
	public virtual void Start () 
	{
		// Attach Scripts to holders
		sc_ScriptHelper = Camera.main.GetComponent<ScriptHelper>();
		sc_AudioManager = sc_ScriptHelper.sc_AudioManager;
		
		toggleOn = true;
	}
	
	public virtual void ExecuteToggle()
	{
		toggleOn = !toggleOn;	
	}
}
