using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour 
{
	GameObject _GameController;
	BallController glow_ball;
	LevelManager level_man;
	GameController game_controller;
	
	public GameObject TopBoundary, BottomBoundary, LeftBoundary, RightBoundary;
	
	void Awake()
	{
		_GameController = GameObject.Find("game_controller");
		game_controller = _GameController.GetComponent<GameController>();
		level_man = _GameController.GetComponent<LevelManager>();
		glow_ball = GameObject.Find("glow_ball").GetComponent<BallController>();
		
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
		LeftBoundary.transform.position = new Vector3(LeftBoundary.transform.position.x, game_controller.transform.position.y, LeftBoundary.transform.position.z);
		RightBoundary.transform.position = new Vector3(RightBoundary.transform.position.x, game_controller.transform.position.y, RightBoundary.transform.position.z);
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
