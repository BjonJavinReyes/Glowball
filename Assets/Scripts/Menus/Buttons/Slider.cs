using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class Slider : MonoBehaviour 
{
	[SerializeField] SettingsController settings;
	public bool ResetToDefault;
	public float Node_Value = 0.0f;
	[SerializeField] GameObject Node;
	[SerializeField] GameObject Bar;
	
	Vector3 barPos;
	float   barScale;
	
	float ScreenMax, ScreenMin;
	float BarMax, BarMin, RangeMax, RangeMin;
	float BarRange, NewRange, ScreenRange;

	void Start()
	{
		DetermineScale();
	}
	
	void DetermineScale()
	{
		barScale = Bar.transform.localScale.x;
		barPos   = Bar.transform.localPosition;
		
		BarMax = barScale/2;
		BarMin = -barScale/2;
		
		RangeMax = 11;
		RangeMin = 7;
		
		// Get screen positions of bar
		Vector3 screenPos;
		screenPos = Camera.main.WorldToScreenPoint(new Vector3(barPos.x + BarMax, barPos.y, barPos.z));
		ScreenMax = screenPos.x;
		
		screenPos = Camera.main.WorldToScreenPoint(new Vector3(barPos.x + BarMin, barPos.y, barPos.z));
		ScreenMin = screenPos.x;
		
		// Set up ranges
		BarRange    = (BarMax - BarMin);
		NewRange    = (RangeMax - RangeMin);
		ScreenRange = (ScreenMax - ScreenMin);
	}
	
	public void InitiateNodeSpot()
	{
		Vector3 NodePos = Node.transform.localPosition;
		NodePos.x = (((PlayerPrefs.GetFloat("glow_ball_speed", 9.0f) - RangeMin) * BarRange) / NewRange) + BarMin;
		// Update the node value from 0-1 scale
		Node_Value = (((NodePos.x - BarMin) * NewRange) / BarRange) + RangeMin;
		Node.transform.localPosition = new Vector3(NodePos.x, NodePos.y, NodePos.z);
	}
	
	public void UpdateNodePosition(float mousePos)
	{ 
		Vector3 NodePos = Node.transform.localPosition;
		
		// Detect mouse.x pos and move node position to corresponding place on screen
		if (mousePos > ScreenMax)
			NodePos.x = BarMax;
		else if (mousePos < ScreenMin)
			NodePos.x = BarMin;
		else
			NodePos.x = (((mousePos - ScreenMin) * BarRange) / ScreenRange) + BarMin;
		
		// Update the node value from 0-1 scale
		UpdateNodeValue(NodePos.x);
		
		// Update node position on screen
		Node.transform.localPosition = new Vector3(NodePos.x, NodePos.y, NodePos.z);
	}
	
	void UpdateNodeValue(float pos_x)
	{
		// NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin
		Node_Value = (((pos_x - BarMin) * NewRange) / BarRange) + RangeMin;
		
		settings.glow_ball.GetComponent<BallController>().ball_speed = Node_Value;
	}
	
	
}

