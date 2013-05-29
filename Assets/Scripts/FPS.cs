using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour 
{
	[SerializeField] GUIText fps_text;
	int last_frame = 0;
	
	void Start()
	{
		InvokeRepeating("CountFps",1,1);
	}
	
	void CountFps()
	{
		int frame = Time.frameCount;
		int diff = frame - last_frame;
		last_frame = frame;
		
		fps_text.text = "FPS:" + diff.ToString();
	}
}
