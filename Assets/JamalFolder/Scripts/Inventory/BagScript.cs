using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagScript : MonoBehaviour
{
	[SerializeField]	
	private GameObject slotPrefab;

	private List<SlotScript> slots = new List<SlotScript>();

//	private CanvasGroup canvasGroup;

//	private void Awake()
//	{
//		canvasGroup = GetComponent<CanvasGroup>();
//	}
//	public bool IsOpen
//	{
//		get {
//			return canvasGroup.alpha > 0;
//		}
//	}

//	public List<Item> GetItem()
//	{
//		
//	}

	public List<SlotScript> MySlots
	{
		get
		{
			return slots;	
		}
	}
	public void AddSlots(int slotCount)
	{
		for (int i = 0; i < slotCount; i++)
		{
			SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
			slots.Add(slot);
		}
	}
//	public void OpenCloseBags()
//	{
//			canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
//			canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
//	}

	public bool AddItem(Item item)
	{
		foreach (SlotScript slot in slots) {
			if (slot.IsEmpty)
			{
				slot.AddItem(item);

				return true;
			}
		}
		return false;
	}
}
