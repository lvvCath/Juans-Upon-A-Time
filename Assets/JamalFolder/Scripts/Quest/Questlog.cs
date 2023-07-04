using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour {

	[SerializeField]
	private GameObject questPrefab;
	[SerializeField]
	private Transform questParent;

	private List<QuestScript> questScripts = new List<QuestScript>();

	private Quest selected;

	[SerializeField]
	private Text questDescription;

	private static Questlog instance;

	public static Questlog MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<Questlog>();
			}
			return instance;
		}
	}

	public void AcceptQuest(Quest quest)
	{
		foreach (CollectObjective o in quest.MyCollectObjective)
		{
			InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
		}

		//quests.Add(quest);

		GameObject go = Instantiate(questPrefab, questParent);

		QuestScript qs = go.GetComponentInChildren<QuestScript>();
		quest.MyQuestScript = qs;
		qs.MyQuest = quest;

		questScripts.Add(qs);

		go.GetComponentInChildren<Text>().text = quest.MyTitle;

	}


	public void UpdateSelected()
	{
		ShowDescription(selected);
	}


	public void ShowDescription(Quest quest)
	{
		if (quest != null)
		{
			if (selected != null && selected != quest)
			{
				selected.MyQuestScript.DeSelectQuest();
			}

			string objectives = string.Empty;

			selected = quest;

			string title = quest.MyTitle;

			foreach (Objective obj in quest.MyCollectObjective)
			{
				objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
			}

			questDescription.text = string.Format("{0}\n<size=15>{1}</size>\nObjectives\n<size=15>{2}</size>", title, quest.MyDescription, objectives);
		}
	}


	public void CheckCompletion()
	{
		foreach (QuestScript qs in questScripts)
		{
			qs.IsComplete();
		}
	}
}