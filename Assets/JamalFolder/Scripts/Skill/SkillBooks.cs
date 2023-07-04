using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBooks : MonoBehaviour
{

	private static SkillBooks instance;

	public static SkillBooks MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<SkillBooks>();
			}
			return instance;
		}
	}

	[SerializeField]
	private Skills[] skills;

	public Skills[] MySkill
	{
		get
		{
			return skills;
		}
	}

//	[SerializeField]
//	private Text HP;
//
//	[SerializeField]
//	private Text MP;
//    
//	[SerializeField]
//	private Text EXP;
//
//	[SerializeField]
//	private Text Level;
//
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//		ShowStat ();
    }
	public Skills CastSkill(int index){
		return skills[index];
	}

//	public void ShowStat ()
//	{
//		//HP
//		string CurrentValueHP = Player.MyInstance.MyHealth.MyCurrentValue.ToString();
//		string MaxValueHP = Player.MyInstance.MyHealth.MyMaxValue.ToString();
//
//		HP.text = string.Format("HP  {0}/{1}", CurrentValueHP, MaxValueHP);
//
//		//MP
//		string CurrentValueMP = Player.MyInstance.MyManaStat.MyCurrentValue.ToString();
//		string MaxValueMP = Player.MyInstance.MyManaStat.MyMaxValue.ToString();
//
//		MP.text = string.Format("HP  {0}/{1}", CurrentValueMP, MaxValueMP);
//
//		//MP
//		string CurrentValueEXP = Player.MyInstance.MyXPstat.MyCurrentValue.ToString();
//		string MaxValueEXP = Player.MyInstance.MyXPstat.MyMaxValue.ToString();
//
//		EXP.text = string.Format("HP  {0}/{1}", CurrentValueEXP, MaxValueEXP);
//
//		Level.text = string.Format("{0}", Player.MyInstance.MyLevelText.ToString());
//	}
}
