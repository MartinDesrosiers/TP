﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour {

	[HideInInspector] public bool facingRight;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform wallCheck;
	private float horizontalForce =0;
	private float verticalForce = 0;

	public bool playable = true;

	private bool grounded = false;
	private bool walled = false;
	public Animator anim;
	private Rigidbody2D rb2d;


	//Mobile controls
	private Vector2 touchOrigin = -Vector2.one;

	// Use this for initialization
	void Awake () 
	{
		facingRight = true;
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (playable) {
			grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			walled = Physics2D.Linecast (transform.position, wallCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

			#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

			if (Input.GetButtonDown ("Jump") && grounded) {
				//Debug.Log("Jump");
				jump = true;
			}

			if (Input.GetButtonDown ("Jump") && walled) {
				jump = true;
			}

			#else

		if (Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if(myTouch.phase == TouchPhase.Began) {
				touchOrigin = myTouch.position;
			}
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >=0 ) {
				Vector2 touchEnd = myTouch.position;
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				touchOrigin.x =-1;
				if(Mathf.Abs(x) > Mathf.Abs(y)) {
					horizontalForce = x > 0  ? 1 : -1;
					Debug.Log("Moving to " +horizontalForce);
				}
				else {
					verticalForce = y >0 ? 1 : -1;
					if (verticalForce == 1 && grounded) {
						jump = true;
					}
					if (verticalForce == 1 && walled) {
						jump = true;
					}
					if(verticalForce == -1) {
					anim.SetBool ("Roll", true);
					}
					else{
					anim.SetBool ("Roll", false);
					}
				}
			}
		}

			#endif
		}
	}

	void FixedUpdate()
	{
		if (playable) {
			#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_HTML5

			horizontalForce = Input.GetAxis ("Horizontal");
			verticalForce = Input.GetAxis ("Vertical");

			anim.SetFloat ("Speed", Mathf.Abs (horizontalForce));

			if (horizontalForce * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * horizontalForce * moveForce);

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

			if (verticalForce <= -0.01f) {
				anim.SetBool ("Roll", true);
				Debug.Log (verticalForce); 
			} else {
				anim.SetBool ("Roll", false);
			}

			#else

		if (horizontalForce != 0) {
		rb2d.AddForce(Vector2.right * horizontalForce * moveForce);
		rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

			#endif

			anim.SetFloat ("Speed", Mathf.Abs (horizontalForce));

			if (horizontalForce > 0 && !facingRight)
				Flip ();
			else if (horizontalForce < 0 && facingRight)
				Flip ();

			if (jump) {	
				anim.SetTrigger ("Jump");
				rb2d.AddForce (new Vector2 (0f, jumpForce));
				jump = false;
			}

			if (walled && rb2d.velocity.y<=0f) {
				rb2d.gravityScale = 0;
				Debug.Log ("No gravity");
			} else {
				rb2d.gravityScale = 1;
			}

			if (Input.GetButtonDown ("Fire1")) {
				anim.SetTrigger ("Punch");
			}
		}
	}


	void Flip()
	{
		Debug.Log ("Character flipped");
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void isDead() {
		//Debug.Log ("He is dead");
		this.transform.position = new Vector3(3f,4.1f,0);
		horizontalForce = 0f;
		//HeroController.Respawn();
	}

	public void levelCompleted() {
		this.transform.position = new Vector3(3f,4.1f,0);
		horizontalForce = 0f;
	}
}