using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class LevelVisual : MonoBehaviour {

	//Import the prefabs of levels
	public GameObject Coin;
	public GameObject End;
	public GameObject[] DirtTiles;
	public GameObject[] Flowers;

	//Lists all existing objects of the level
	public List<List<GameObject>> existingObjectsArray = new List<List<GameObject>>();

	//Imports the complex algorithm that connect the tiles
	public GameObject tileConnectorImporter;

	Vector3 spawnPosition = new Vector3 ();

	void Start () {
		//Checks what is the current level or if a new level must be created
		loadCurrentLevelData ();
		//Create the visual of this level
		instantiateLevel ();
	}

	public void loadCurrentLevelData () {
		//Load scene transition settigns
		SaveAndLoadData.LoadSettings ();
		SettingsData.current = SaveAndLoadData.savedSettings;

		//Load the list of levels
		SaveAndLoadData.LoadLevel ();
		LevelData.current = new LevelData ();

		//Get to the right saved level slot
		try
		{	
			LevelData.current = SaveAndLoadData.savedLevels[SettingsData.current.loadLevelId];
			Debug.Log("Level was loaded : "+ SettingsData.current.loadLevelId);
		}
		//Create a new level if needed
		catch 
		{
			LevelData.current.createNewLevel ();
		}
	}

	public void instantiateLevel () {
		//Create an empty list with the levels dimension
		for (int x = 0; x < LevelData.current.LevelRows; x++) {
			existingObjectsArray.Add (new List<GameObject> ());
			for (int y = 0; y < LevelData.current.LevelColumns; y++) {
				existingObjectsArray [x].Add (null);
			}
		}
		//Add all the loaded level's object
		for (int x = 0; x < LevelData.current.LevelRows; x++) {
			for (int y = 0; y < LevelData.current.LevelColumns; y++) {
				if (LevelData.current.level [x] [y] != "Empty") {
					AddRemoveObject(LevelData.current.level [x] [y], true, x, y);
				}
			}
		}
	}

	//Funtion to add or remove level objects
	public void AddRemoveObject(string objectType, bool addObject, int levelRow, int levelColumn) {

		//On call, update the current level data
		LevelData.current.updateLevel(objectType, addObject, levelRow, levelColumn);

		//Import the Tile Connector
		TileConnector tileConnect = tileConnectorImporter.GetComponent<TileConnector> ();

		//Detect if the object is of the connectable type --> detect if there are others connectable tiles around its position
		bool connectableTile = tileConnect.isConnectable (objectType);
		bool[] surroundingTiles = new bool[9];
		if (connectableTile) {
			surroundingTiles = tileConnect.detectSurroundingTiles (objectType, levelRow, levelColumn, LevelData.current.level);
		}

		//Destroy or add the attributed object
		if (addObject) {
			instantiateObject(objectType,levelRow,levelColumn, surroundingTiles);
		} else {
			Destroy(existingObjectsArray [levelRow] [levelColumn]);
		}

		//If the object is connectable, check if objects needs to be updated after its appearance/disappearance
		if (connectableTile) {
			tileConnect.updateSurroundingObjects (objectType, levelRow, levelColumn, surroundingTiles, LevelData.current.level);
		}
	}

	//Custom function to have a better Instantiate function
	//Instantiate the selected object at the level's Row/Column position
	public void instantiateObject (string objectType, int levelRow, int levelColumn, bool[] surroundingTiles) {

		//Create an gameobject instance to avoid overide in List
		GameObject Tile = null;

		//Adjust the object's spawn position to fit the level grid
		spawnPosition.x = levelColumn-1.5f;
		spawnPosition.y = levelRow-1.5f;
		spawnPosition.z = 0;

		if (existingObjectsArray [levelRow] [levelColumn] != null) {
			Destroy (existingObjectsArray [levelRow] [levelColumn]);
		}

		//Debug.Log ("Position de la souris:"+MouseXY.x+","+MouseXY.y+"/Position de l'objet:"+spawnPosition.x+","+spawnPosition.y);
		TileConnector tileConnect = tileConnectorImporter.GetComponent<TileConnector> ();

		//Instantiate the selected object at the desired position
		if (objectType == "Tile") {
			int resultObject = tileConnect.getConnectedSprite (surroundingTiles);
			GameObject newTile = (GameObject)Instantiate (DirtTiles [resultObject], spawnPosition, transform.rotation);
			Tile = newTile;
		} else if (objectType == "Coin") {
			GameObject newTile = (GameObject)Instantiate (Coin, spawnPosition, transform.rotation);
			Tile = newTile;
		} else if (objectType == "End") {
			GameObject newTile = (GameObject)Instantiate (End, spawnPosition, transform.rotation);
			Tile = newTile;
		}

		//Update the existing object array
		existingObjectsArray[levelRow][levelColumn] = Tile;
	}

	public void setName(string newName){
		//Set the name of the level from a text input
		LevelData.current.name = newName;
	}

	public void saveClick() {
		//Save the level from the save button
		Debug.Log (LevelData.current.name+LevelData.current.offlineID);
		LevelData.current.newLevel = false;
		SaveAndLoadData.SaveLevel ();
	}

}
