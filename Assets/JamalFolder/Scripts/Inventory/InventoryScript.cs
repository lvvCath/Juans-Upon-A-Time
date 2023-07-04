using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
	public event ItemCountChanged itemCountChangedEvent;

	private static InventoryScript instance;

	public static InventoryScript MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<InventoryScript>();
			}
			return instance;
		}
	}	
	[SerializeField]
	private Item[] items;
	[SerializeField]
	private BagButton[] bagButtons;

	private List<Bag> bags = new List<Bag>();

	private SlotScript SelectedSlot;

	public bool CanAddBag
	{
		get { return bags.Count < 5; }
	}
	public SlotScript MySelectedSlot
	{
		get 
		{ 
			return SelectedSlot; 
		}
		set 
		{
			SelectedSlot = value;

			if (value != null)
			{
				SelectedSlot.MyIcon.color = Color.grey;
			}
		}
	}
	private void Awake()
	{
		Bag bag = (Bag)Instantiate(items[0]);
		bag.Initialize(21);
		bag.Use();
	}
	private void Update() {
		if (Input.GetKeyDown(KeyCode.B))
		{
			Bag bag = (Bag)Instantiate(items[0]);
			bag.Initialize(21);
			bag.Use();
		}	
		if (Input.GetKeyDown(KeyCode.F))
		{
			Bag bag = (Bag)Instantiate(items[0]);
			bag.Initialize(21);
			AddItem(bag);
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			HealthPotion potion = (HealthPotion)Instantiate(items[1]);
			AddItem(potion);
		}
	}
	public bool AddItem(Item item)
	{
		if (item.MyStackSize > 0)
		{
			if (PlaceInStack(item))
			{
				return true;
			}
		}
		return PlaceInEmpty(item);
	}
	public void AddBag(Bag bag)
	{
		foreach (BagButton bagButtons in bagButtons) {
			if (bagButtons.MyBag == null) {
				bagButtons.MyBag = bag;
				bags.Add(bag);
				break;
			}
		}
	}
	private bool PlaceInEmpty(Item item)
	{
		foreach (Bag bag in bags) 
		{

			if (bag.MyBagScript.AddItem(item))
			{
				OnItemCountChanged(item);
				return true;
			}
		}
		return false;
	}
	private bool PlaceInStack(Item item) {
		foreach (Bag bag in bags) 
		{
			foreach (SlotScript slots in bag.MyBagScript.MySlots) 
			{
				if (slots.StackItem(item))
				{
					OnItemCountChanged(item);
					return true;
				}
			}
		}
		return false;
	}
	public Stack<UseableUI> GetUseableUI(UseableUI type)
	{
		Stack<UseableUI> useables = new Stack<UseableUI>();

		foreach (Bag bag in bags)
		{
			foreach (SlotScript slot in bag.MyBagScript.MySlots)
			{
				if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType()) 
				{
					foreach (Item item in slot.MyItems)
					{
						useables.Push(item as UseableUI);
					}	
				}
			}	
		}
		return useables; 
	}

	public void OnItemCountChanged(Item item)
	{
		if (itemCountChangedEvent != null)
		{
			itemCountChangedEvent.Invoke(item);
		}
	}

	public int GetItemCount (string type) {
		int itemCount = 0;

		foreach (Bag bag in bags) 
		{
			foreach (SlotScript slot in bag.MyBagScript.MySlots) 
			{
				if (!slot.IsEmpty && slot.MyItem.MyItemName == type)
				{
					itemCount += slot.MyItems.Count;
				}
			}
		}
		return itemCount;
	}
//	public void OpenClose()
//	{
//		bool closeBag = bags.Find(x => !x.MyBagScript.IsOpen);
//
//		foreach (Bag bag in bags)
//		{
//			if (bag.MyBagScript.IsOpen != closeBag) {
//				bag.MyBagScript.OpenCloseBags();
//			}
//		}
//	}
}
