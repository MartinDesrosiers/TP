using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public GameObject hero;
	public int levelRows;
	public int levelColumns;
	private float horizonExtent;
	private float verticalExtent;
	float xPosition;
	float yPosition;

	private float horizontalForce = 0;
	private float verticalForce = 0;

	public bool controlBehavior = false;
	public bool lockPosition = false;

	//Mobile controls
	private Vector2 touchOrigin = -Vector2.one;

	// Use this for initialization
	void Start () {
		//Horizontal half of the screen compared to world units
		horizonExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		//Vertical half of the screen compared to world units
		verticalExtent = Camera.main.orthographicSize+1;
		//Debug.Log ("Screen Boundaries x :"+horizonExtent+" /y :"+verticalExtent);
	}

	// Update is called once per frame
	void Update () {
		followHero ();
	}

	void followHero (){
		if (controlBehavior) {
			//Camera controls
			controlPositions ();
		} else if(!lockPosition){
			//Hero's position
			xPosition = hero.transform.position.x;
			yPosition = hero.transform.position.y;
		}

		if (xPosition <= horizonExtent + 1 && yPosition <= verticalExtent - 1) {
			xPosition = horizonExtent + 1;
			yPosition = verticalExtent - 1;
			Debug.Log ("Camera blocked left/bottom");
		} else if (xPosition <= horizonExtent + 1 && yPosition >= verticalExtent+levelRows) {
			xPosition = horizonExtent + 1;
			yPosition = verticalExtent + levelRows;
			Debug.Log ("Camera blocked left/top");
		} else if (xPosition >= horizonExtent + levelColumns && yPosition <= verticalExtent - 1) {
			xPosition = horizonExtent + levelColumns;
			yPosition = verticalExtent - 1;
			Debug.Log ("Camera blocked right/bottom");
		} else if (xPosition >= horizonExtent+levelColumns && yPosition >= verticalExtent+levelRows) {
			xPosition = horizonExtent + levelColumns;
			yPosition = verticalExtent + levelRows;
			Debug.Log ("Camera blocked right/top");
		} else if (yPosition <= verticalExtent - 1) {
			xPosition = xPosition;
			yPosition = verticalExtent - 1;
			Debug.Log ("Camera blocked bottom");
		} else if (xPosition <= horizonExtent + 1) {
			xPosition = horizonExtent + 1;
			yPosition = yPosition;
			Debug.Log ("Camera blocked left");
		} else if (xPosition >= horizonExtent + levelColumns) {
			xPosition = horizonExtent + levelColumns;
			yPosition = yPosition;
			Debug.Log ("Camera blocked right");
		}  else if (yPosition >= verticalExtent + levelRows) {
			xPosition = xPosition;
			yPosition = verticalExtent + levelRows;
			Debug.Log ("Camera blocked top");
		}
			this.transform.position = new Vector3 (xPosition, yPosition, -10);
	}

	void controlPositions() {
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

		horizontalForce = Input.GetAxis ("Horizontal");
		verticalForce = Input.GetAxis ("Vertical");

		xPosition += horizontalForce;
		yPosition += verticalForce;

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
			xPosition-=x;
			yPosition-=y;
		}
	}
		#endif
	}

	public void editorControls(bool editorActive){
		controlBehavior = editorActive;
		Debug.Log (controlBehavior);
	}

	public void freezeCamera(bool lockUnlocked){
		lockPosition = lockUnlocked;
	}
}
