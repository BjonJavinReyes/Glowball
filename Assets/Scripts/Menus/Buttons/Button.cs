using UnityEngine;
using System.Collections;

public abstract class Button : MonoBehaviour 
{
	public bool OneTimeUse;
	
	public virtual void Start()
	{
	}
	
	public virtual void ExecuteButton()
	{	
	}
}
