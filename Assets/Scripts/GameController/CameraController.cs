using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	// Script Holders
	BallController sc_BallController;
	LevelManager   sc_LevelManager;
	ScriptHelper   sc_ScriptHelper;
	
	
	private float camera_offset = -10;
	private float camera_drag = 0.5f;
	Vector3 current_pos, new_pos, old_pos;
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_BallController = sc_ScriptHelper.sc_BallController;
		sc_LevelManager   = sc_ScriptHelper.sc_LevelManager; 
		
		DetermineOrthographicView();
		
		// Store current camera position
		current_pos = new Vector3(transform.position.x,
								 	sc_BallController.transform.position.y,
									transform.position.z);
	}
	
	void DetermineOrthographicView()
	{
		gameObject.camera.aspect = 9.0f/16.0f;	
	}
	
	void Update()
	{
		// Update camera
		MoveCameraPosition();
	}

	void MoveCameraPosition ()
	{
		if ( transform.position.y >= (-sc_LevelManager.BallIntermissionHeight+1) && 
			transform.position.y <= 0)
			transform.position = new Vector3(0, 0, -30);
		else
			transform.position = new Vector3(transform.position.x,
											sc_BallController.transform.position.y + camera_offset,
											transform.position.z);
	}
	
	void CameraLagUpdate()
	{
		// Save old camera position
		old_pos = current_pos;
		
		// Store new camera position
		new_pos = new Vector3(transform.position.x,
								sc_BallController.transform.position.y,
								transform.position.z);
		
		// Change current position to laged position
		current_pos.y = Mathf.Lerp(old_pos.y, new_pos.y, (Time.deltaTime / camera_drag));
		
		// Set cameras position the lagged position
		transform.position = new Vector3(transform.position.x, current_pos.y, transform.position.z);
	}
}
