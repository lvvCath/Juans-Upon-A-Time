using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
	private Enemy parent;
	[SerializeField]
	private bool Attacks;

	// Start is called before the first frame update
	void Start()
	{
		parent = GetComponentInParent<Enemy>();
	} 

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) { 
			parent.SetTarget(other.transform);
			Attacks = true;
		}
	}
//	private void OnTriggerExit2D (Collider2D other) {
	//	if (other.CompareTag("BasicSkill")) { 
//			Destroy(gameObject);
//		}
	//}
}
