using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUseable : MonoBehaviour
{
	[SerializeField]
	private Item itemsUseable;

	[SerializeField]
	private LootButton lootButtons;

	private void Awake() {
		AddLoots();
	}
	private void AddLoots()	
	{
		lootButtons.MyIcon.sprite = itemsUseable.MyIcon;

		lootButtons.MyLoot = itemsUseable;

		lootButtons.gameObject.SetActive(true);

		lootButtons.MyIcon.color = Color.white;
	}
}
