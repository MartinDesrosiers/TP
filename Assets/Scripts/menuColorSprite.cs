using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuColorSprite : MonoBehaviour {

	public GameObject HeroObject;
	public GameObject editorImporter;
	public GameObject levelImporter;

	public GameObject topMenu;

	public Color[] menuColors;
	public GameObject categoryLabel;
	public GameObject[] ObjectToggles;
	public GameObject undoButton;

	public Sprite[] levelTogglesSprites;
	public Sprite[] enemyTogglesSprites;
	public Sprite[] collectibleTogglesSprites;
	public Sprite[] trapTogglesSprites;
	public Sprite[] groundTogglesSprites;
	public Sprite[] editTogglesSprites;

	public Sprite lockSprite;

	public GameObject gridPattern;

	// Use this for initialization
	void Start () {
		CategoryChanged ("groundTGL");
		drawTheGrid();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void drawTheGrid (){
		for (int x = 0; x < levelCode.current.LevelRows; x++) {
			for (int y = 0; y < levelCode.current.LevelColumns; y++) {
				Vector3 gridPosition = new Vector3 ();
				gridPosition.x = x+0.5f;
				gridPosition.y = y+0.5f;
				gridPosition.z = 0;
				Instantiate (gridPattern, gridPosition, transform.rotation);
			}
		}
	}

	public void CategoryChanged(string categorySelected){

		LevelEditor levelEditor = editorImporter.GetComponent<LevelEditor> ();

		Debug.Log ("Edit Mode : None Selected");
		levelEditor.objectSelected = "None";

		MainScript mainScript = (MainScript)HeroObject.GetComponent<MainScript> ();

		int theMenuColor;
		string categoryText;

		switch (categorySelected) {
		case "levelTGL":
			theMenuColor = 0;
			categoryText = "LEVEL"; 
			updateTopMenu (theMenuColor, categoryText, levelTogglesSprites, mainScript.unlockedLevelObjects);
			break;
		case "enemyTGL":
			theMenuColor = 1;
			categoryText = "ENEMIES"; 
			updateTopMenu (theMenuColor, categoryText, enemyTogglesSprites, mainScript.unlockedEnemyObjects);
			break;
		case "collectibleTGL":
			theMenuColor = 2;
			categoryText = "OBJECTS"; 
			updateTopMenu (theMenuColor, categoryText, collectibleTogglesSprites, mainScript.unlockedCollectibleObjects);
			break;
		case "trapTGL":
			theMenuColor = 3;
			categoryText = "TRAPS"; 
			updateTopMenu (theMenuColor, categoryText, trapTogglesSprites, mainScript.unlockedTrapObjects);
			break;
		case "groundTGL":
			theMenuColor = 4;
			categoryText = "GROUND"; 
			updateTopMenu (theMenuColor, categoryText, groundTogglesSprites, mainScript.unlockedGroundObjects);
			break;
		case "editTGL":
			theMenuColor = 5;
			categoryText = "EDIT"; 
			updateTopMenu (theMenuColor, categoryText, editTogglesSprites, mainScript.unlockedEditObjects);
			break;
		}
	}

	void updateTopMenu(int theMenuColor, string theText, Sprite[] selectedToggles, string[] unlockedSelectedObjects) {
		topMenu.GetComponent<Image>().color = menuColors [theMenuColor];
		categoryLabel.GetComponent<Text> ().text = theText;

		for (int i = 0; i < levelTogglesSprites.Length; i++) {
			if (selectedToggles [i] != null && unlockedSelectedObjects[i] != null) {
				ObjectToggles [i].transform.Find ("Image").GetComponent<Image> ().sprite = selectedToggles [i];
				ObjectToggles[i].name = unlockedSelectedObjects[i];
				ObjectToggles [i].GetComponent<Toggle>().interactable = true;
				//Debug.Log ("There is an object at toogle : "+i);
			} else {
				ObjectToggles [i].transform.Find ("Image").GetComponent<Image> ().sprite = lockSprite; 
				ObjectToggles [i].GetComponent<Toggle>().interactable = false;
				//Debug.Log("This toogle is locked : "+i);
			}
			ObjectToggles[i].GetComponent<Toggle>().isOn = false;
		}
	}

	public void lockUndoButton(bool isLocked) {
		if (isLocked) {
			undoButton.GetComponent<Toggle> ().interactable = false;
		} else {
			undoButton.GetComponent<Toggle> ().interactable = true;
		}
	}
}


