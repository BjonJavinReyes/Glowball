using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowManager : MonoBehaviour 
{
	LevelManager level_man;
	BoundaryManager boundary_man;
	GameObject GameController;
	
	
	[SerializeField] GameObject RowPrefab;
	[SerializeField] GameObject RowParent;
	[SerializeField] GameObject RowSpawnPoint;
	public List<GameObject> RowList = new List<GameObject>();
	public float Time_Between_Rows;
	
	private float new_row_height = -2.0f;
	public float Row_Speed;
	private float row_death = 5.0f;		// Spacing after top boundary where rows are deleted
	
	public bool LevelIntermission = true;

	
	void Awake()
	{
		// Set up scripts
		GameController = GameObject.Find("game_controller");
		level_man = GameController.GetComponent<LevelManager>();
		boundary_man = GameObject.Find("Boundaries").GetComponent<BoundaryManager>();
		
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
		if (!level_man.LevelIntermission)
		{
			//Debug.Log("Create");
			
			// Create a new row
			GameObject row;
			row = Instantiate(RowPrefab, RowSpawnPoint.transform.position, Quaternion.identity) as GameObject;
			row.transform.parent = RowParent.transform;
			RowList.Add(row);
			
			while (true)
			{
				if (row.transform.position.y - boundary_man.BottomBoundary.transform.position.y > new_row_height)
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
		// Update each rows y position
		foreach (GameObject row in RowList)
		{
			row.gameObject.transform.position = new Vector3(row.gameObject.transform.position.x,
															row.gameObject.transform.position.y + (Time.deltaTime * Row_Speed),
												 			row.gameObject.transform.position.z);
			
			if (row.gameObject.transform.position.y > boundary_man.TopBoundary.transform.position.y + row_death)
			{
				RowList.Remove(row);
				Destroy(row);
				break;
			}
		}
	}
}
