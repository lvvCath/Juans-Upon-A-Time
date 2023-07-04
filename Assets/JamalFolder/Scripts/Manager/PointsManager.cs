using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
	private Rigidbody2D myRigidbody;

	[SerializeField]	
	private GameObject LootCanvas;
	 
	private void OnTriggerExit2D (Collider2D points) {
		if (points.CompareTag("Points")) { 	
			Destroy(LootCanvas);
		} 
	}
}
