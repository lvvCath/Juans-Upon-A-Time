using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AttackState : IState
{
	private Enemy parent;
	private float attackColdown = 1;
	private float ExRange = 2;

	private int BasicIndex;
	public void Enter(Enemy parent) 
	{
		this.parent = parent;
	}
	public void Exit()
	{

	}
	public void Update()
	{
		if (parent.MyAttackTime >= attackColdown && !parent.isAttacking) {

			parent.MyAttackTime = 0;
			CastSkills(BasicIndex);
		}
		if (parent.MyTarget != null) {
			if (parent.MyTarget != null) {

				float distance = Vector2.Distance(parent.MyTarget.transform.position, parent.transform.position);

				if (distance >= parent.MyAttackRange + ExRange && !parent.isAttacking) {
					parent.ChangeState(new FollowState());
				}
			} 
		} 
		else 
		{
			parent.ChangeState(new IdleState());
		}
	}
	public IEnumerator Attack(int index)
	{
		Skills NewSkill = parent.MyskillBooks.CastSkill(index);
		parent.isAttacking = true;
		parent.myAnimator.SetTrigger("attack");

		yield return new WaitForSeconds (parent.myAnimator.GetCurrentAnimatorStateInfo(2).length); 

//		if (parent.MyTarget != null) {	
//			EnemySkill ea = parent.Instantiate(NewSkill.MySkillPrefab[0], parent.transform.position, parent.Quaternion.identity).GetComponent<EnemySkill>();
//			ea.Initialize(parent.MyTarget, NewSkill.MyDamage, parent.transform);
//		}

		StopAttacking();
	}
	public void CastSkills(int BasicIndex)
	{
		if(parent.MyTarget != null){
			parent.MyattackRoutine = parent.StartCoroutine(Attack(BasicIndex));
		}
	}
	public virtual void StopAttacking() {
		parent.isAttacking = false;
		parent.myAnimator.SetBool ("attack", parent.isAttacking);
		parent.myAnimator.SetBool ("attack", parent.isAttacking);

		if(parent.MyattackRoutine != null) {
			parent.StopCoroutine(parent.MyattackRoutine);	
		}
	}
}
