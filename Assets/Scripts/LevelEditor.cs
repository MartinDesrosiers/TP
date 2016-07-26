using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour {

	public GameObject Tile;
	public GameObject Coin;

	public int LevelRows;
	public int LevelColumns;

	public float CameraPaddingX;
	public float CameraPaddingY;

	//private int[,] levelTileArray = new int[,] {{}};
	private List<List<int>> levelArray = new List<List<int>>();


	[HideInInspector] public string objectSelected;

	// Use this for initialization
	void Start () {
		setLevelArray ();
	}
	
	// Update is called once per frame
	void Update () {
		detectInputPosition ();
	}

	void detectInputPosition () {
		if (Input.GetButton("Fire1")) {
			Vector3 MouseXY = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			MouseXY.x =Mathf.Floor(MouseXY.x)+0.5f;
			MouseXY.y =Mathf.Floor(MouseXY.y)+0.5f;
			MouseXY.z =0;
			Debug.Log ("An object was added at these coordinates : " + MouseXY);
			checkLevelPosition (MouseXY);
		}
	}

	void checkLevelPosition (Vector3 MouseXY) {
		float arrayColumn = Mathf.Abs(MouseXY.x - CameraPaddingX);
		float arrayRow = Mathf.Abs(MouseXY.y - CameraPaddingY);
		int intColumn = (int)arrayColumn;
		int intRow = (int)arrayRow;
		Debug.Log("La row est :"+intRow+" et la column est :"+ intColumn);
		if (levelArray[intRow][intColumn] == 0) {
			updateLevel (MouseXY, intRow, intColumn);
		}
	}

	void updateLevel (Vector3 MouseXY, int arrayRow, int arrayColumn){
		switch (objectSelected) {
		case "Tile":
			levelArray[arrayRow][arrayColumn] = 1;
			Debug.Log("À la tuile "+arrayRow+"x"+arrayColumn+", nous avons :"+levelArray[arrayRow][arrayColumn]);
			Instantiate (Tile, MouseXY, transform.rotation);
			break;
			case "Coin":
			levelArray[arrayRow][arrayColumn] = 2;
			Debug.Log("À la tuile "+arrayRow+"x"+arrayColumn+", nous avons :"+levelArray[arrayRow][arrayColumn]);
			Instantiate (Coin, MouseXY, transform.rotation);
			break;
		}
	}

	void setLevelArray () {
		for(int x=0; x < LevelRows; x++){
			levelArray.Add(new List<int> ());
			for(int y=0; y < LevelColumns; y++){
				levelArray[x].Add(0);
			}
		}
	}
}
