using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class LevelEditor : MonoBehaviour {

	public GameObject heroImporter;
	public GameObject levelImporter;

	Vector3 MouseXY = new Vector3();

	private List<List<List<string>>> UndoStates = new List<List<List<string>>>();
	private int SaveState = 0;
	private int MaxState = 0;

		public List<string> undoObjects = new List<string>();
	public List<bool> undoType = new List<bool>();
	public List<int> undoColumn = new List<int>();
	public List<int> undoRow = new List<int>();

	//Value sent from editor buttons
	[HideInInspector] public string objectSelected;

	void Start () {
		//HeroController characterControls = heroImporter.GetComponent<HeroController>();
		//characterControls.playable = false;
	}

	// Update is called once per frame
	void Update() {
		bool userClicked = detectInputPosition ();
		if (userClicked) {
			levelVisual LevelVisual = levelImporter.GetComponent<levelVisual> ();
			int levelRow = (int)Mathf.Abs (MouseXY.y);
			int levelColumn = (int)Mathf.Abs (MouseXY.x);
			bool FreeLocation = checkLevelPosition (levelRow, levelColumn);
			bool addObject;
			if (FreeLocation && objectSelected != "None" && objectSelected != "Eraser") {
				addObject = true;
				LevelVisual.AddRemoveObject(objectSelected, addObject, levelRow, levelColumn);
				storeChanges (objectSelected,addObject, levelRow,levelColumn);
			} else if (FreeLocation == false && objectSelected == "Eraser") {
				string objectToDelete;

				objectToDelete = levelCode.current.level [levelRow] [levelColumn];
				addObject = false;
				LevelVisual.AddRemoveObject(objectToDelete, addObject, levelRow, levelColumn);
				storeChanges (objectToDelete, addObject, levelRow,levelColumn);
			}
			userClicked = false;
			FreeLocation = false;
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

	private bool checkLevelPosition (int levelRow, int levelColumn) {
		//Debug.Log("La row est :"+levelRow+" et la column est :"+ levelColumn);
		if (levelCode.current.level [levelRow] [levelColumn] == "Empty") {
			return true;
		} else {
			//Debug.Log ("Case Occupied");
			return false;
		}
	}

	public void storeChanges(string objectSelected, bool objectAdded, int levelRow, int levelColumn){

		if (SaveState < MaxState) {
			undoObjects [SaveState] = objectSelected;
			undoType [SaveState] = objectAdded;
			undoRow [SaveState] = levelRow;
			undoColumn [SaveState] = levelColumn;
		} else if (SaveState >= MaxState) {
			undoObjects.Add (objectSelected);
			undoType.Add(objectAdded);
			undoRow.Add (levelRow);
			undoColumn.Add (levelColumn);
			MaxState += 1;
		}
		SaveState += 1;
		Debug.Log ("Changes saved at state "+SaveState);

	}

	public void undoChanges(){
		if (SaveState > 0) {
			SaveState -= 1;
			bool objectAdded;
			objectAdded = !undoType[SaveState];
			Debug.Log ("Return to save state " + SaveState);
			Debug.Log ("Object undid " + undoObjects [SaveState]);
			Debug.Log ("Object added? " + objectAdded);
			levelVisual LevelVisual = levelImporter.GetComponent<levelVisual> ();
			LevelVisual.AddRemoveObject (undoObjects [SaveState], objectAdded, undoRow [SaveState], undoColumn [SaveState]);
		}
	}
}
