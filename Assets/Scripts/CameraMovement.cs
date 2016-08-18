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
		//Debug.Log ("Screen Boundaries x :"+horizonExtent+" /y :"+verticalExtent);

		//Initial camera position(inside the boudaries)
		//SHOULD BE MODIFIED TO APPEAR ANYWHERE INSIDE THE BOUNDARIES
	}

	// Update is called once per frame
	void Update () {
		//Hero's position
		xPosition = hero.transform.position.x;
		yPosition = hero.transform.position.y;

		if (xPosition <= horizonExtent && yPosition <= verticalExtent-1) {
			this.transform.position = new Vector3 ((horizonExtent + 1), (verticalExtent-1), -10);
			//Debug.Log ("Camera blocked left/bottom");
		} else if (xPosition <= horizonExtent + 1 && yPosition >= verticalExtent+levelRows) {
			this.transform.position = new Vector3 ((horizonExtent + 1), (verticalExtent+levelRows), -10);
			//Debug.Log ("Camera blocked left/top");
		} else if (xPosition >= horizonExtent+levelColumns && yPosition <= verticalExtent-1) {
			this.transform.position = new Vector3 ((horizonExtent + levelColumns), verticalExtent-1, -10);
			//Debug.Log ("Camera blocked right/bottom");
		} else if (xPosition >= horizonExtent+levelColumns && yPosition >= verticalExtent+levelRows) {
			this.transform.position = new Vector3 ((horizonExtent + levelColumns), verticalExtent+levelRows, -10);
			//Debug.Log ("Camera blocked right/top");
		} else if (yPosition < verticalExtent-1) {
			this.transform.position = new Vector3 (xPosition+1, verticalExtent-1, -10);
			//Debug.Log ("Camera blocked bottom");
		} else if (xPosition < horizonExtent + 1) {
			this.transform.position = new Vector3 ((horizonExtent + 1), yPosition, -10);
			//Debug.Log ("Camera blocked left");
		} else if (xPosition > horizonExtent+levelColumns) {
			this.transform.position = new Vector3 ((horizonExtent+levelColumns), yPosition, -10);
			//Debug.Log ("Camera blocked right");
		}  else if (yPosition > verticalExtent+levelRows) {
			this.transform.position = new Vector3 (xPosition+1, verticalExtent+levelRows, -10);
			//Debug.Log ("Camera blocked top");
		} else {
			this.transform.position = new Vector3 (xPosition, yPosition, -10);
		}
			

		//if (xPosition < horizonExtent) {
		//	this.transform.position = new Vector3 (xPosition, yPosition, -10);		
		//}

		//If the camera is in the world's positive zone within the level's boundaries, it will follow the player's movement.
		//if (this.transform.position.x > horizonExtent && this.transform.position.x < (levelColumns-horizonExtent) && this.transform.position.y > verticalExtent && this.transform.position.y < (levelRows-horizonExtent)) {
			//this.transform.position = new Vector3 (xPosition, yPosition, -10);
		//}
	}
}
