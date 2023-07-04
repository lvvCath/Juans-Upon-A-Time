using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
	
	private Animator MyAnimator;

	[SerializeField]
	protected Transform ObjectHitBox;
    // Start is called before the first frame update
    void Start()
    {
		MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	public void SmashObject() {
		MyAnimator.SetBool("Smash", true);
		StartCoroutine(BreakObjects());
	}

	IEnumerator BreakObjects() {
		yield return new WaitForSeconds(.5f);
		this.gameObject.SetActive(false);
	}
	//Select Target
	public virtual void DeSelect() {

	}
	public virtual Transform Select() {
		return ObjectHitBox;
	}
}
