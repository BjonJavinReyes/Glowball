using UnityEngine;
using System.Collections;

public class ButtonTransition : MonoBehaviour 
{
	[SerializeField] AnimationClip Clip;
	[SerializeField] bool ReverseAnimation;
	[SerializeField] bool OneTimeUse;
	[SerializeField] MenuTransition Menu_Transition;
	
	Collider buttonCollider;
	bool buttonUsed = false;
	
	void Start()
	{
		// Attach gameobjects collider
		buttonCollider = gameObject.collider;	
	}
	
	public void PerfromTransition()
	{	
		// If button can only be used once check if button was already hit
		// if hit skip the rest of the function
		if (OneTimeUse && buttonUsed)
		{
			buttonCollider.enabled = false;  // Button was already used, deactivate collider
			return;
		}
		
		buttonUsed = true;	//Button was hit, set boolean to true;
		
		//Debug.Log("Hit Button: " + gameObject.name);
		
		// Perform animation
		if (ReverseAnimation)
			Menu_Transition.ReverseAnimation(Clip.name);
		else
			Menu_Transition.PlayAnimation(Clip.name);
	}
}
