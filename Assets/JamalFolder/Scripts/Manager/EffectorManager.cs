using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorManager : MonoBehaviour
{
	[SerializeField]
	private GameObject effector;

	[SerializeField]
	private Enemy enemy;

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.M))  
		{
			Instantiate(effector, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}
