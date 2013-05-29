using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour 
{
	public List<GameObject> BlockList = new List<GameObject>();
	public int NumberOfEmpty = 0;
	public int NumberOfBlocks = 0;
	public GameObject[] EmptySections;
	
	GameObject BlockParent;
	GameObject ColliderParent;
	
	void Awake()
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
		foreach (Transform block in BlockParent.transform)
		{
			BlockList.Add(block.gameObject);
			NumberOfBlocks++;
		}
		// Add empty rows
		StartCoroutine( InsertEmptySection() );
	}
	
	IEnumerator InsertEmptySection()
	{		
		// Store number of blocks that are empty
		NumberOfEmpty = EmptyChance();
		
		EmptySections = new GameObject[NumberOfEmpty];
		int sectNum = 0;
		
		// Randomly choose which blocks in the row are empty
		int empty_spaces = NumberOfEmpty;
		while (empty_spaces > 0)
		{
			int rand = Random.Range(0, NumberOfBlocks);
			
			if (!BlockList[rand].GetComponent<Block>().empty_block)
			{	
				EmptySections[sectNum] = BlockList[rand].gameObject;
				BlockList[rand].GetComponent<Block>().SetBlock_Empty();
				sectNum++;
				empty_spaces--;	
			}
	
			yield return null;	
		}
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
