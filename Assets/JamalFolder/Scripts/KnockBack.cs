using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
//	private Rigidbody2D myRigidbody;
	private float Knock;

	private void OnTriggerEnter2D (Collider2D Target) {
		if (Target.CompareTag("HitBox")) { 	
			Rigidbody2D enemy = Target.GetComponent<Rigidbody2D>();
			if (enemy != null) {
				enemy.isKinematic = false;
				Vector2 difference = enemy.transform.position - transform.position; 
				difference = difference.normalized * Knock;
				enemy.AddForce(difference, ForceMode2D.Impulse);
				enemy.isKinematic = true;
			}
		}
	}
//	private void OnTriggerExit2D (Collider2D Target) {
//		if (Target.CompareTag("HitBox")) { 	
//			
//		}
//	}
}
