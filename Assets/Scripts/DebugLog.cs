	using UnityEngine;
using System.Collections;

public class DebugLog : MonoBehaviour 
{
	[SerializeField] GUIText fpsText;
	[SerializeField] GUIText screenSizeText;
	int last_frame = 0;
	
	void Start()
	{
		screenSizeText.text = (Screen.width + "x" + Screen.height);
		InvokeRepeating("CountFps",1,1);
	}
	
	void CountFps()
	{
		int frame = Time.frameCount;
		int diff = frame - last_frame;
		last_frame = frame;
		
		fpsText.text = "FPS:" + diff.ToString();
	}
	
	void Update()
	{
		//DisplayGameTime();	
	}
	
	// Displays Game time
	float oldtime = 0;
	void DisplayGameTime()
	{
		float time = Mathf.Round(Time.timeSinceLevelLoad / 1.0f);
		if (oldtime != time)
			Debug.Log("Time: " + Mathf.Round(Time.timeSinceLevelLoad / 1.0f));
		oldtime = time;
	}
}
