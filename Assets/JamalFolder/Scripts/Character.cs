using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	private Vector2 direction;
	protected Rigidbody2D myRigidbody;
	protected  Coroutine attackRoutine;
	private LootEffector lootEffector;
	[SerializeField]	
	private int Level;
	[SerializeField]
	protected Transform HitBox;
	[SerializeField]
	private float Speed;
	[SerializeField]
	private Stat health;
	protected int HealthInit;
    [SerializeField]
   

    public Animator myAnimator { get; set; }

	public bool isAttacking { get; set; }

	public Transform MyTarget { get; set; }

	public Coroutine MyattackRoutine
	{
		get
		{
			return attackRoutine; 
		}
		set 
		{
			attackRoutine = value;
		}
	}
	public bool Moving
	{
		get
		{
			return direction.x != 0 || direction.y != 0; 
		}
	}
	public int MyHealthInit
	{
		get
		{
			return HealthInit; 
		}
		set 
		{
			HealthInit = value; 
		}
	}
	public Stat MyHealth 
	{
		get
		{
			return health; 
		}
		set 
		{
			health = value; 
		}
	}
	public Vector2 MyDirection 
	{
		get
		{
			return direction; 
		}
		set 
		{
			direction = value; 
		}
	}
	public float MySpeed 
	{
		get
		{
			return Speed; 
		}
		set 
		{
			Speed = value; 
		}
	}

	public bool IsAlive  
	{
		get
		{
			return health.MyCurrentValue > 0;
		}
	}
	public bool IsDeath
	{
		get
		{
			return health.MyCurrentValue <= 0;
		}
	}
	public int MyLevel 
	{
		get 
		{
			return Level;
		}
		set
		{
			Level = value;
		}
	}

	// Use this for initialization
	protected virtual void Start () {
//		health.Initialize (HealthInit, HealthInit);
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		EnemyHandleLayers();
	}
	private void FixedUpdate() {
		Move ();
	}
	public void EnemyHandleLayers()  {

		if (IsAlive) {
			if (Moving) {
				ActivateLayer("WalkLayer");

				if (Moving) {

					//Float direction para sa idle 
					myAnimator.SetFloat ("x",  direction.x);
					myAnimator.SetFloat ("y",  direction.y);
				}
			}
			else if (isAttacking) {
				ActivateLayer("AttackLayer");
			}
			else {
				ActivateLayer ("IdleLayer");
			}
		}
        else
        {
            ActivateLayer("DeathLayer");
        }

    }
	//GetKey direction
	public void Move() {
		if (IsAlive) { 
			myRigidbody.velocity = direction.normalized * Speed;
		}
	}

	public void ActivateLayer(string layerName) {
		for (int i = 0; i < myAnimator.layerCount; i++) {
			myAnimator.SetLayerWeight (i, 0);
		}
		myAnimator.SetLayerWeight (myAnimator.GetLayerIndex(layerName), 1);
	}


	public virtual void PlayerDamage(float damage, Transform source) {

		health.MyCurrentValue -= damage;	

		if (health.MyCurrentValue <= 0)
		{
			MyDirection = Vector2.zero;
			myRigidbody.velocity = MyDirection;
			myAnimator.SetTrigger("die");

			if (this as Enemy) {
				Player.MyInstance.GainXP(XPManager.CalculatingXP((this as Enemy)));
			}
		}
	}
}
