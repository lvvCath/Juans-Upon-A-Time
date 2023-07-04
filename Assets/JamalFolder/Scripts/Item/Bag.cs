using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag",menuName = "Items/Bag", order = 1)]
public class Bag : Item, UseableUI
{
	private int slots;
	[SerializeField]	
	private GameObject BagPrefab;

	public BagScript MyBagScript { get; set; }

	public int MySlots
	{
		get
		{
			return slots;
		}
	}

	public void Initialize (int slots) {
		this.slots = slots;
	}

	public void Use()
	{
		if (InventoryScript.MyInstance.CanAddBag) {
			Remove();
			MyBagScript = Instantiate(BagPrefab,InventoryScript.MyInstance.transform).GetComponent<BagScript>();
			MyBagScript.AddSlots(slots);
			InventoryScript.MyInstance.AddBag(this);
		}
	}
	public override string GetDescription() 
	{
		return base.GetDescription() + string.Format("\n{0} Slot", MySlots);
	}
}
