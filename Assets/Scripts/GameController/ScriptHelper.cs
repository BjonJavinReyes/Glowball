using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptHelper : MonoBehaviour 
{
	// Script Holders
	[HideInInspector] public AudioManager     sc_AudioManager;
	[HideInInspector] public BallController   sc_BallController;
	[HideInInspector] public BoundaryManager  sc_BoundaryManager;
	[HideInInspector] public CameraController sc_CameraController;
	[HideInInspector] public FadeToScene      sc_FadeToScene;
	[HideInInspector] public GameController   sc_GameController;
	[HideInInspector] public LevelManager     sc_LevelManager;
	[HideInInspector] public MenuSystem       sc_MenuSystem;	
	[HideInInspector] public RowManager       sc_RowManager;
	[HideInInspector] public ScoreTracker     sc_ScoreTracker;
	
	void Awake()
	{
		// Find scripts within scene
		FindScripts();
	}

	void FindScripts ()
	{
		// Find Controller GameObject
		GameObject controller =  Camera.main.gameObject;
	
		// Attach scripts that are attached to controller object
		sc_CameraController = controller.GetComponent<CameraController>();
		sc_GameController   = controller.GetComponent<GameController>();
		sc_LevelManager     = controller.GetComponent<LevelManager>();
		sc_RowManager       = controller.GetComponent<RowManager>();
		
		// Find Scripts not attached to controller object
		sc_AudioManager     = GameObject.Find("audio_manager").GetComponent<AudioManager>();
		sc_MenuSystem       = GameObject.Find("Menu").GetComponent<MenuSystem>();
		sc_FadeToScene      = GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeToScene>();
				
		if (CheckObjectExist("score_tracker"))
			sc_ScoreTracker     = GameObject.Find("score_tracker").GetComponent<ScoreTracker>();

		if (CheckObjectExist("glow_ball"))
			sc_BallController   = GameObject.Find("glow_ball").GetComponent<BallController>();
		
		if (CheckObjectExist("boundaries"))
			sc_BoundaryManager   = GameObject.Find("boundaries").GetComponent<BoundaryManager>();
		
	}
	
	bool CheckObjectExist(string objectName)
	{
		if (GameObject.Find(objectName) != null)
			return true;
		else return false;
	}
}
