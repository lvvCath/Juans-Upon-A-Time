using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : Item, UseableUI	
{
	[SerializeField]
	private int health; 

	public void Use()
	{
		if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
		{
			Remove();

			Player.MyInstance.MyHealth.MyCurrentValue += health;
		}

	}
	public override string GetDescription() 
	{
		return base.GetDescription() + string.Format("\nUse: Restores <color=#ff0000>{0}</color> health", health);
	}
//	This is a health Postion
}
