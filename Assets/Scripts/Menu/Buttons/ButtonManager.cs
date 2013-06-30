using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour 
{	
	void Update()
	{
		CheckButtonHit();
	}

	void CheckButtonHit ()
	{
		Vector3 mousePos = Input.mousePosition;
		RaycastHit hit;
		ButtonTransition transitionButton = null;
		
		// Check to see if mouse hit collider object
		if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
			transitionButton = hit.collider.GetComponent<ButtonTransition>();
		
		// Get Mouse Input
		if (Input.GetMouseButtonDown(0))
		{
			// Check to see if hit a button
			if (transitionButton != null)
				transitionButton.PerfromTransition();	
		}
	}
}
