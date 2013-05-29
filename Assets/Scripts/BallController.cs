using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour 
{
	GameObject _GameController;
	GameController game_controller;
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
		game_controller = _GameController.GetComponent<GameController>();
		level_man = _GameController.GetComponent<LevelManager>();
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
						level_man.LevelCameraMove = true;
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
	
	void OnGUI()
	{	
		GUI.Label(new Rect(Screen.width/2,Screen.height/2,Screen.width,Screen.height), Screen.width + " x " + Screen.height);
	}
}
