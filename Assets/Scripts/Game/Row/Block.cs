using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public bool empty_block = false;
	public int  BlockID = -1;
	
	
	public void SetBlock_Empty()
	{
		empty_block = true;
		gameObject.renderer.enabled = false;
	}
	
	public void SetBlock_Full()
	{
		empty_block = false;
		gameObject.renderer.enabled = true;
	}	
}
