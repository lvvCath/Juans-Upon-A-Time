using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyNPC
{
	private static Enemy instance;

	public static Enemy MyInstance
	{
		get 
		{
			if (instance == null) 
			{
				instance = FindObjectOfType<Enemy>();
			}
			return instance;
		}
	}

	[SerializeField]
	private CanvasGroup HealthGroup;
   
    //Patrol 
    private SkillBooks skillBooks;
	public Transform[] PatrolSpots;
	public int RandomSpots { get; set; }
	public float WaitTime;
	public float StartWaitTime{ get; set; }

	private IState currentState;

	public float MyAttackRange { get; set; }

	public float MyAttackTime { get; set; }

	public Vector3 MyHomePosition { get; set; }

	[SerializeField]
	private float InitAggroRange;

	public float MyAggroRange { get; set; }

    

    public SkillBooks MyskillBooks 
	{
		get 
		{
			return skillBooks;
		}
	}

	public bool InRange 
	{
		get 
		{
			return Vector2.Distance(transform.position,MyTarget.position) < MyAggroRange;
		}
	}

	protected void Awake() {
		skillBooks = GetComponent<SkillBooks> ();
		MyHealth.Initialize(20, Mathf.Floor(20 * MyLevel * Mathf.Pow(MyLevel, 0.001f)));
		WaitTime = StartWaitTime;
		RandomSpots = Random.Range(0, PatrolSpots.Length);
		MyHomePosition = transform.position;
		MyAggroRange = InitAggroRange;
		MyAttackRange = 3;
		ChangeState(new IdleState());
	} 
	protected override void Update () {
		if (IsAlive) { 
			if (!isAttacking) {
				MyAttackTime += Time.deltaTime;
			}
			currentState.Update();
		}
		base.Update();
	} 
	public override void PlayerDamage(float damage, Transform source) {
		if (!(currentState is EvadeState)) {
			SetTarget(source);
			base.PlayerDamage(damage, source);
		}
	}
	public override Transform Select() {
		HealthGroup.alpha = 1;

		return base.Select();
	}
	public override void DeSelect() {
		HealthGroup.alpha = 0;

		base.DeSelect();
	}

	public void ChangeState(IState newState) { 
		if (currentState != null) {
			currentState.Exit();
		}
		currentState = newState;
		currentState.Enter(this);
	}
	public void SetTarget(Transform target) 
	{
		if (MyTarget == null && !(currentState is EvadeState)) {
			float distance = Vector2.Distance(transform.position, target.position);
			MyAggroRange = InitAggroRange;
			MyAggroRange += distance;
			MyTarget = target;
		}
	}
	public void Reset() {
		this.MyTarget = null;
		this.MyAggroRange = InitAggroRange;
		this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
	}
	public override void Interact()
	{

	}
	public override void StopInteract()
	{

	}
}
