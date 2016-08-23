using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour {

	[HideInInspector] public bool facingRight;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	private float horizontalForce =0;
	private float verticalForce = 0;


	private bool grounded = false;
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
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (grounded) {
			//rb2d.transform.position = new Vector2(rb2d.position.x,rb2d.position.y+0.1f);
		}

		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

		if (Input.GetButtonDown("Jump") && grounded) {
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
				}
				verticalForce = 0;
			}
		}

		#endif
	}

	void FixedUpdate()
	{
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_HTML5

		horizontalForce = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(horizontalForce));

		if (horizontalForce * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * horizontalForce * moveForce);

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		#else

		if (horizontalForce != 0) {
		rb2d.AddForce(Vector2.right * horizontalForce * moveForce);
		rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		#endif

		anim.SetFloat("Speed", Mathf.Abs(horizontalForce));

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

	public void isDead() {
		//Debug.Log ("He is dead");
		this.transform.position = new Vector3(3f,4f,0);
		horizontalForce = 0f;
		//HeroController.Respawn();
	}

	public void levelCompleted() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}