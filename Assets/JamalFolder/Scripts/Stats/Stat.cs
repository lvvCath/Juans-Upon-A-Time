using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

	private static Stat instance;

	public static Stat MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<Stat>();
			}
			return instance;
		}
	}

	private Image content;

	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Text statValue;
		
	private float currentFill;
	 
	private float overflow;

	public float MyMaxValue { get; set; }

	private float currentValue;

	public bool IsFull
	{
		get 
		{
			return content.fillAmount == 1; 
		}
	}
	public float MyOverflow 
	{
		get 
		{
			float tmp = overflow; 
			overflow = 0;
			return tmp;
		}
	}
	public float MyCurrentValue
	{
		get
		{
			return currentValue;
		}
		set
		{
			if (value > MyMaxValue) {
				overflow = value - MyMaxValue;
				currentValue = MyMaxValue;
			} else if (value < 0) {
				currentValue = 0;
			} else {
				currentValue = value;
			}

			currentFill = currentValue / MyMaxValue;

			statValue.text = currentValue + "/" + MyMaxValue;
		}
	}

	// Use this for initialization
	void Start () {
		
		content = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFill != content.fillAmount) {
			content.fillAmount = Mathf.MoveTowards(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
		}
	}

	public void Initialize(float currentValue, float maxValue)
	{
		MyMaxValue = maxValue;
		MyCurrentValue = currentValue;
	}
	public void ResetContent()
	{
		content.fillAmount = 0;
	}
}
