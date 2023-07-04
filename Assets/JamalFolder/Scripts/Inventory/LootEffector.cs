using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEffector : MonoBehaviour
{
	[SerializeField]
	private GameObject effector;

	[SerializeField]
	private Enemy enemy;

    // Update is called once per frame
    void Update()
    {
		if(enemy.IsDeath || Input.GetKeyDown(KeyCode.Z))  
		{
			Instantiate(effector, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
    }
}
