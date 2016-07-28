using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject hero;
	private int levelRows;
	private int levelColumns;
	private float horizonExtent;
	private float verticalExtent;
	float xPosition;
	float yPosition;

	// Use this for initialization
	void Start () {
		levelRows = 50;
		levelColumns = 50;
		//Horizontal half of the screen compared to world units
		horizonExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		//Vertical half of the screen compared to world units
		verticalExtent = Camera.main.orthographicSize+1;
		Debug.Log ("Screen Boundaries x :"+horizonExtent+" /y :"+verticalExtent);

		//Initial camera position(inside the boudaries)
		//SHOULD BE MODIFIED TO APPEAR ANYWHERE INSIDE THE BOUNDARIES
		this.transform.position = new Vector3(horizonExtent,verticalExtent,-10);
	}
	
	// Update is called once per frame
	void Update () {
		//Hero's position
		xPosition = hero.transform.position.x;
		yPosition = hero.transform.position.y;
		//If the camera is in the world's positive zone within the level's boundaries, it will follow the player's movement.
		if (this.transform.position.x > horizonExtent && this.transform.position.x < (levelColumns-horizonExtent) && this.transform.position.y > verticalExtent && this.transform.position.y < (levelRows-horizonExtent)) {
			this.transform.position = new Vector3 (xPosition, yPosition, -10);
		}
	}
}
