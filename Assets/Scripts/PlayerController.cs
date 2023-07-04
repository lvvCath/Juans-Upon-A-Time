using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RPGTALK.Localization;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private Animator anim;

    public Joystick joystick;

    private bool playerMoving;
	private Vector2 lastMove;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    //start script for RPGTALK
    //The user can move the hero?
    public bool controls;

        //We will sometimes initialize the talk by script, so let's keep a instance of the current RPGTalk
        public RPGTalk rpgTalk;

    //End script for RPGTALK


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();

        //In the tagsDemo scene, we want to do something when we make a choice...
        rpgTalk.OnMadeChoice += OnMadeChoice;

    }
	
	// Update is called once per frame
	void Update () {


        //start script for RPGTALK
            //skip the Talk to the end if the player hit Return
            if (Input.GetKeyDown(KeyCode.Return))
            {
                rpgTalk.EndTalk();
            }
        //End script for RPGTALK

        playerMoving = false;

        if (!attacking)
        {

            if (joystick.Horizontal > 0.5f || joystick.Horizontal < -0.5f) {
			transform.Translate (new Vector3(joystick.Horizontal * moveSpeed * Time.deltaTime, 0f, 0f));	
			playerMoving = true;
			lastMove = new Vector2(joystick.Horizontal, 0f);
		}

		if(joystick.Vertical > 0.5f || joystick.Vertical < -0.5f) {
			transform.Translate(new Vector3(0f, joystick.Vertical * moveSpeed * Time.deltaTime, 0f));	
			playerMoving = true;
			lastMove = new Vector2(0f, joystick.Vertical);
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackTimeCounter = attackTime;
            attacking = true;
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("Attack", true);
        }

    }

			if (attackTimeCounter >= 0){
				attackTimeCounter -= Time.deltaTime;
			}
			if(attackTimeCounter <= 0){
				attacking = false;
				anim.SetBool("Attack", false);
			}

        anim.SetFloat("MoveX", joystick.Horizontal);
        anim.SetFloat("MoveY", joystick.Vertical);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
    


    //the player cant move
    public void CancelControls()
    {
        playerMoving = false;
    }

    //give back the controls to player
    public void GiveBackControls()
    {
        playerMoving = true;
    }

    void OnMadeChoice(int questionId, int choiceID)
    {

        if (choiceID == 0)
        {
            rpgTalk.NewTalk("11", "12");
        }
        else
        {
            rpgTalk.NewTalk("15", "16");

        }

    }

}
