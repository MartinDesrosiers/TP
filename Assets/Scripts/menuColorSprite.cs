using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuColorSprite : MonoBehaviour {

	public GameObject HeroObject;
	public GameObject editorImporter;

	public Sprite[] menuColors;
	public GameObject[] ObjectToggles;
	public GameObject undoButton;

	public Sprite[] levelTogglesSprites;
	public Sprite[] enemyTogglesSprites;
	public Sprite[] collectibleTogglesSprites;
	public Sprite[] trapTogglesSprites;
	public Sprite[] groundTogglesSprites;
	public Sprite[] editTogglesSprites;

	public Sprite lockSprite;

	// Use this for initialization
	void Start () {
		CategoryChanged ("groundTGL");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void CategoryChanged(string categorySelected){

		LevelEditor levelEditor = editorImporter.GetComponent<LevelEditor> ();

		Debug.Log ("Edit Mode : None Selected");
		levelEditor.objectSelected = "None";

		MainScript mainScript = (MainScript)HeroObject.GetComponent<MainScript> ();

		int theMenuColor;

		switch (categorySelected) {
		case "levelTGL":
			theMenuColor = 0;
			updateTopMenu (theMenuColor, levelTogglesSprites, mainScript.unlockedLevelObjects);
			break;
		case "enemyTGL":
			theMenuColor = 1;
			updateTopMenu (theMenuColor, enemyTogglesSprites, mainScript.unlockedEnemyObjects);
			break;
		case "collectibleTGL":
			theMenuColor = 2;
			updateTopMenu (theMenuColor, collectibleTogglesSprites, mainScript.unlockedCollectibleObjects);
			break;
		case "trapTGL":
			theMenuColor = 3;
			updateTopMenu (theMenuColor, trapTogglesSprites, mainScript.unlockedTrapObjects);
			break;
		case "groundTGL":
			theMenuColor = 4;
			updateTopMenu (theMenuColor, groundTogglesSprites, mainScript.unlockedGroundObjects);
			break;
		case "editTGL":
			theMenuColor = 5;
			updateTopMenu (theMenuColor, editTogglesSprites, mainScript.unlockedEditObjects);
			break;
		}
	}

	void updateTopMenu(int theMenuColor, Sprite[] selectedToggles, string[] unlockedSelectedObjects) {
		
		GetComponent<Image> ().sprite = menuColors [theMenuColor];

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


