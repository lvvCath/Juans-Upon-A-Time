using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPoints : MonoBehaviour
{
	private BoxCollider2D MyBoxCollider;

	[SerializeField]
	private int health; 

	[SerializeField]
	private GameObject Effector; 
	void Start () {
		MyBoxCollider = GetComponent<BoxCollider2D>();
	}
	void Update () {
		if (Player.MyInstance.MyHealth.MyCurrentValue == Player.MyInstance.MyHealth.MyMaxValue) {
			Effector.SetActive(false);

		} 
		else {
			Effector.SetActive(true);
		}
	}
	public void UseHPpoints()
	{
		if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
		{
			Player.MyInstance.MyHealth.MyCurrentValue += health;
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D (Collider2D points) {
		if (points.CompareTag("Simulation")) { 	
			UseHPpoints();
		} 
		if (points.CompareTag("Simulation") && Player.MyInstance.MyHealth.MyCurrentValue == Player.MyInstance.MyHealth.MyMaxValue)
		{
			MyBoxCollider.enabled = false;
		}
	}
	private void OnTriggerExit2D (Collider2D points) {
		if (points.CompareTag("Simulation")) { 	
			MyBoxCollider.enabled = true;
		} 
	}
}
