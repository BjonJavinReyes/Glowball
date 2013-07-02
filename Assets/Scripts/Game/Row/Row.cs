using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour 
{
	public List<GameObject> BlockList = new List<GameObject>();
	public int NumberOfEmpty = 0;
	public int NumberOfBlocks = 0;
	public GameObject[] EmptySections;
	public int[] EmptySectionNum;
	
	Vector3 BlockSize;	
	
	GameObject BlockParent;
	GameObject ColliderParent;
	
	void Start()
	{
		// Get parent gameObjects for Blocks and Colliders
		foreach(Transform child in transform)
		{
			if (child.name == "blocks")
				BlockParent = child.gameObject;
			
			if (child.name == "colliders")
				ColliderParent = child.gameObject;	
		}
		
		// Get number of blocks in entire row and add block to block list
		int id = 0;
		foreach (Transform block in BlockParent.transform)
		{
			BlockList.Add(block.gameObject);
			block.GetComponent<Block>().BlockID = id;
			id++;
			NumberOfBlocks++;
		}
		
		// Store size of blocks
		BlockSize = BlockList[0].transform.localScale;
		
		// Add empty rows
		StartCoroutine( InsertEmptySection() );
	}
	
	IEnumerator InsertEmptySection()
	{		
		// Store number of blocks that are empty
		NumberOfEmpty = EmptyChance();
		
		// Set up array sizes for section nums and blocks
		EmptySections = new GameObject[NumberOfEmpty];
		EmptySectionNum = new int[NumberOfEmpty];
		
		int sectNum = 0;
		
		// Randomly choose which blocks in the row are empty
		int empty_spaces = NumberOfEmpty;
		while (empty_spaces > 0)
		{
			int rand = Random.Range(0, NumberOfBlocks);
			
			if (!BlockList[rand].GetComponent<Block>().empty_block)
			{	
				EmptySections[sectNum] = BlockList[rand].gameObject;		// Store Empty Block GameObject into an array of empties
				EmptySectionNum[sectNum] = rand;							// Store the blocks id num in the row
				BlockList[rand].GetComponent<Block>().SetBlock_Empty();		// Set current blocks renerer off so it is blank
				sectNum++;
				empty_spaces--;	
			}
	
			yield return null;	
		}
		
		// Sort the empty section array
		System.Array.Sort(EmptySectionNum);	
		StartCoroutine( InsertColliders() );
	}
	
	IEnumerator InsertColliders()
	{		
		int collider_num = 1;
		
		// Create first row collider (everything to the right of the first empty)
		
		if (EmptySectionNum[0] != 0)
		{	
			Vector3 size = new Vector3((BlockSize.x * EmptySectionNum[0]), BlockSize.y, BlockSize.z);
			Vector3 center = new Vector3( (NumberOfBlocks - EmptySectionNum[0]), 0, 0);
			
			AddCollider(collider_num, size, center);
			collider_num++;
		}
				
		if (NumberOfBlocks != 1)
		{
			// Go through each middle block and add colliders
			for (int i = 1; i < NumberOfEmpty; i++)
			{	
				Vector3 size = new Vector3( BlockSize.x * ((EmptySectionNum[i] - EmptySectionNum[i-1])-1), BlockSize.y, BlockSize.z);
				Vector3 center = new Vector3( (NumberOfBlocks - EmptySectionNum[i] - EmptySectionNum[i-1]-1), 0, 0);
				
				// Only add a collider if the current empty space and the previous 
				// empty space are not connecting
				//  Good =>   ----_---_--
				//  Bad  =>   --__-_--_--
				if ( EmptySectionNum[i] - EmptySectionNum[i-1] != 1 )
				{
					AddCollider(collider_num, size, center);
					collider_num++;
				}
		
				yield return null;	
			}
		}
		
		// End of row collider
		if (EmptySectionNum[NumberOfEmpty-1] != 10)
		{
			Vector3 size = new Vector3( BlockSize.x * ((NumberOfBlocks - EmptySectionNum[NumberOfEmpty-1])-1), BlockSize.y, BlockSize.z);
			Vector3 center = new Vector3( -(EmptySectionNum[NumberOfEmpty-1] + 1), 0, 0);
			AddCollider(collider_num, size, center);
		}
		
		yield return null;
	}
	
	void AddCollider(int collide_num, Vector3 size, Vector3 center)
	{
		// Create collider gameObject
		GameObject colGO = new GameObject();
		colGO.transform.position = gameObject.transform.position;
		colGO.transform.parent = ColliderParent.transform;
		colGO.name = ("collider_" + collide_num.ToString());
		
		// Create a collider on that object
		colGO.AddComponent<BoxCollider>();
		BoxCollider boxCol = colGO.GetComponent<BoxCollider>();
		
		// Set box colliders size and center point
		boxCol.size = size;
		boxCol.center = center;
	}
	
	int EmptyChance()
	{
		// Chance to get 4 empties  -  2%      Maximum Empty
		// Chance to get 3 empties  -  10%
		// Chance to get 2 empties  -  35%
		// Chance to get 1 empties  -  100%

		int empties = 0;
		int chance = Random.Range(0, 99);		// chances are 0 - 99, 100 numbers
		
		if (chance < 2)
			empties = 4;
		else if (chance < 10)
			empties = 3;
		else if (chance < 35)
			empties = 2;
		else
			empties = 1;
		
		return empties;
	}
	
	void Update()
	{
			
	}
}
