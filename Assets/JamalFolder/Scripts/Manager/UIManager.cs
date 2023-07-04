using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;

	public static UIManager MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<UIManager>();
			}
			return instance;
		}
	}

	public bool IsOpen = true;
	[SerializeField]
	private Button[] SkillActionbuttons;
	[SerializeField]
	private ActionButton[] actionbuttons;
	[SerializeField]
	private GameObject Setting;
	[SerializeField]
	private Text LevelText;
	[SerializeField]
	private GameObject TargetFrame;
	[SerializeField]
	private CanvasGroup BagInventory;
	[SerializeField]
	private CanvasGroup QuestsUI;
	[SerializeField]
	private CanvasGroup KeybindMenu;	
	[SerializeField]
	private CanvasGroup OpenUpgradeUI;	
	[SerializeField]
	private GameObject Tooltip;
	[SerializeField]
	private Text TooltipText;

	private GameObject[] keybindButtons;

	private KeyCode Action1;

	private void Awake()
	{
		keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
		TooltipText = Tooltip.GetComponentInChildren<Text>();
	}
    // Start is called before the first frame update
    void Start()
    {
		Action1 = KeyCode.Space;
    }

    // Update is called once per frame
    void Update()
    {
//		if (Input.GetKeyDown(Action1)) {
//			ActionButtonOnClick(0);
//		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			if (QuestsUI.alpha == 0)
			{
				OpenQuestsUI();
			} 
			else if (QuestsUI.alpha == 1)
			{
				CloseQuestsUI();
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (BagInventory.alpha == 0)
			{
				OpenInventory();
			} 
			else if (BagInventory.alpha == 1)
			{
				CloseInventory();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (KeybindMenu.alpha == 0)
			{
				OpenMenu();
			} 
			else if (KeybindMenu.alpha == 1)
			{
				CloseMenu();
			}
		}
    }
//	private void ActionButtonOnClick(int btnIndex) 
//	{
//		SkillActionbuttons[btnIndex].onClick.Invoke();
//	}
	public void ActivateTargetFrame(EnemyNPC target) {
		TargetFrame.SetActive(true);
		LevelText.text = target.MyLevel.ToString();
		if (target.MyLevel >= Player.MyInstance.MyLevel + 5) {
			LevelText.color = Color.red;
		}
		else if (target.MyLevel == Player.MyInstance.MyLevel + 3 || target.MyLevel >= Player.MyInstance.MyLevel + 4 ) {
			LevelText.color = new Color(255, 124, 0, 255);
		}
		else if (target.MyLevel >= Player.MyInstance.MyLevel - 2 && target.MyLevel >= Player.MyInstance.MyLevel + 2) {
			LevelText.color = Color.yellow;
		}
		else if (target.MyLevel <= Player.MyInstance.MyLevel - 3 && target.MyLevel > XPManager.calculateGrayLevel()) {
			LevelText.color = Color.green;
		}
		else {
			LevelText.color = Color.gray;
		}
	}
	public void DeactivateTargetFrame() {
		TargetFrame.SetActive(false);
	}

	public void OpenQuestsUI() {
		if (QuestsUI.alpha == 0)
		{
			QuestsUI.alpha = QuestsUI.alpha < 1 ? 1 : 1;
			QuestsUI.blocksRaycasts = QuestsUI.blocksRaycasts == true ? true : true;
			Time.timeScale = Time.timeScale > 0 ? 0 : 0;

			CloseInventory();
			CloseMenu();
			ClosePlayerStat();
		} 
		else if (QuestsUI.alpha == 1)
		{
			CloseQuestsUI();
		}
	}
	public void CloseQuestsUI() {
		QuestsUI.alpha = QuestsUI.alpha < 0 ? 0 : 0;
		QuestsUI.blocksRaycasts = QuestsUI.blocksRaycasts == false ? false : false;
//		Time.timeScale = Time.timeScale > 0 ? 0 : 0;
	}
	public void OpenInventory() 
	{
		if (BagInventory.alpha == 0)
		{
			BagInventory.alpha = BagInventory.alpha > 1 ? 1 : 1;
			BagInventory.blocksRaycasts  = BagInventory.blocksRaycasts == true ? true : true;
			Time.timeScale = Time.timeScale > 0 ? 0 : 0;

			CloseQuestsUI();
			CloseMenu();
			ClosePlayerStat();
		} 
		else if (BagInventory.alpha == 1)
		{
			CloseInventory();
		}
	}
	public void CloseInventory() 
	{
		BagInventory.alpha = BagInventory.alpha < 0 ? 0 : 0;
		BagInventory.blocksRaycasts = BagInventory.blocksRaycasts == false ? false : false;
//		Time.timeScale = Time.timeScale > 0 ? 0 : 0;
	}
	public void OpenMenu()
	{
		if (KeybindMenu.alpha == 0)
		{
			KeybindMenu.alpha = KeybindMenu.alpha > 1 ? 1 : 1;
			KeybindMenu.blocksRaycasts  = KeybindMenu.blocksRaycasts == true ? true : true;
			Time.timeScale = Time.timeScale > 1 ? 1 : 0;

			CloseQuestsUI();
			CloseInventory();
			ClosePlayerStat();
		} 
		else if (KeybindMenu.alpha == 1)
		{
			CloseMenu();
		}
	}
	public void CloseMenu()
	{
		KeybindMenu.alpha = KeybindMenu.alpha < 0 ? 0 : 0;
		KeybindMenu.blocksRaycasts = KeybindMenu.blocksRaycasts == false ? false : false;
		Time.timeScale = Time.timeScale > 0 ? 0 : 1;
	}
	public void OpenPlayerStat()
	{
		if (OpenUpgradeUI.alpha == 0)
		{
			OpenUpgradeUI.alpha = OpenUpgradeUI.alpha > 1 ? 1 : 1;
			OpenUpgradeUI.blocksRaycasts  = OpenUpgradeUI.blocksRaycasts == true ? true : true;
			Time.timeScale = Time.timeScale > 0 ? 0 : 0;

			CloseQuestsUI();
			CloseInventory();
			CloseMenu();
		} 
		else if (OpenUpgradeUI.alpha == 1)
		{
			ClosePlayerStat();
		}
	}
	public void ClosePlayerStat()
	{
		OpenUpgradeUI.alpha = OpenUpgradeUI.alpha < 0 ? 0 : 0;
		OpenUpgradeUI.blocksRaycasts = OpenUpgradeUI.blocksRaycasts == false ? false : false;
	}
//	public void OpenBag(int bagbtnIndex) {
//		Actionbuttons[bagbtnIndex].onClick.Invoke();
//	}
	public void UpdateStackSize(IClickable clickable)
	{
		if (clickable.MyCount > 1) {
			clickable.MyStackText.text = clickable.MyCount.ToString();
			clickable.MyStackText.color = Color.white;
			clickable.MyIcon.color = Color.white;
		}
		else {
			clickable.MyStackText.color = new Color(0,0,0,0);
			clickable.MyIcon.color = Color.white;

		}
		if (clickable.MyCount == 0) {
			clickable.MyIcon.color = new Color(0,0,0,0);
			clickable.MyStackText.color = new Color(0,0,0,0);
		}
	}
	public void UpdateKeyText(string key, KeyCode code)
	{
		Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
		tmp.text = code.ToString();	
	}
		
	public void ClickActionButton(string buttonName)
	{
		Array.Find(actionbuttons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
	}	
	public void SetUseable(ActionButton Btn, UseableUI useable) {
		Btn.MyButton.image.sprite = useable.MyIcon;
		Btn.MyButton.image.color = Color.white;
		Btn.MyUseable = useable;
	}
	public void ShowTooltip (Vector3 position)
	{
		Tooltip.SetActive(true);
		Tooltip.transform.position = position;
	}
	public void HideToolTip()
	{
		Tooltip.SetActive(false);
	}
}
