using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMaster : MonoBehaviour
{
	[SerializeField]
	private Quest[] quests;

	[SerializeField]
	private Questlog tmpLog;

	private void Awake() {
		tmpLog.AcceptQuest(quests[0]);
	}
}
