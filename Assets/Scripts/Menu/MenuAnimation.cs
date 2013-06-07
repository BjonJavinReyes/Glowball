using UnityEngine;
using System.Collections;

public class MenuAnimation : MonoBehaviour 
{
	public string AnimationName;
	
	void Start()
	{
		AnimationName = gameObject.animation.clip.name;	
	}
	
	public void AnimationOut()
	{
		animation.Play();
		gameObject.animation[AnimationName].speed = 1.0f;
	}
	
	public void AnimationIn()
	{
		animation.Play();
		gameObject.animation[AnimationName].speed = -1.0f;
	}
	
}
