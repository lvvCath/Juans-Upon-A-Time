using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
	private static Player instance;

	public static Player MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<Player>();
			}
			return instance;
		}
	}

	[SerializeField]
	protected Joystick  joystick;

	[SerializeField]
	private Stat ManaStat;

	[SerializeField]
	private Stat XPstat;

	[SerializeField]	
	private Text LevelText;

	[SerializeField]	
	private Text UpgradeText;

//	[SerializeField]
//	private Block[] blocks;

	[SerializeField]
	private bool Attacks;

	private Ienteractable interactable;
	private SkillBooks skillBooks;
	private Vector3 joystickdirection;
	private int exitIndex = 2;

	private float ManaInit = 50;

	public bool Movingjoystick
	{
		get
		{
			return joystick.Horizontal != 0 || joystick.Vertical != 0; 
		}
	}
	public Stat MyXPstat
	{
		get
		{
			return XPstat; 
		}
		set 
		{
			XPstat = value;
		}
	}
	public Stat MyManaStat
	{
		get
		{
			return ManaStat; 
		}
		set 
		{
			ManaStat = value;
		}
	}
	public int MyLevelText
	{
		get
		{
			return MyLevel; 
		}
	}
	protected override void Start()
	{
		skillBooks = GetComponent<SkillBooks> ();
		ManaStat.Initialize(20, Mathf.Floor(20 * MyLevel * Mathf.Pow(MyLevel, 0.01f)));
		MyHealth.Initialize(100, Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.1f)));
		XPstat.Initialize(0,Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
		LevelText.text = MyLevel.ToString();
		base.Start();
	}
	// Update is called once per frame
	protected override void Update () {
		GetInput ();
		base.Update();
		joystickMove();
		HandleLayers();	
//		GetInputjoystick();

	} 		
	private void joystickMove()
	{
		Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical).normalized;

		if (moveVector != Vector3.zero)
		{ 
			transform.Translate(moveVector * MySpeed * Time.deltaTime, Space.World);
		}
	}
	public void HandleLayers(){

        if (IsAlive)
        {
            if (Movingjoystick || Moving)
            {
                ActivateLayer("WalkLayer");

                if (Movingjoystick)
                {

                    //Float direction para sa idle 
                    myAnimator.SetFloat("x", joystick.Horizontal);
                    myAnimator.SetFloat("y", joystick.Vertical);
                }
                if (Moving)
                {

                    //Float direction para sa idle 
                    myAnimator.SetFloat("x", MyDirection.x);
                    myAnimator.SetFloat("y", MyDirection.y);
                }
                StopAttacking();
            }
            else if (isAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                ActivateLayer("IdleLayer");
            }
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }

	}
//	private void GetInputjoystick()
//	{
//		joystickdirection = Vector3.zero; 
//
//		//Debugging 
//
//		if(Input.GetKey(KeyCode.JoystickButton1)) {
//			joystickdirection += Vector3.up;
//			exitIndex = 0;
//			Debug.Log("up");
//		}
//		if(Input.GetKey(KeyCode.JoystickButton2)) {
//			joystickdirection += Vector3.left;
//			exitIndex = 3;
//			Debug.Log("left");
//		}
//		if(Input.GetKey(KeyCode.JoystickButton3)) {
//			joystickdirection += Vector3.down;
//			exitIndex = 2;
//			Debug.Log("down");
//		}
//		if(Input.GetKey(KeyCode.JoystickButton4)) {
//			joystickdirection += Vector3.right;
//			exitIndex = 1;
//			Debug.Log("right");
//		}
//	}
	private void GetInput()
	{
		MyDirection = Vector2.zero; 

		//Debugging 
		if(Input.GetKey(KeyCode.X)) {
			GainXP (17);
		}
		//Debugging
		if(Input.GetKeyDown(KeyCode.I)) {
			MyHealth.MyCurrentValue -= 10;
			ManaStat.MyCurrentValue -= 10;
		}
		//Debugging
		if(Input.GetKeyDown(KeyCode.O)) {
			MyHealth.MyCurrentValue += 10;
			ManaStat.MyCurrentValue += 10;
		}

		if(Input.GetKey(KeySetting.MyInstance.Keybinds["UP"])) {
			MyDirection += Vector2.up;
			exitIndex = 0;
		}
		if(Input.GetKey(KeySetting.MyInstance.Keybinds["LEFT"])) {
			MyDirection += Vector2.left;
			exitIndex = 3;
		}
		if(Input.GetKey(KeySetting.MyInstance.Keybinds["DOWN"])) {
			MyDirection += Vector2.down;
			exitIndex = 2;
		}
		if(Input.GetKey(KeySetting.MyInstance.Keybinds["RIGHT"])) {
			MyDirection += Vector2.right;
			exitIndex = 1;
		} 
		if (Moving) {
			StopAttacking();
		}
		foreach (string action in KeySetting.MyInstance.ActionBinds.Keys) 
		{
			if (Input.GetKeyDown(KeySetting.MyInstance.ActionBinds[action])) 
			{
				UIManager.MyInstance.ClickActionButton(action);
			}
		}
	}
