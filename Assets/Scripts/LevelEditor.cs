using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour {

	public GameObject Coin;

	public int LevelRows;
	public int LevelColumns;

	public float CameraPaddingX;
	public float CameraPaddingY;

	Vector3 MouseXY = new Vector3();
	Vector3 spawnPosition = new Vector3 ();

	private bool[] surroundingTiles = new bool[9];

	private List<List<int>> levelArray = new List<List<int>>();
	private List<List<GameObject>> existingObjectsArray = new List<List<GameObject>>();

	public GameObject[] DirtTiles;

	//Value sent from editor buttons
	[HideInInspector] public string objectSelected;

	// Use this for initialization
	void Start () {
		setLevelArray ();
	}

	// Update is called once per frame
	void Update () {
		bool attemptCreateObject = detectInputPosition ();
		if (attemptCreateObject) {
			int levelRow = (int)Mathf.Abs(MouseXY.y - CameraPaddingY);
			int levelColumn = (int)Mathf.Abs(MouseXY.x - CameraPaddingX);
			bool FreeLocation = checkLevelPosition (levelRow,levelColumn);
			if (FreeLocation) {
				int objectType = checkObjectType();
				detectSurroundingTiles(objectType,levelRow,levelColumn);
				int resultObject = selectTileSprite();
				instantiateObject(resultObject,levelRow,levelColumn);
				updateSurroundingObjects(objectType,levelRow,levelColumn);
				updateLevelArray();
			}
			attemptCreateObject = false;
			FreeLocation = false;
		}
	}

	private bool detectInputPosition () {
		if (Input.GetButton ("Fire1")) {
			MouseXY = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			MouseXY.x = Mathf.Floor (MouseXY.x) + 0.5f;
			MouseXY.y = Mathf.Floor (MouseXY.y) + 0.5f;
			MouseXY.z = 0;
			return true;
		} else {
			return false;
		}
	}

	private bool checkLevelPosition (int levelRow, int levelColumn) {
		Debug.Log("La row est :"+levelRow+" et la column est :"+ levelColumn);
		if (levelArray [levelRow] [levelColumn] == 0) {
			int objectType = checkObjectType ();
			//Store the object type in the level array
			levelArray [levelRow] [levelColumn] = objectType;
			Debug.Log ("Object type id is :"+levelArray [levelRow] [levelColumn]);
			return true;
		} else {
			Debug.Log ("Case Occupied");
			return false;
		}
	}

	private int checkObjectType() {

		switch (objectSelected) {
		case "Tile":
			return 1;
			break;

		case "Coin":
			return 2;
			break;

		default:
			return 0;
			break;
		}
	}
		
	void detectSurroundingTiles (int tileType, int levelRow, int levelColumn) {
		//Stores in an array if the surrounding tiles are connected (bool)
		int t = 0;
		//r for rows and c for columns around the tile (-1,0,1)
		for (int r = 1; r > -2 ; r--) {
			for (int c = -1; c < 2; c++) {
				//Debug.Log ("Position relative vérifiée = Row :"+r+"("+(levelRow + r)+")"+" Column :"+c+"("+(levelColumn + c)+")"+" == "+levelArray[levelRow + r][levelColumn + c]);
				//Except for the tile itself
				//if (r != 0 && c != 0) {
					//If they are the same = true
					if (tileType == levelArray[levelRow + r][levelColumn + c]) {
						//Debug.Log("Case Row :"+(levelRow+r)+" column :"+(levelColumn + c)+" is a sibbling Tile");
						surroundingTiles[t] = true;
					} else {
						surroundingTiles[t] = false;
					}
					t++;
				//}
			}
		}
		Debug.Log ("surroundingTiles[] ==" + "[0 :"+surroundingTiles[0]+"], [1 :"+surroundingTiles[1]+"], [2 :"+surroundingTiles[2]+"], [3 :"+surroundingTiles[3]+"], [4 :"+surroundingTiles[4]+"], [5 :"+surroundingTiles[5]+"], [6 :"+surroundingTiles[6]+"], [7 :"+surroundingTiles[7]+"], [8 :"+surroundingTiles[8]+"]");
	}

	private int selectTileSprite() {

		int correctTile = 0;

		if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 0; //Dirt
		}
		else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 16; //DirtCTopLeft
		}
		else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 26; //DirtTop
		}
		else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 26; //DirtTop
		}
		else if (surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 28; //DirtTopLeft
		}
		else if (surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [8]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 26; //DirtTop
		}
		else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 26; //DirtTop
		}
		else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 28; //DirtTopLeft
		}
		else if (surroundingTiles [0] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [0] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [0] && surroundingTiles [8]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [0]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 40; //Dirt2CTop
		}
		else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 12; //DirtClTopRight
		}
		else if (surroundingTiles [1] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 19; //DirtLeftRight
		}
		else if (surroundingTiles [1] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 19; //DirtLeftRight
		}
		else if (surroundingTiles [1] && surroundingTiles [8]){
			correctTile = 22; //DirtSingleBottom
		}
		else if (surroundingTiles [1]){
			correctTile = 22; //DirtSingleBottom
		}		
		else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 28; //DirtTopLeft
		}
		else if (surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [2] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [2] && surroundingTiles [8]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [2]){
			correctTile = 21; //DirtSingle
		}		
		else if (surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 29; //DirtTopRight
		}
		else if (surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 8; //DirtCCBottomLeft
		}
		else if (surroundingTiles [3] && surroundingTiles [8]){
			correctTile = 24; //DirtSingleRight
		}
		else if (surroundingTiles [3]){
			correctTile = 24; //DirtSingleRight
		}
		else if (surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]){
			correctTile = 28; //DirtTopLeft
		}
		else if (surroundingTiles [5] && surroundingTiles [8]){
			correctTile = 23; //DirtSingleLeft
		}
		else if (surroundingTiles [5]){
			correctTile = 23; //DirtSingleLeft
		}
		else if (surroundingTiles [6] && surroundingTiles [8]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [6]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [7]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [8]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]){
			correctTile = 5; //DirtCBottomRight
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]){
			correctTile = 1; //DirtBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5]){
			correctTile = 1; //DirtBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3]){
			correctTile = 28; //DirtTopLeft
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2]){
			correctTile = 22; //DirtSingleBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1]){
			correctTile = 22; //DirtSingleBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]){
			correctTile = 37; //Dirt2CBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7]){
			correctTile = 13; //DirtCRBottomLeft
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [7]){
			correctTile = 19; //DirtLeftRight
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [7]){
			correctTile = 19; //DirtLeftRight
		}
		else if (surroundingTiles [0] && surroundingTiles [7]){
			correctTile = 25; //DirtSingleTop
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6]){
			correctTile = 3; //DirtBottomRight
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6]){
			correctTile = 22; //DirtSingleBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [6]){
			correctTile = 22; //DirtSingleBottom
		}
		else if (surroundingTiles [0] && surroundingTiles [6]){
			correctTile = 21; //DirtSingle
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5]){
			correctTile = 10; //DirtCCTopRight
		}
		else if (surroundingTiles [0] && surroundingTiles [5]){
			correctTile = 23; //DirtSingleLeft
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5]){
			correctTile = 2; //BottomLeft
		}
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5]){
			correctTile = 10; //DirtCCTopRight
		}
		else if (surroundingTiles [0] && surroundingTiles [5]){
			correctTile = 23; //DirtSingleLeft
		}		
		else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3]){
			correctTile = 3; //BottomRight
		}
		else if (surroundingTiles [0] && surroundingTiles [3]){
			correctTile = 24; //DirtSingleRight
		}
		else if (surroundingTiles [0] && surroundingTiles [2]){
			correctTile = 21; //DirtSingle
		}
		else{
			correctTile = 21; //DirtSingle
		}



		Debug.Log("Correct tile is :"+correctTile);

		return correctTile;
	}

	void instantiateObject (int resultObject, int levelRow, int levelColumn) {
		spawnPosition.x = levelColumn+0.5f;
		spawnPosition.y = levelRow+0.5f;
		spawnPosition.z = 0;
		Debug.Log ("Position de la souris:"+MouseXY.x+","+MouseXY.y+"/Position de l'objet:"+spawnPosition.x+","+spawnPosition.y);
		//((levelRow + 0.5f),(levelColumn + 0.5f),0f);
		GameObject Tile = DirtTiles[resultObject];
		existingObjectsArray[levelRow][levelColumn] = Tile;
		//Instantiate (existingObjectsArray[levelRow][levelColumn], new Vector3(levelRow,levelColumn,5), transform.rotation);
		Instantiate (existingObjectsArray[levelRow][levelColumn], spawnPosition, transform.rotation);
	}

	void updateSurroundingObjects (int objectType, int levelRow, int levelColumn){
		bool[] tilesToModify = new bool[9];
		int thisRow = levelRow;
		int thisColumn = levelColumn;
		//Check the surrounding objects nature

		for (int t = 0; t < (tilesToModify.Length); t++) {
			//Save center tile data in array
			tilesToModify[t]=surroundingTiles[t];
		}

		for(int i = 0; i < (tilesToModify.Length); i++){
			//If the tiles needs to be modified and not center tile
			if (tilesToModify[i] && i !=4) {
				//Find its position
				switch (i) {
				case 0:
					thisRow = (levelRow + 1);
					thisColumn = (levelColumn - 1);
					break;
				case 1:
					thisRow = (levelRow + 1);
					thisColumn = levelColumn;
					break;
				case 2:
					thisRow = (levelRow + 1);
					thisColumn = (levelColumn + 1);
					break;
				case 3:
					thisRow = levelRow;
					thisColumn = (levelColumn - 1);
					break;
				case 5:
					thisRow = levelRow;
					thisColumn = (levelColumn + 1);
					break;
				case 6:
					thisRow = (levelRow - 1);
					thisColumn = (levelColumn - 1);
					break;
				case 7:
					thisRow = (levelRow - 1);
					thisColumn = levelColumn;
					break;
				case 8:
					thisRow = (levelRow - 1);
					thisColumn = (levelColumn + 1);
					break;
				default:
					break;
				}
				Debug.Log ("To modify : tilesToModify["+i+"]");
				detectSurroundingTiles(objectType,thisRow,thisColumn);
				int resultObject = selectTileSprite();
				instantiateObject (resultObject, thisRow, thisColumn);
			}
		}
	}

	void setLevelArray () {
		for(int x=0; x < LevelRows; x++){
			levelArray.Add(new List<int> ());
			existingObjectsArray.Add(new List<GameObject> ());
			for(int y=0; y < LevelColumns; y++){
				levelArray[x].Add(0);
				existingObjectsArray[x].Add(null);
				if (System.Convert.ToBoolean(levelArray [x] [y])) {
					//setTile(levelArray [x][y]);

					//int correctTile = selectTileSprite();

					//GameObject Tile = DirtTiles[correctTile]; 
					//existingObjectsArray[x].Add(Tile);
				}
			}
		}
	}

	void updateLevelArray () {

	}
}
