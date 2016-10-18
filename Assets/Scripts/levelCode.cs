using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

[System.Serializable]
public class levelCode {

	public static levelCode current;

	public string name;
	public List<List<string>> level = new List<List<string>>();

	public int date;
	public int month;
	public int year;

	public bool published;

	public List<float> playerScores = new List<float>();

	public int LevelRows;
	public int LevelColumns;

	public element levelElement;
	public LevelObjectives objectives;
	public HiddenObject equipmentObject;
	public HiddenObject editorObject;

	public levelCode(){
		Debug.Log ("LevelCode");
		name = "Untilted";
		LevelRows = 100;
		LevelColumns = 100;
		setLevelArray ();
	}

	public void setLevelArray () {
		for(int x=0; x < LevelRows; x++){
			level.Add(new List<string> ());
			for(int y=0; y < LevelColumns; y++){
				level[x].Add("Empty");
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
	}

	public void updateLevel(string objectType, bool addObject, int levelRow, int levelColumn) {
		//If we add an object
		if (addObject) {
			//The object type is stored in the level array
			level [levelRow] [levelColumn] = objectType;
			//Debug.Log (levelRow + "/" + levelColumn);
		} else { //If we remove an object
			//The previous object type is removed from the level array
			level [levelRow] [levelColumn] = "Empty";
		}
	}
}
