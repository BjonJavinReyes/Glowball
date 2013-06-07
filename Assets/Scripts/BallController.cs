using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour 
{
	GameObject _GameController;
	GameController game_controller;
	BoundaryManager boundary_man;
	LevelManager level_man;
	
	private float ball_drag = 2;
	private float ball_speed = 28.0f;
	private float dead_speed = 0.1f;
	private float raycast_height = 30;
	private float debug_dir = 0f;
	
	void Awake()
	{
		// Store Game Controller
		_GameController = GameObject.Find("game_controller");
		boundary_man = GameObject.Find("Boundaries").GetComponent<BoundaryManager>();
		game_controller = _GameController.GetComponent<GameController>();
		level_man = _GameController.GetComponent<LevelManager>();
		
		//gameObject.transform.position = new Vector3(-100, level_man.BallIntermissionHeight, gameObject.transform.position.z);
		SET_BallToIntermission();
	}
	
	
	void Update()
	{
		BallDrag();
		BallMovement();
	}
	
	void BallDrag()
	{
		// When in intermission slow ball fall by adding drag
		if (level_man.LevelIntermission)
			gameObject.rigidbody.drag = ball_drag;
		else
		{
			if (gameObject.rigidbody.drag != 0)
			{
				// Detect if raycast hits a row, when it does turn off drag
				RaycastHit hit;
				Ray landing_ray = new Ray(transform.position, Vector3.down);
				
				if (Physics.Raycast(landing_ray, out hit, raycast_height))
				{
					if (hit.collider.tag == "Row")
					{
						boundary_man.Close_TopBoundary();
						gameObject.rigidbody.drag = 0;
					}
				}
			}
		}
	}
	
	void BallMovement()
	{
		Vector3 force = Vector3.zero;

		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.A))
			debug_dir = -1;
		
		if (Input.GetKeyDown(KeyCode.S))
			debug_dir = 0;
		
		if (Input.GetKeyDown(KeyCode.D))
			debug_dir = 1;
		
		if (Input.GetKeyDown(KeyCode.Q))
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, level_man.BallIntermissionHeight, gameObject.transform.position.z);
		
		force = new Vector3(debug_dir * ball_speed, 0,0);
		#else
		float xdir = game_controller.Accelerometer.x;
		if ( Mathf.Abs(xdir) < dead_speed) return;
		if ( Mathf.Abs(xdir) > dead_speed && Mathf.Abs(xdir) < 0.5f)
		{
			if (xdir < 0)
				xdir = -0.5f;
			else
				xdir = 0.5f;
		}
		force = new Vector3(xdir * ball_speed, 0,0);
		#endif
			
		// Apply force to ball
		gameObject.rigidbody.AddForce(force, ForceMode.Acceleration);
	}
	
	public void SET_BallToIntermission()
	{
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, -level_man.BallIntermissionHeight, gameObject.transform.position.z);	
	}
	
	public void SET_BallToGameField()
	{		
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, level_man.BallIntermissionHeight, gameObject.transform.position.z);	
	}
	
}
