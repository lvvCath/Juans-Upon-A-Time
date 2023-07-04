using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour
{
	private static HandScript instance;

	public static HandScript MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<HandScript>();
			}
			return instance;
		}
	}
	public IMoveable MyMoveable { get; set; }
//	[SerializeField]
	private Image icon;

    // Start is called before the first frame update
    void Start()
    {
		icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		icon.transform.position = Input.mousePosition;
//		icon.transform.position = Input.GetTouch(0).position;
		if (Input.touchCount > 0)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			touchDeltaPosition = icon.transform.position;
		}

		DeleteItem();
    }
	public void TakeMoveable(IMoveable moveable)
	{
		this.MyMoveable = moveable;
		icon.sprite = moveable.MyIcon;
		icon.color = Color.white;
	}
	public IMoveable Put ()
	{
		IMoveable	tmp = MyMoveable;

		MyMoveable = null;

		icon.color = new Color(0,0,0,0);

		return tmp;
	}

	public void DropItem()
	{
		MyMoveable = null;
		icon.color = new Color(0,0,0,0);
	}
	private void DeleteItem()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null) 
		{
			if (MyMoveable is Item && InventoryScript.MyInstance.MySelectedSlot != null) 
			{
				(MyMoveable as Item).MySlot.Clear();
			}
			DropItem();

			InventoryScript.MyInstance.MySelectedSlot = null;
		}
	}
}