//	public void ActivateAttack(int Skillsindex) {
//		if(Input.GetKey(KeyCode.Space) && Attacks) {
////		Block ();
//				if(MyTarget != null && !isAttacking && !Moving && InlineofSight()){
//				attackRoutine = StartCoroutine(Attack (Skillsindex));
//				}
//		}
//	}
	public IEnumerator Attack(int BasicIndex)
	{
		Skills NewSkill = skillBooks.CastSkill(BasicIndex);
		Transform currentTarget = MyTarget;
		isAttacking = true;
		myAnimator.SetBool ("attack", isAttacking);

		yield return new WaitForSeconds (NewSkill.MyCastTime); 

		if (currentTarget != null && InlineofSight()) {
			BasicAttackScript s1 = NewSkill.MySkillPrefab[0].GetComponent<BasicAttackScript>();
			BasicAttackScript s2 = NewSkill.MySkillPrefab[1].GetComponent<BasicAttackScript>();
			BasicAttackScript s3 = NewSkill.MySkillPrefab[2].GetComponent<BasicAttackScript>();
			BasicAttackScript s4 = NewSkill.MySkillPrefab[3].GetComponent<BasicAttackScript>();

			s1.Initialize(currentTarget, NewSkill.MyDamage, transform);
			s2.Initialize(currentTarget, NewSkill.MyDamage, transform);
			s3.Initialize(currentTarget, NewSkill.MyDamage, transform);
			s4.Initialize(currentTarget, NewSkill.MyDamage, transform);
		}
		StopAttacking ();
	}
	public void CastSkills(int BasicIndex)
	{
		//		Block ();

		if(MyTarget != null && Attacks && !isAttacking && !Moving && InlineofSight()){
			attackRoutine = StartCoroutine(Attack(BasicIndex));
		}
	}

	public IEnumerator FirstSkills(int FirstSkillsindex)
	{
		Skills NewSkill = skillBooks.CastSkill(FirstSkillsindex);
		Transform currentTarget = MyTarget;
		isAttacking = true;
		myAnimator.SetBool ("skill", isAttacking);

		yield return new WaitForSeconds (NewSkill.MyCastTime); 

		if (currentTarget != null && InlineofSight()) {
			FirstSkill fs1 = Instantiate (NewSkill.MySkillPrefab[0], transform.position, Quaternion.identity).GetComponent<FirstSkill>();
			fs1.Initialize(currentTarget, NewSkill.MyDamage, transform);
		}
		StopAttacking ();
	}

	public void CastFirstSkill(int FirstSkillsindex)
	{
		//		Block ();

		if(MyTarget != null && Attacks && !isAttacking && !Moving && InlineofSight()){
			attackRoutine = StartCoroutine(FirstSkills(FirstSkillsindex));
		}
	}

	public IEnumerator SecondSkills(int SecondSkillsindex)
	{
	Skills NewSkill = skillBooks.CastSkill(SecondSkillsindex);
	Transform currentTarget = MyTarget;
	isAttacking = true;
		myAnimator.SetBool ("skill", isAttacking);

	yield return new WaitForSeconds (NewSkill.MyCastTime); 
	if (currentTarget != null && InlineofSight()) {
		FirstSkill fs1 = Instantiate (NewSkill.MySkillPrefab[0], transform.position, Quaternion.identity).GetComponent<FirstSkill>();
	fs1.Initialize(currentTarget, NewSkill.MyDamage, transform);
	}
		StopAttacking ();
    }
//
public void CastSecondSkill(int SecondSkillsindex)
	{
      //  Block ();

if(MyTarget != null && Attacks && !isAttacking && !Moving && InlineofSight()){
			attackRoutine = StartCoroutine(SecondSkills(SecondSkillsindex));
	}
	}

