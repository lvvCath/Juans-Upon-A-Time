using System;
using UnityEngine;

class IdleState: IState
{
	private Enemy parent;

	public void Enter(Enemy parent) 
	{
		this.parent = parent;
		this.parent.MyTarget = null;
		this.parent.Reset();
	}
	public void Exit()
	{

	}
	public void Update()
	{	
		parent.MyDirection = (parent.PatrolSpots[parent.RandomSpots].transform.position - parent.transform.position).normalized;
		parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.PatrolSpots[parent.RandomSpots].position, parent.MySpeed * Time.deltaTime);

		if (Vector2.Distance(parent.transform.position, parent.PatrolSpots[parent.RandomSpots].position) < 0.2f) {
			if (parent.WaitTime <= 0) {
				parent.RandomSpots = UnityEngine.Random.Range(0, parent.PatrolSpots.Length);
				parent.WaitTime = parent.StartWaitTime;
			}
			else {
				parent.WaitTime -= Time.deltaTime;	
			}
		}

		if (parent.MyTarget != null) {
			parent.ChangeState(new FollowState());
		}
	}
}

