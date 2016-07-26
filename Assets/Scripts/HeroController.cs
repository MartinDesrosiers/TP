using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	[HideInInspector] public bool facingRight;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	private float horizontalForce =0;


	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake () 
	{
		facingRight = true;
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate()
	{
		horizontalForce = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontalForce));

		if (horizontalForce * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * horizontalForce * moveForce);

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (horizontalForce > 0 && !facingRight)
			Flip ();
		else if (horizontalForce < 0 && facingRight)
			Flip ();

		if (jump) {	
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
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

	public void Respawn(){
		Debug.Log ("The character is respawning.");
	}

	public void isDead() {
		Debug.Log ("He is dead");
		this.transform.position = new Vector3(0,-2.99f,0);
		horizontalForce = 0f;
		//HeroController.Respawn();
	}

	public void levelCompleted() {
		Application.LoadLevel("menu");
	}
}