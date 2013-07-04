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
		Slider slider = null;
		ToggleButton toggle = null;
		
		// Check to see if mouse hit collider object
		if (Physics.Raycast(gameObject.camera.ScreenPointToRay(mousePos), out hit))
		{
			// Add button
			transitionButton = hit.collider.GetComponent<ButtonTransition>();
			
			// Add slider
			slider = hit.collider.GetComponent<Slider>();
			
			// Add toggle
			toggle = hit.collider.GetComponent<ToggleButton>();
		}
		
		// Get Mouse Input
		if (Input.GetMouseButtonDown(0))
		{
			// Check to see if hit a button
			if (transitionButton != null)
				transitionButton.PerfromTransition();
			
			if (toggle != null)
				toggle.ExecuteToggle();
		}
		
		if (Input.GetMouseButton(0))
		{
			if (slider != null)
				slider.UpdateNodePosition(mousePos.x);	
		}
	}
}
