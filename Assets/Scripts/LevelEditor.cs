using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelEditor : MonoBehaviour {

	public GameObject Coin;
	public GameObject gridPattern;

	public int LevelRows;
	public int LevelColumns;

	Vector3 MouseXY = new Vector3();
	Vector3 spawnPosition = new Vector3 ();

	public GameObject tileConnectorImporter;

	//private List<List<string>>[] UndoStates = new List<List<string>>[10];

	//private List<levelCase> levelArray2 = new List<levelCase> ();

	private List<List<List<string>>> UndoStates = new List<List<List<string>>>();
	private int SaveState = 0;

	private List<List<string>> levelArray = new List<List<string>>();
	private List<List<GameObject>> existingObjectsArray = new List<List<GameObject>>();

	public GameObject[] DirtTiles;
	public GameObject[] Flowers;

	//Value sent from editor buttons
	[HideInInspector] public string objectSelected;

	void Start () {
		drawTheGrid();
	}

	// Update is called once per frame
	void Update() {
		bool userClicked = detectInputPosition ();
		if (userClicked) {
			int levelRow = (int)Mathf.Abs (MouseXY.y);
			int levelColumn = (int)Mathf.Abs (MouseXY.x);
			bool FreeLocation = checkLevelPosition (levelRow, levelColumn);
			bool addObject;
			if (FreeLocation && objectSelected != "None") {
				addObject = true;
				updateLevel (objectSelected, addObject, levelRow, levelColumn);
				storeLevelChanges ();
			} else if (FreeLocation == false && objectSelected == "Eraser") {
				objectSelected = levelArray [levelRow] [levelColumn];
				addObject = false;
				updateLevel (objectSelected, addObject, levelRow, levelColumn);
				storeLevelChanges ();
			}
			userClicked = false;
			FreeLocation = false;
		}
	}

	void drawTheGrid (){
		for (int x = 0; x < LevelRows; x++) {
			for (int y = 0; y < LevelColumns; y++) {
				Vector3 gridPosition = new Vector3 ();
				gridPosition.x = x+0.5f;
				gridPosition.y = y+0.5f;
				gridPosition.z = 0;
				Instantiate (gridPattern, gridPosition, transform.rotation);
			}
		}
	}

	void updateLevel(string objectType, bool addObject, int levelRow, int levelColumn) {
		//Import the tile connector
		tileConnector tileConnect = tileConnectorImporter.GetComponent<tileConnector> ();

		//If we add an object
		if (addObject) {
			//The object type is stored in the level array
			levelArray [levelRow] [levelColumn] = objectType;
			Debug.Log(levelRow+"/"+levelColumn);
		} else { //If we remove an object
			//The previous object type is removed from the level array
			levelArray[levelRow][levelColumn]="Empty";
		}

		//Detect if the object is of the connectable type --> detect if there are others connectable tiles around its position
		bool connectableTile = tileConnect.isConnectable (objectType);
		bool[] surroundingTiles = new bool[9];
		if (connectableTile) {
			surroundingTiles = tileConnect.detectSurroundingTiles (objectType, levelRow, levelColumn, levelArray);
		}

		if (addObject) {
			instantiateObject(objectType,levelRow,levelColumn, surroundingTiles);
			//Debug.Log("Object created "+objectType);
		} else {
			Destroy(existingObjectsArray [levelRow] [levelColumn]);
			//Debug.Log("Object deleted "+objectType);
		}

		//If the object is connectable, check if objects needs to be updated after its appearance/disappearance
		if (connectableTile) {
			tileConnect.updateSurroundingObjects (objectType, levelRow, levelColumn, surroundingTiles, levelArray);
		}
	}

	private bool detectInputPosition () {
		if (Input.GetButton ("Fire1")) {

			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
				return false;
			}

			MouseXY = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			MouseXY.x = Mathf.Floor (MouseXY.x) + 2.5f;
			MouseXY.y = Mathf.Floor (MouseXY.y) + 2.5f;
			MouseXY.z = 0;
			return true;
		} else {
			return false;
		}
	}

	void addDecoration(){
		int randomDecoration;
		float randomizer;
		//randomizer = Random ();
		//randomDecoration = Mathf.RoundToInt(randomizer);
		//if (randomDecoration == 1) {
		//	if (surroundingTiles [1] == false) {
				
		//	}
		//}
	}

	private bool checkLevelPosition (int levelRow, int levelColumn) {
		//Debug.Log("La row est :"+levelRow+" et la column est :"+ levelColumn);
		if (levelArray [levelRow] [levelColumn] == "Empty") {
			return true;
		} else {
			//Debug.Log ("Case Occupied");
			return false;
		}
	}
		
	public void instantiateObject (string objectType, int levelRow, int levelColumn, bool[] surroundingTiles) {
		GameObject Tile = null;
		spawnPosition.x = levelColumn-1.5f;
		spawnPosition.y = levelRow-1.5f;
		spawnPosition.z = 0;

		if (existingObjectsArray [levelRow] [levelColumn] != null) {
			Destroy (existingObjectsArray [levelRow] [levelColumn]);
		}

		//Debug.Log ("Position de la souris:"+MouseXY.x+","+MouseXY.y+"/Position de l'objet:"+spawnPosition.x+","+spawnPosition.y);

		if (objectType == "Tile") {
			tileConnector tileConnect = tileConnectorImporter.GetComponent<tileConnector> ();
			int resultObject = tileConnect.getConnectedSprite (surroundingTiles);
			GameObject newTile = (GameObject) Instantiate (DirtTiles[resultObject], spawnPosition, transform.rotation);
			Tile = newTile;
		}
		else if(objectType == "Coin"){
			GameObject newTile = (GameObject) Instantiate (Coin, spawnPosition, transform.rotation);
			Tile = newTile;
		}

		existingObjectsArray[levelRow][levelColumn] = Tile;
	}
		
	public void UndoLevelChanges(){
		if (SaveState >= 1) {
			SaveState -= 2;
			loadLevel (UndoStates[SaveState]);
			Debug.Log ("Return to save state "+SaveState);
		}
	}

	public void storeLevelChanges() {
		UndoStates.Add(levelArray);
		Debug.Log ("Changes saved at state "+SaveState);
		SaveState += 1;
	}

	public void setLevelArray () {
		for(int x=0; x < LevelRows; x++){
			levelArray.Add(new List<string> ());
			existingObjectsArray.Add(new List<GameObject> ());
			for(int y=0; y < LevelColumns; y++){
				levelArray[x].Add("Empty");
				existingObjectsArray[x].Add(null);
			}
		}

		for (int i = 1; i < LevelRows-1; i++) {
			updateLevel ("Tile",true, i, 2);
			updateLevel ("Tile",true, i, 3);
			updateLevel ("Tile",true, i, LevelColumns-2);
			updateLevel ("Tile",true, i, LevelColumns-3);
		}
		//Debug.Log ("LevelInitiated");
		updateLevel("Tile",true,1,3);
		updateLevel("Tile",true,1,4);
		updateLevel("Tile",true,1,5);
		updateLevel("Tile",true,2,3);
		updateLevel("Tile",true,2,4);
		updateLevel("Tile",true,2,5);
		updateLevel("Coin",true,8, 5);
		storeLevelChanges ();
	}

	void loadLevel(List<List<string>> selectedLevel){
		//For each cases
		for (int x = 0; x < LevelRows; x++) {
			for (int y = 0; y < LevelColumns; y++) {
				Debug.Log ("SelectedLevel "+x+"/"+y+" "+selectedLevel[x][y]);
				Debug.Log ("levelArray "+x+"/"+y+" "+levelArray[x][y]);
				//If a case in the actual level and the loaded level is different
				if (levelArray[x][y] != selectedLevel[x][y]) {
					Debug.Log ("The case "+x+"/"+y+"is different");
					//If the case of the loaded level is empty --> delete case content
					if (selectedLevel [x] [y] == "Empty") {
						updateLevel (levelArray [x] [y], false, x, y);
					}
					else { //Else destroy the actual content and add the new one 
						updateLevel (levelArray [x] [y], false, x, y);
						updateLevel (selectedLevel [x] [y], true, x, y);
					}
				}
			}
		}
	}
}
