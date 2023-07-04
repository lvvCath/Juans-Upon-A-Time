using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
	public Quest MyQuest { get; set; }

	public bool MarkedComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void SelectQuest()
	{
		GetComponent<Text>().color = Color.red;
		Questlog.MyInstance.ShowDescription(MyQuest);
	}
	public void DeSelectQuest()
	{
		GetComponent<Text>().color = Color.white;
	}
	public void IsComplete()
	{
		if(MyQuest.IsComplete && !MarkedComplete)
		{
			MarkedComplete = true;
			GetComponent<Text>().text += "(Complete)";
		} 
		else if (!MyQuest.IsComplete)
		{
			MarkedComplete = false;
			GetComponent<Text>().text = MyQuest.MyTitle;
		}
	}
}
