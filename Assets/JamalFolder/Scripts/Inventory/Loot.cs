using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	[SerializeField]
	private GameObject Items; 

	[SerializeField]
	private GameObject Point; 

	[SerializeField]
	private Enemy enemy;

	void Update()
	{
		if (enemy.IsDeath || Input.GetKeyDown(KeyCode.Z)) 
		{
			Instantiate(Items, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
		if (enemy.IsDeath || Input.GetKeyDown(KeyCode.Z)) 
		{
			Instantiate(Point, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}