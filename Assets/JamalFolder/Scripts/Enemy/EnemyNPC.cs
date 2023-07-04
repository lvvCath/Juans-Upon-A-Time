using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : Character, Ienteractable
{
	
	public virtual void DeSelect() {
		
	}
	public virtual Transform Select() {
		return HitBox;
	}
	public virtual void Interact()
	{
		
	}
	public virtual void StopInteract()
	{

	}
}
