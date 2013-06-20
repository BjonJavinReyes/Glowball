using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour 
{
	// Script Holders
	BoundaryManager sc_BoundaryManager;
	GameController  sc_GameController;
	LevelManager    sc_LevelManager;
	ScriptHelper    sc_ScriptHelper;
	
	private float ball_drag = 2;
	private float ball_speed = 28.0f;
	private float dead_speed = 0.1f;
	private float raycast_height = 30;
	private float debug_dir = 0f;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper    = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_BoundaryManager = sc_ScriptHelper.sc_BoundaryManager;
		sc_GameController  = sc_ScriptHelper.sc_GameController;
		sc_LevelManager    = sc_ScriptHelper.sc_LevelManager; 
		
		// Set the ball to the first intermission height
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
		if (sc_LevelManager.LevelIntermission)
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
					// Detect if colliders is a row
					if (hit.collider.tag == "Row")
					{
						sc_BoundaryManager.Close_TopBoundary();
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
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,
														sc_LevelManager.BallIntermissionHeight,
														gameObject.transform.position.z);
		
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
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,
													-sc_LevelManager.BallIntermissionHeight,
													gameObject.transform.position.z);	
	}
	
	public void SET_BallToGameField()
	{		
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,
													sc_LevelManager.BallIntermissionHeight,
													gameObject.transform.position.z);	
	}
	
	public void FreezeBall()
	{
		gameObject.rigidbody.isKinematic = true;	
	}
	
	public void UnFreezeBall()
	{
		gameObject.rigidbody.isKinematic = false;	
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if (sc_LevelManager.LevelIntermission) return;
		
		if (hit.tag == "Top Boundary")
			sc_GameController.GameOver();
	}
	
}
