using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable
{
	private StackManager<Item> items = new StackManager<Item>();

	public UseableUI MyUseable { get; set; }

	public Button MyButton { get; private set; }

	private ActionButton ActionButtonSlot;

	[SerializeField]
	public Text stackSize;

	[SerializeField]
	private Image icon;

	private Stack<UseableUI> useables = new Stack<UseableUI>();

	private int count;

	public ActionButton MyActionButtonSlot
	{
		get 
		{ 
			return ActionButtonSlot; 
		}
		set 
		{
			ActionButtonSlot = value;

			if (value != null)
			{
				ActionButtonSlot.MyIcon.color = Color.grey;
			}
		}
	}
	public Stack<UseableUI> MyUseables
	{
		get 
		{ 
			return useables; 
		}
		set 
		{
			if (value.Count > 0)
			{
				MyUseable = value.Peek();
			} 
			else
			{
				MyUseable = null;
			}
			useables = value;
		}
	}
	public  StackManager<Item> MyItems
	{
		get 
		{
			return items;
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
		get 
		{
			return count;
		}
	}
	public Text MyStackText
	{
		get 
		{
			return stackSize;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		MyButton = GetComponent<Button>();	
		MyButton.onClick.AddListener(OnClick);
		InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }
	public void OnClick() {
		if (HandScript.MyInstance.MyMoveable == null)
		{
			if (MyUseable != null) {
				MyUseable.Use();
			}
			else if (MyUseables != null && MyUseables.Count > 0) {
				MyUseables.Peek().Use();
			}	
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is UseableUI && IsEmpty) {
				SetUseable(HandScript.MyInstance.MyMoveable as UseableUI);

			} 
//			else if (InventoryScript.MyInstance.MySelectedSlot == null && !IsEmpty){
//				HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
//			}
		}
//		if (eventData.button == PointerEventData.InputButton.Right)
//		{
//			if (MyActionButtonSlot == null) {
//				HandScript.MyInstance.TakeMoveable(MyItems as IMoveable);
////				InventoryScript.MyInstance.MySelectedSlot = this;	
////			} else if (InventoryScript.MyInstance.MySelectedSlot != null) {
////				if (PutItemBack() || MargeItems(InventoryScript.MyInstance.MySelectedSlot) || SwapItem(InventoryScript.MyInstance.MySelectedSlot) || AddItems(InventoryScript.MyInstance.MySelectedSlot.MyItems)) {
////					HandScript.MyInstance.DropItem();
////					InventoryScript.MyInstance.MySelectedSlot = null;
////				}
//			}
//		}
	}
	public void SetUseable (UseableUI useable) {
		if (useable is Item) 
		{
			MyUseables = InventoryScript.MyInstance.GetUseableUI(useable);
			count = MyUseables.Count;
			InventoryScript.MyInstance.MySelectedSlot.MyIcon.color = Color.white;
			InventoryScript.MyInstance.MySelectedSlot = null;
		}
		else {
			this.MyUseable = useable;
		}
		UpdateVisual();
	}
	public void UpdateVisual() {
		MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
		MyIcon.color = Color.white;

		if (count  > 1)
		{
			UIManager.MyInstance.UpdateStackSize(this);
		}
	}

	public void UpdateItemCount(Item item)
	{
		if (item is UseableUI && MyUseables.Count > 0 ) 
		{
			if (MyUseables.Peek().GetType() == item.GetType())
			{
				MyUseables = InventoryScript.MyInstance.GetUseableUI(item as UseableUI);

				count = MyUseables.Count;

				UIManager.MyInstance.UpdateStackSize(this);
			}
		}
	}
}
