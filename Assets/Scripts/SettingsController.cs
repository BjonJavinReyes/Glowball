using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class SettingsController : MonoBehaviour
{
	// Script Holders
	GameController sc_GameController;
	ScriptHelper   sc_ScriptHelper;
	
	[SerializeField] ButtonTransition SensitivityButton;
	
	[SerializeField] Camera     cameraDisplay;
	[SerializeField] Transform  BallSpawnPoint;
	[SerializeField] GameObject GlowBallTestPrefab;
	[SerializeField] Slider     sensitivitySlider;
	
 	[HideInInspector] public GameObject glow_ball;
	
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_GameController = sc_ScriptHelper.sc_GameController;	
	}
	
	public void ActivateBallSettings()
	{
		sensitivitySlider.InitiateNodeSpot();
		
		// Turn button collider off
		SensitivityButton.gameObject.collider.enabled = false;
		StartCoroutine(ChangeCameraPort(true));
	}
	
	public void DeactivateBallSettings()
	{
		PlayerPrefs.SetFloat("glow_ball_speed", sensitivitySlider.Node_Value);
		
		// Turn button collider on again
		SensitivityButton.gameObject.collider.enabled = true;
		StartCoroutine( ChangeBallAlpha(false) );
	}
	
	IEnumerator ChangeCameraPort(bool forward)
	{
		Rect vp = cameraDisplay.rect;
		
		float time_delay = 1.5f;
		float time = 0;
		
		while (time < 1)
		{
			time += Time.deltaTime / time_delay;
			
			if (forward)
			{
				vp.x     = Mathf.Lerp(0.1f, 0, time);
				vp.width = Mathf.Lerp(0, 1, time);
			}
			else
			{
				vp.x     = Mathf.Lerp(0, 0.1f, time);
				vp.width = Mathf.Lerp(1, 0, time);
			}
			
			cameraDisplay.rect = new Rect(vp.x, vp.y, vp.width, vp.height);
			yield return null;
		}
		
		if (forward)
		{
			glow_ball = Instantiate(GlowBallTestPrefab, BallSpawnPoint.position, Quaternion.identity) as GameObject;
			StartCoroutine( ChangeBallAlpha(true) );
		}
		
		yield return null;	
	}
	
	IEnumerator ChangeBallAlpha( bool forward )
	{	
		if (!forward)
			sc_GameController.useGameInput = false;
		
		Color color = glow_ball.renderer.material.color;
		
		float time_delay = 1.0f;
		float time = 0;
		
		while (time < 1)
		{
			time += Time.deltaTime / time_delay;
			
			if (forward)
				color.a = Mathf.Lerp(0,1, time);
			else
				color.a = Mathf.Lerp(1,0, time);

			glow_ball.renderer.material.color = color;
			
			yield return null;
		}
		
		if (!forward)
		{
			Destroy(glow_ball);
			StartCoroutine(ChangeCameraPort(false));
		}
		else
			sc_GameController.useGameInput = true;
			
		
		yield return null;	
	}
}
