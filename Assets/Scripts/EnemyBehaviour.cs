using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	[HideInInspector] public bool facingRight;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	private float horizontalForce =0;
	private float verticalForce = 0;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		facingRight = false;
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void walk(){
		
	}
}
