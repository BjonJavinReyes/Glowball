using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour 
{
	// Script Holders
	BallController sc_BallController;
	GameController sc_GameController;
	LevelManager   sc_LevelManager;
	ScriptHelper   sc_ScriptHelper;
	
	public GameObject TopBoundary, BottomBoundary, LeftBoundary, RightBoundary;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_BallController = sc_ScriptHelper.sc_BallController;
		sc_GameController = sc_ScriptHelper.sc_GameController;
		sc_LevelManager   = sc_ScriptHelper.sc_LevelManager; 
		
		foreach ( Transform child in transform)
		{
			if (child.name == "top")
				TopBoundary = child.gameObject;
			if (child.name == "bottom")
				BottomBoundary = child.gameObject;
			if (child.name == "left")
				LeftBoundary = child.gameObject;
			if (child.name == "right")
				RightBoundary = child.gameObject;
		}
	}
	
	void Update()
	{		
		FollowGlowBall();
	}
	
	void FollowGlowBall ()
	{
		// Boundaries y position should follow cameras y position
		LeftBoundary.transform.position = new Vector3(LeftBoundary.transform.position.x,
														sc_GameController.transform.position.y,
														LeftBoundary.transform.position.z);
		RightBoundary.transform.position = new Vector3(RightBoundary.transform.position.x,
														sc_GameController.transform.position.y,
														RightBoundary.transform.position.z);
	}
	
	public void Open_TopBoundary()
	{
		TopBoundary.collider.enabled = false;
		TopBoundary.renderer.enabled = false;
		//Debug.Log("Boundary Open");
	}
	
	public void Close_TopBoundary()
	{
		TopBoundary.collider.enabled = true;
		TopBoundary.renderer.enabled = true;
		//Debug.Log("Boundary Closed");
	}
	
	
}
