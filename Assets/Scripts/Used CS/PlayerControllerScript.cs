using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private Animator anim;

	private bool attacking;
	public float attackTime;
	private float attackTimeCounter;

	private bool playerMoving;
	private Vector2 lastMove;
	public VectorValue startingPosition;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		transform.position = startingPosition.initialValue;
	}
	
	// Update is called once per frame
	void Update () {

		playerMoving = false;

		if(!attacking){
			
		

		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) {
			transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));	
			playerMoving = true;
			lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
		}

		if(Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f) {
			transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));	
			playerMoving = true;
			lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			attackTimeCounter = attackTime;
			attacking = true;
			myRigidbody.velocity = Vector2.zero;
			anim.SetBool("Player Attack", true);
		}

		}

			if (attackTimeCounter >= 0){
				attackTimeCounter -= Time.deltaTime;
			}
			if(attackTimeCounter <= 0){
				attacking = false;
				anim.SetBool("Player Attack", false);
			}

		anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		anim.SetBool("PlayerMoving", playerMoving);
		anim.SetFloat("LastMoveX", lastMove.x);
		anim.SetFloat("LastMoveY", lastMove.y);
	}
}
