using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour,IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
	private StackManager<Item> items = new StackManager<Item>();

	[SerializeField]
	private Image icon;
	[SerializeField]
	private Text stackSize;

	public bool IsFull
	{
		get 
		{
			if (IsEmpty || MyCount < MyItem.MyStackSize)
			{
				return false;
			}

			return true;
		}
	}
	public bool IsEmpty
	{
		get 
		{
			return MyItems.Count == 0;
		}
	}
	public Item MyItem 
	{
		get
		{
			if (!IsEmpty) {
				return MyItems.Peek(); 
			}
			return null;
		}
	}
	public Image MyIcon 
	{
		get
		{
			return icon;
		}
		set
		{
			icon = value;
		}
	}
	public int MyCount 
	{
		get {return MyItems.Count;}
	}
	public Text MyStackText
	{
		get 
		{
			return stackSize;
		}
	}
	public  StackManager<Item> MyItems
	{
		get 
		{
			return items;
		}
	}
	private void Awake()
	{
		MyItems.OnPop += new UpdateStackEvent(UpdateSlots);
		MyItems.OnPush += new UpdateStackEvent(UpdateSlots);
		MyItems.OnClear += new UpdateStackEvent(UpdateSlots);
	}
	public bool AddItem(Item item)
	{
		MyItems.Push(item);
		icon.sprite = item.MyIcon;
		icon.color = Color.white;
		item.MySlot = this;
		return true;
	}
	public bool AddItems(StackManager<Item> newItem)
	{
		if (IsEmpty || newItem.Peek().GetType() == MyItem.GetType())
		{
			int count = newItem.Count;
			for (int i = 0; i < count; i++)
			{
				if (IsFull) {
					return false;
				}
				AddItem(newItem.Pop());
			}
			return true;
		}
		return false;
	}

	public void RemoveItem(Item item) 
	{
		if (!IsEmpty)
		{
			InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
		}	
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (InventoryScript.MyInstance.MySelectedSlot == null && !IsEmpty) {
				HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
				InventoryScript.MyInstance.MySelectedSlot = this;	
			} else if (InventoryScript.MyInstance.MySelectedSlot != null) {
				if (PutItemBack() || MargeItems(InventoryScript.MyInstance.MySelectedSlot) || SwapItem(InventoryScript.MyInstance.MySelectedSlot) || AddItems(InventoryScript.MyInstance.MySelectedSlot.MyItems)) {
					HandScript.MyInstance.DropItem();
					InventoryScript.MyInstance.MySelectedSlot = null;
				}
			}
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			UseItem();
		}
	}
	public void epuipItems() {
		HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
	}
	public void UseItem()
	{
		if (MyItem is UseableUI) {
			(MyItem as UseableUI).Use();
		}
	}
	public bool StackItem(Item item)
	{
		if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize) 
		{
			MyItems.Push(item);
			item.MySlot = this;
			return true;
		}
		return false;
	}
	private void UpdateSlots()
	{
		UIManager.MyInstance.UpdateStackSize(this);
	}
	public bool PutItemBack() {
		if (InventoryScript.MyInstance.MySelectedSlot == this) 
		{
			InventoryScript.MyInstance.MySelectedSlot.MyIcon.color = Color.white;
			return true;
		}
		return false;
	}
	private bool SwapItem(SlotScript from)
	{
		if (IsEmpty)
		{
			return false;
		}
		if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
		{
			StackManager<Item> tmpFrom = new StackManager<Item>(from.MyItems);
			from.MyItems.Clear();
			from.AddItems(items);
			MyItems.Clear();
			AddItems(tmpFrom);
			return true;
		}
		return false;
	}

	public bool MargeItems(SlotScript from)
	{
		if (IsEmpty) 
		{
			return false;
		}
		if (from.MyItem.GetType() == MyItem.GetType() && !IsFull) 
		{
			int free = MyItem.MyStackSize - MyCount;

			for (int i = 0; i < free ; i++)
			{
				AddItem(from.MyItems.Pop());
			}
			return true;
		}
		return false;
	}
	public void Clear()
	{
		int initialCount = MyItems.Count;
		if (initialCount > 0)
		{
			for (int i = 0; i < initialCount; i++)
			{
				InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!IsEmpty)
		{
			UIManager.MyInstance.ShowTooltip(transform.position);
		}	

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UIManager.MyInstance.HideToolTip();
	}
}
