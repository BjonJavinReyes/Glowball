using UnityEngine;
using System.Collections;

public class ButtonTransition : MonoBehaviour 
{
	[SerializeField] AnimationClip Clip;
	[SerializeField] bool ReverseAnimation;
	[SerializeField] MenuTransition Menu_Transition;
	
	
	public void PerfromTransition()
	{	
		Debug.Log("Hit Button: " + gameObject.name);
		
		if (ReverseAnimation)
			Menu_Transition.ReverseAnimation(Clip.name);
		else
			Menu_Transition.PlayAnimation(Clip.name);
	}
}
