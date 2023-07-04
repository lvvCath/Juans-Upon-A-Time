using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSkill : MonoBehaviour
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
	// Update is called once per frame
//		private void FixedUpdate () {
//	
//			if(MyTarget != null) {
//				Vector2 direction = MyTarget.position - transform.position;
//	
//				myRigedbody.velocity = direction.normalized * speed;
//	
//				float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
//	
//				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
//			}	
//		}
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
