using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemQuality{Common, Uncommon, Rare, Epec}

public abstract class Item : ScriptableObject, IMoveable, Describable
{
	[SerializeField]	
	private Sprite Icon;

	[SerializeField]	
	private string ItemName;

	[SerializeField]
	private ItemQuality quality;

	[SerializeField]
	private int stackSize;

	private SlotScript Slot;

	public Sprite MyIcon 
	{
		get
		{
			return Icon;
		}
	}
	public string MyItemName
	{
		get
		{
			return ItemName;
		}
	}
//	public String MyItemName 
//	{
//		get
//		{
//			return ItemName;
//		}
//	}
	public int MyStackSize 
	{
		get
		{
			return stackSize;
		}
	}
	public SlotScript MySlot
	{
		get
		{
			return Slot;
		}
		set
		{
			Slot = value;
		}
	}
	public void Remove()
	{
		if (MySlot != null)
		{
			MySlot.RemoveItem(this);
		}
	}
	public virtual string GetDescription()
	{
		string color = string.Empty;

		switch (quality)
		{
		case ItemQuality.Common:
			color = "#d6d6d6";
			break;
		case ItemQuality.Uncommon:
			color = "#00ff00ff";
			break;
		case ItemQuality.Rare:
			color = "#0000ffff";
			break;
		case ItemQuality.Epec:
			color = "#800080ff";
			break;
		}
			return string.Format("<color={0}>{1}</color>", color, ItemName);
	}
}
