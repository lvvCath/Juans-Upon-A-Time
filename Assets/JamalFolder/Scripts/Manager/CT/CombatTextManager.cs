using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CTtype {XP}

public class CombatTextManager : MonoBehaviour
{
	private static CombatTextManager instance;

	public static CombatTextManager MyInstance 
	{
		get
		{
			if (instance == null) {
				instance = FindObjectOfType<CombatTextManager>();
			}
			return instance;
		}
	}
	[SerializeField]
	private GameObject CombatTextPrefab;

	public void CreateText(Vector2 position, string text, CTtype type) {
		Text sct = Instantiate(CombatTextPrefab, transform).GetComponent<Text>();
		sct.transform.position = position;

		string operation = string.Empty;
		string TxtType = string.Empty;
		switch (type)
		{
			case CTtype.XP:
			operation += "+";
			TxtType += "XP";
			sct.color = Color.yellow;
				break;
		}
		sct.text = operation + text + TxtType;
	}

}
