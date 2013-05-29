using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	
	LevelManager level_man;
	BallController glow_ball;
	GameObject GameController;
	
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
		Camera.main.aspect = 9.0f/16.0f;	
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
