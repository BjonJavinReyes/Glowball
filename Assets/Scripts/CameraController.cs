using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	
	LevelManager level_man;
	BallController glow_ball;
	GameObject GameController;
	
	private float default_ortho_size = 17.5f;		// Based on 480x800
	public float Camera_Speed;
	private float camera_offset = -10;
	private float camera_drag = 0.5f;
	Vector3 current_pos, new_pos, old_pos;
	
	void Awake()
	{
		// Set up scripts 
		GameController = GameObject.Find("game_controller");
		level_man = GameController.GetComponent<LevelManager>();
		glow_ball = GameObject.Find("glow_ball").GetComponent<BallController>();
		
		DetermineOrthographicView();
	}
	
	void DetermineOrthographicView()
	{
		// Scale camera according to screen size
		float ortho_size = 0;
		float x = Screen.width;
		float y = Screen.height;
		
//		if (x < 320 && y < 480)
//			
//		
//		if (Screen.width == 320 &&
//			Screen.height == 480)
//			ortho_size = 15.7f;
//		if (Screen.width == 480 &&
//			Screen.height == 800)
//			ortho_size = 17.5f;
//		if (Screen.width == 480 &&
//			Screen.height == 854)
//			ortho_size = 17.5f;
//		if (Screen.width == 600 &&
//			Screen.height == 1024)
//			ortho_size = 17.5f;
//		if (Screen.width == 720 &&
//			Screen.height == 1024)
//			ortho_size = 14.9f;
		
		//var height = 2*Camera.main.orthographicSize;
  		//var width = height*Camera.main.aspect;
		
		// 240 x 320
		// 320 x 480
		// 480 x 800
		// 480 x 854
		// 540 x 960
		// 640 x 960
		// 600 x 1024
		// 720 x 1280
		// 768 x 1024
		// 768 x 1080
		// 800 x 1200
		// 800 x 1280
		
		
		
		float size = (Screen.height/2) * (Screen.width/640.0f);
	
		Camera.main.orthographicSize = Screen.height/2;		
		//Camera.main.aspect = x/y;
		Camera.main.aspect = 9.0f/16.0f;
		
		Debug.Log("Ortho: "  + Camera.main.orthographicSize);
		Debug.Log("Aspect: " + Camera.main.aspect);
		
		//ortho_size = (default_ortho_size * Screen.width) / 640;
		
		//gameObject.camera.orthographicSize = size;
		//gameObject.camera.orthographicSize = ortho_size;	
	}
	
	void Start()
	{
		// Store current camera position
		current_pos = new Vector3(transform.position.x, glow_ball.transform.position.y, transform.position.z);
	}
	
	void Update()
	{
		// Update camera
		if (!level_man.LevelCameraMove)
			FollowBallPosition();
		else
			MoveCameraPosition();
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			float x = Camera.main.aspect;
			x+=0.1f;
			Camera.main.aspect = x;
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			float x = Camera.main.aspect;
			x-=0.1f;
			Camera.main.aspect = x;
		}
	}

	void FollowBallPosition ()
	{
		transform.position = new Vector3(transform.position.x, glow_ball.transform.position.y + camera_offset, transform.position.z);
	}
	
	void MoveCameraPosition ()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * Camera_Speed), transform.position.z);
	}
	
	void CameraLagUpdate()
	{
		// Save old camera position
		old_pos = current_pos;
		
		// Store new camera position
		new_pos = new Vector3(transform.position.x, glow_ball.transform.position.y, transform.position.z);
		
		// Change current position to laged position
		current_pos.y = Mathf.Lerp(old_pos.y, new_pos.y, (Time.deltaTime / camera_drag));
		
		// Set cameras position the lagged position
		transform.position = new Vector3(transform.position.x, current_pos.y, transform.position.z);
	}
	
	public void SET_CameraSpeed(float new_speed)
	{
		Camera_Speed = new_speed;
	}
}
