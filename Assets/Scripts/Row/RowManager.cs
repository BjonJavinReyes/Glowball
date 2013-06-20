using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowManager : MonoBehaviour 
{
	// Script Holders
	BoundaryManager sc_BoundaryManager;
	LevelManager    sc_LevelManager;
	MenuSystem      sc_MenuSystem;
	ScriptHelper    sc_ScriptHelper;
	
	[SerializeField] GameObject RowPrefab;
	[SerializeField] GameObject RowParent;
	[SerializeField] GameObject RowSpawnPoint;
	
	[HideInInspector]
	public List<GameObject> RowList = new List<GameObject>();
	[HideInInspector]
	public float Time_Between_Rows;
	[HideInInspector]
	public float Row_Speed;
	
	private float new_row_height = -2.0f;
	private float row_death = 5.0f;		// Spacing after top boundary where rows are deleted
	
	public bool LevelIntermission = true;

	
	void Start()
	{
		// Attach Scripts to holders
		sc_ScriptHelper    = GameObject.FindGameObjectWithTag("Controller").GetComponent<ScriptHelper>();
		sc_BoundaryManager = sc_ScriptHelper.sc_BoundaryManager;
		sc_MenuSystem      = sc_ScriptHelper.sc_MenuSystem;
		sc_LevelManager    = sc_ScriptHelper.sc_LevelManager; 
		
		Time_Between_Rows = 2.0f;	
	}
	
	void Update()
	{
		
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Z))
		{
			GameObject row;
			row = Instantiate(RowPrefab, RowSpawnPoint.transform.position, Quaternion.identity) as GameObject;
			row.transform.parent = RowParent.transform;
			RowList.Add(row);
		}
		#endif
		
		// Update Row positions, based on speed
		if (RowList.Count > 0)
			RowUpdate();
		
	}

	IEnumerator CreateRow ()
	{
		// If gameplay is not in intermission create rows
		if (!sc_LevelManager.LevelIntermission)
		{
			//Debug.Log("Create");
			
			// Create a new row
			GameObject row;
			row = Instantiate(RowPrefab, RowSpawnPoint.transform.position, Quaternion.identity) as GameObject;
			row.transform.parent = RowParent.transform;
			
			
			// Add row to a list
			RowList.Add(row);
			
			while (true)
			{
				if (row.transform.position.y - sc_BoundaryManager.BottomBoundary.transform.position.y > new_row_height)
					break;
				
				yield return null;
			}
			
			// Spawn a new row
			StartCoroutine( CreateRow() );
		}		
	}
	
	public void SET_RowSpeed(float new_speed)
	{
		Row_Speed = new_speed;
	}
	
	void RowUpdate ()
	{			
		if (!sc_MenuSystem.isGamePlayMoving()) return;
			
		// Update each rows y position
		foreach (GameObject row in RowList)
		{
			row.gameObject.transform.position = new Vector3(row.gameObject.transform.position.x,
															row.gameObject.transform.position.y + (Time.deltaTime * Row_Speed),
												 			row.gameObject.transform.position.z);
			
			if (row.gameObject.transform.position.y > sc_BoundaryManager.TopBoundary.transform.position.y + row_death)
			{
				RowList.Remove(row);
				Destroy(row);
				break;
			}
		}
		
	}
}
