using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
	[SerializeField]
	private float thrust;

	private void OnTriggerEnter2D (Collider2D BasicSkill) {
		if (BasicSkill.CompareTag("HitBox")) { 	
			TimeDestroy();
		}
		if (BasicSkill.gameObject.CompareTag("HitBox")) {
			Rigidbody2D Enemy = BasicSkill.GetComponent<Rigidbody2D>();
				if (Enemy != null) {
					Enemy.isKinematic = false;
					Vector2 difference = Enemy.transform.position - transform.position;
					difference = difference.normalized * thrust;
					Enemy.AddForce(difference, ForceMode2D.Impulse);
					StartCoroutine(KnockCo(Enemy));
				}
			TimeDestroy();
		}
		else if (BasicSkill.CompareTag("Object")) {
			BasicSkill.GetComponent<Objects>().SmashObject();

			TimeDestroy();

		}
	}
	private IEnumerator KnockCo(Rigidbody2D Enemy) {
		if (Enemy != null) {
			yield return new WaitForSeconds(.1f);
			Enemy.velocity = Vector2.zero;
			Enemy.isKinematic = true;
		}
	}
	private IEnumerator BasicAttact() {
		yield return new WaitForSeconds(.01f);
	}
	private IEnumerator Skills() {
		yield return new WaitForSeconds(.5f);
		Destroy(gameObject);
	}
	private void TimeDestroy()
	{
		

		if (SkillBooks.MyInstance.MySkill[0].MySkillPrefab[0] || SkillBooks.MyInstance.MySkill[0].MySkillPrefab[1] || SkillBooks.MyInstance.MySkill[0].MySkillPrefab[2] || SkillBooks.MyInstance.MySkill[0].MySkillPrefab[3])
		{
			StartCoroutine(BasicAttact());
		}
		if (SkillBooks.MyInstance.MySkill[1].MySkillPrefab[0])
		{
			StartCoroutine(Skills());
		}
	}
}
