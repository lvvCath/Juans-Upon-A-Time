using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : IState
{
	private Enemy parent;

	public void Enter(Enemy parent) 
	{
		this.parent = parent;
	}
	public void Exit()
	{
		parent.MyDirection = Vector2.zero;
		parent.Reset();	
	}
	public void Update()
	{
		parent.MyDirection = (parent.MyHomePosition - parent.transform.position).normalized;

		parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyHomePosition, parent.MySpeed * Time.deltaTime);

		float distance = Vector2.Distance(parent.MyHomePosition, parent.transform.position);

		if (distance <= 0) {
			parent.ChangeState(new IdleState());
		}
	}
}
