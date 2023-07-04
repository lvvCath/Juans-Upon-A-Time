using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;

	private EnemyNPC currentTarget;

    // Update is called once per frame
    void Update()
    {
//		ClickTarget();
    }
//	private void ClickTarget() {
//		if (Input.GetMouseButtonDown(0)) {
//			
//			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,512);
//
//			if (hit.collider != null) {
//				if (currentTarget != null){
//					currentTarget.DeSelect();
//				}
//				currentTarget = hit.collider.GetComponent<EnemyNPC>();
//				ObjectTarget = hit.collider.GetComponent<Objects>();
//
//				player.MyTarget = currentTarget.Select();
//
//			} else {
//				if (currentTarget != null) {
//					currentTarget.DeSelect();
//				} 
//				currentTarget = null;
//				player.MyTarget = null;
//			} 
//		}
//	}
	private void OnTriggerEnter2D (Collider2D SetTarget) {
		if (SetTarget.CompareTag("Enemy")) { 
			Vector3 targetDirection = (SetTarget.transform.position - transform.position).normalized;
			Debug.DrawRay (transform.position, targetDirection, Color.blue);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance (transform.position, SetTarget.transform.position),512);
			if (hit.collider != null) {
				if (currentTarget != null) {
					currentTarget.DeSelect();
				}
				currentTarget = hit.collider.GetComponent<EnemyNPC>();

				player.MyTarget = currentTarget.Select();

				UIManager.MyInstance.ActivateTargetFrame(currentTarget);

			} else {

				if (currentTarget != null) {
					currentTarget.DeSelect();
				}
				currentTarget = null;
				player.MyTarget = null;

			}
		} else if (SetTarget.CompareTag("Object")) { 
			Vector3 targetDirection = (SetTarget.transform.position - transform.position).normalized;
			Debug.DrawRay (transform.position, targetDirection, Color.blue);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance (transform.position, SetTarget.transform.position),512);
			if (hit.collider != null) {
				if (hit.collider.tag == "Object") {
					player.MyTarget = hit.transform;
				} 
			} else {
				player.MyTarget = null;
			}
		}
	}
	private void OnTriggerExit2D (Collider2D SetTarget) {
		if (SetTarget.CompareTag("Enemy")) { 	
			UIManager.MyInstance.DeactivateTargetFrame();
			Vector3 targetDirection = (SetTarget.transform.position - transform.position).normalized;
			Debug.DrawRay (transform.position, targetDirection, Color.blue);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance (transform.position, SetTarget.transform.position),512);
			SetTarget.GetComponent<Enemy>().DeSelect();
			if (hit.collider == null) {
				currentTarget = null;
				player.MyTarget = null;
			}
		}
	}
}
