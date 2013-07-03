using UnityEngine;
using System.Collections;

public class VersionNumber : MonoBehaviour 
{	
	// Script Holders
	GameController sc_GameController;
	ScriptHelper   sc_ScriptHelper;
	
	// Text holder
	TextMesh version_text;
	string text_details = "Version Number: ";
	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper   = Camera.main.GetComponent<ScriptHelper>();
		sc_GameController = sc_ScriptHelper.sc_GameController;
		
		version_text = gameObject.GetComponent<TextMesh>();
		
		version_text.text = text_details + sc_GameController.Product_VersionNumber;
	}
}
