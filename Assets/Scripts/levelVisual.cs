using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class levelVisual : MonoBehaviour {

	public GameObject Coin;
	public GameObject End;
	public GameObject[] DirtTiles;
	public GameObject[] Flowers;

	public List<List<GameObject>> existingObjectsArray = new List<List<GameObject>>();

	public GameObject tileConnectorImporter;

	Vector3 spawnPosition = new Vector3 ();

	// Use this for initialization
	void Start () {
		levelCode.current = new levelCode ();
		instantiateLevel ();
	}

	public void instantiateLevel () {
		for (int x = 0; x < levelCode.current.LevelRows; x++) {
			existingObjectsArray.Add (new List<GameObject> ());
			for (int y = 0; y < levelCode.current.LevelColumns; y++) {
				existingObjectsArray [x].Add (null);
			}
		}
		for (int x = 0; x < levelCode.current.LevelRows; x++) {
			for (int y = 0; y < levelCode.current.LevelColumns; y++) {
				if (levelCode.current.level [x] [y] != "Empty") {
					AddRemoveObject(levelCode.current.level [x] [y], true, x, y);
				}
			}
		}
	}

	public void AddRemoveObject(string objectType, bool addObject, int levelRow, int levelColumn) {
		levelCode.current.updateLevel(objectType, addObject, levelRow, levelColumn);

		tileConnector tileConnect = tileConnectorImporter.GetComponent<tileConnector> ();

		//Detect if the object is of the connectable type --> detect if there are others connectable tiles around its position
		bool connectableTile = tileConnect.isConnectable (objectType);
		bool[] surroundingTiles = new bool[9];
		if (connectableTile) {
			surroundingTiles = tileConnect.detectSurroundingTiles (objectType, levelRow, levelColumn, levelCode.current.level);
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
			tileConnect.updateSurroundingObjects (objectType, levelRow, levelColumn, surroundingTiles, levelCode.current.level);
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
		tileConnector tileConnect = tileConnectorImporter.GetComponent<tileConnector> ();

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

		existingObjectsArray[levelRow][levelColumn] = Tile;
	}

}