//	public IEnumerator SecondSkills(int SecondSkillsindex)
//	{
//		Skills NewSkill = skillBooks.CastSkill(SecondSkillsindex);
//		Transform currentTarget = MyTarget;
//		isAttacking = true;
//		myAnimator.SetBool ("skill", isAttacking);
//
//		yield return new WaitForSeconds (NewSkill.MyCastTime); 
//
//		if (currentTarget != null && InlineofSight()) {
//			FirstSkill fs1 = Instantiate (NewSkill.MySkillPrefab[0], transform.position, Quaternion.identity).GetComponent<FirstSkill>();
//			fs1.Initialize(currentTarget, NewSkill.MyDamage, transform);
//		}
//		StopAttacking ();
//	}
//
//	public void CastSecondSkill(int SecondSkillsindex)
//	{
//		//		Block ();
//
//		if(MyTarget != null && Attacks && !isAttacking && !Moving && InlineofSight()){
//			attackRoutine = StartCoroutine(SecondSkills(SecondSkillsindex));
//		}
//	}
//	public void FirstSkill()
//	{
//		if(!isAttacking && !Moving){
//			isAttacking = true;
//			myAnimator.SetBool ("attack", isAttacking);
//
//			StopAttacking ();
//		}
//	}
//	public void CastFirstSkill()
//	{
//		if(!isAttacking && !Moving && InlineofSight()){
//			FirstSkill();
//		}
//	}

	private bool InlineofSight()
	{
		if (MyTarget != null) {
			Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
			Debug.DrawRay (transform.position, targetDirection, Color.red);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance (transform.position, MyTarget.transform.position),256);
			if (hit.collider == null){
				return true;	
			}
		}
		return false;
	}
	public virtual void StopAttacking() {
		isAttacking = false;
		myAnimator.SetBool ("attack", isAttacking);
		myAnimator.SetBool ("attack", isAttacking);

		if(attackRoutine != null) {
			StopCoroutine(attackRoutine);	
		}
	}
//	private void Block() {
//		foreach (Block b in blocks) {
//			b.Deactivate ();
//		}
//		blocks [exitIndex].Activate ();
//	}

	public void GainXP (int xp)
	{
		XPstat.MyCurrentValue += xp;

		if (XPstat.MyCurrentValue == XPstat.MyMaxValue)
		{
			MyHealth.MyCurrentValue += MyHealth.MyMaxValue;
			ManaStat.MyCurrentValue += ManaStat.MyMaxValue;
		}

		CombatTextManager.MyInstance.CreateText(transform.position,xp.ToString(),CTtype.XP);

		if (XPstat.MyCurrentValue >= XPstat.MyMaxValue) {
			StartCoroutine(Ding());
		}
	}

	private IEnumerator Ding() {
		while (!XPstat.IsFull) {
			yield return null;
		}	

		MyLevel++;
		LevelText.text = MyLevel.ToString();
		XPstat.MyMaxValue = 100 * MyLevel * Mathf.Pow(MyLevel, 0.5f);
		XPstat.MyMaxValue = Mathf.Floor(XPstat.MyMaxValue);
		XPstat.MyCurrentValue = XPstat.MyOverflow;
		XPstat.ResetContent();

		MyHealth.MyMaxValue = 100 * MyLevel * Mathf.Pow(MyLevel, 0.1f);
		MyHealth.MyMaxValue = Mathf.Floor(MyHealth.MyMaxValue);
		MyHealth.MyCurrentValue = MyHealth.MyMaxValue;

		ManaStat.MyMaxValue = 20 * MyLevel * Mathf.Pow(MyLevel, 0.01f);
		ManaStat.MyMaxValue = Mathf.Floor(ManaStat.MyMaxValue);
		ManaStat.MyCurrentValue = ManaStat.MyMaxValue;




		if (XPstat.MyCurrentValue >= XPstat.MyMaxValue) {
			StartCoroutine(Ding());
		}
	}

	public void Interact()
	{
		if (interactable != null)
		{
			interactable.Interact();
		}
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.CompareTag("HitBox")) { 	
			Attacks = true;
			Debug.Log("enterRangs");
		} else if (collision.CompareTag("ObjectHitBox")) {
			Attacks = true;		
		}
		//Interactable
		if (collision.CompareTag("Enemy"))
		{
			interactable = collision.GetComponent<Ienteractable>();
		}
	}
	private void OnTriggerExit2D (Collider2D collision) {
		if (collision.CompareTag("HitBox")) { 	
			Attacks = false;
			Debug.Log("exitRangs");
		} else if (collision.CompareTag("ObjectHitBox")) { 	
			Attacks = false;
			Debug.Log("exitRangs");
		}
		//Interactable
		if (collision.CompareTag("Enemy"))
		{
			if (interactable != null) {
				interactable.StopInteract();
				interactable = null;
			}
		}
	}
	private void Upgrade() {
		if (MyLevel == 2)
		{
			UpgradeText.text = 1.ToString();
		}
	}
}
