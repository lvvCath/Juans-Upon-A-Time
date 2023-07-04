using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
	private Rigidbody2D myRigedbody;

	[SerializeField]
	private float speed;

	private int damage;

	private bool Hit;

	public Transform MyTarget { get; private set; }

	private Transform source; 

	// Use this for initialization
	void Start () {
		myRigedbody = GetComponent<Rigidbody2D> ();
	}

	public void Initialize(Transform target, int damage, Transform source) {
		this.MyTarget = target;
		this.damage = damage;
		this.source = source;
	}

	private void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag("HitBox")) 
		{
			Hit = true;
			Character character = other.GetComponentInParent<Character>();
			speed = 0;
			character.PlayerDamage(damage, source);
			GetComponent<Animator>().SetTrigger("Effect");
			myRigedbody.velocity = Vector2.zero;
			MyTarget = null;
		}
	}
}
