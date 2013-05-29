using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public bool empty_block = false;
	
	
	public void SetBlock_Empty()
	{
		empty_block = true;
		gameObject.renderer.enabled = false;
		gameObject.collider.enabled = false;
	}
	
	public void SetBlock_Full()
	{
		empty_block = false;
		gameObject.renderer.enabled = true;
		gameObject.collider.enabled = true;
	}	
}
