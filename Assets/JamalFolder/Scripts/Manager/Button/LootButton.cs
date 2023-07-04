using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LootButton : MonoBehaviour, IPointerClickHandler
{
//	, 
//	private SpriteRenderer MySpriteRenderer;

	[SerializeField]	
	private Image Icon;
	[SerializeField]	
	private GameObject LootCanvas;
	public Image MyIcon
	{
		get
		{
			return Icon;
		}
	}
	public Item MyLoot { get; set; }

	public void OnPointerClick(PointerEventData eventData)
	{
		if (InventoryScript.MyInstance.AddItem(MyLoot))
		{
			Destroy(LootCanvas);
		}
	}
}
