using UnityEngine;
using System.Collections;

public class GameOver_HighScore : MonoBehaviour 
{	
	[SerializeField] Color color_1;
	[SerializeField] Color color_2;
	
	public void Celebrate_HighScore()
	{
		// Make text color equal to first color
		transform.renderer.material.color = color_1;
		
		StartCoroutine( ScaleObject() );	// Scale text up
		StartCoroutine( ColorizeObject(color_1, color_2) );	// Flash between color_1 and color_2
	}
	
	IEnumerator ScaleObject()
	{
		Transform trans = transform;
		float time = 0;
		while (time < 1)
		{
			time += Time.deltaTime;
			
			trans.localScale = Vector3.Lerp( Vector3.zero, new Vector3(0.28f,0.28f,0.28f), time);
			yield return null;	
		}
	}
	
	IEnumerator ColorizeObject(Color c_from, Color c_to)
	{
		Transform trans = transform;
		trans.renderer.material.color = c_from;
		
		float time = 0;
		while (time < 1)
		{
			time += Time.deltaTime;
			
			trans.renderer.material.color = Color.Lerp(c_from, c_to, time);
			yield return null;	
		}
		
		trans.renderer.material.color = c_to;
		
		StartCoroutine( ColorizeObject(c_to, c_from) );	// Flash between color_1 and color_2
	}
}
