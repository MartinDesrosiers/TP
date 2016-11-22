using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DressingRoomUI : MonoBehaviour {

	public GameObject heroImporter;
	public GameObject bottomMenu;
	public GameObject categoryBTN;

	public Color[] menuColors;

	public GameObject[] ObjectToggles;
	public Sprite lockSprite;
	public GameObject ethiquette;
	public GameObject buyBTN;

	public GameObject money;
	public GameObject heroName;
	public GameObject level;

	public GameObject[] health;
	public GameObject maxSpeed;

	public GameObject[] stamina;
	public GameObject[] strenght;
	public GameObject[] acceleration;
	public GameObject[] jump;

	public GameObject[] hatObjects;
	public GameObject[] shirtObjects;
	public GameObject[] pantsObjects;
	public GameObject[] shoesObjects;
	public GameObject[] weaponObjects;
	public GameObject[] characterObjects;

	// Use this for initialization
	void Start () {
		ethiquette.SetActive (false);
		buyBTN.SetActive (false);
		CategoryChanged("Hats");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Function called when clicking on a buttons that changes category.
	//Onclick, returns a string assigned in inspector
	public void CategoryChanged(string categorySelected){

		MainScript mainScript = (MainScript)heroImporter.GetComponent<MainScript> ();
		int theMenuColor;

		//Check which is the new category and calls the update
		switch (categorySelected) {
		case "Hats":
			theMenuColor = 0;
			updateBottomMenu (categorySelected,theMenuColor, hatObjects, mainScript.unlockedLevelObjects);
			break;
		case "Shirts":
			theMenuColor = 1;
			updateBottomMenu (categorySelected,theMenuColor, shirtObjects, mainScript.unlockedEnemyObjects);
			break;
		case "Pants":
			theMenuColor = 2;
			updateBottomMenu (categorySelected,theMenuColor, pantsObjects, mainScript.unlockedCollectibleObjects);
			break;
		case "Shoes":
			theMenuColor = 3;
			updateBottomMenu (categorySelected,theMenuColor, shoesObjects, mainScript.unlockedTrapObjects);
			break;
		case "Weapons":
			theMenuColor = 4;
			updateBottomMenu (categorySelected,theMenuColor, weaponObjects, mainScript.unlockedGroundObjects);
			break;
		case "Runner":
			theMenuColor = 5;
			updateBottomMenu (categorySelected,theMenuColor, characterObjects, mainScript.unlockedEditObjects);
			break;
		}
	}

	void updateBottomMenu(string categorySelected, int theMenuColor, GameObject[] selectedToggles, string[] unlockedSelectedObjects) {

		//Update the bottom menu colors
		bottomMenu.GetComponent<Image>().color = menuColors [theMenuColor];
		//Update the category button colors and text
		categoryBTN.GetComponent<Image> ().color = menuColors [theMenuColor];
		categoryBTN.transform.Find("Text").GetComponent<Text>().text = categorySelected;

		//For each object toggles of the bottom menu, show the attributed object and money, if unlocked.
		for (int i = 0; i < ObjectToggles.Length; i++) {
			if (selectedToggles [i] != null && unlockedSelectedObjects[i] != null) { //If the object exists and is unlocked
				//Get the sprite of the objects
				Sprite selectedSprite = selectedToggles [i].GetComponent<SpriteRenderer> ().sprite;
				//Assign it to the toggles, with name and money.
				ObjectToggles [i].transform.Find ("Image").GetComponent<Image> ().sprite = selectedSprite;
				ObjectToggles[i].name = unlockedSelectedObjects[i];
				ObjectToggles [i].GetComponent<Toggle>().interactable = true;
				Debug.Log ("There is an object at toogle : "+i);
			} else { //Locked objects
				ObjectToggles [i].transform.Find ("Image").GetComponent<Image> ().sprite = lockSprite;
				ObjectToggles [i].transform.Find ("Label").GetComponent<Text> ().text = ""; 
				ObjectToggles [i].GetComponent<Toggle>().interactable = false;
				Debug.Log("This toogle is locked : "+i);
			}
			ObjectToggles[i].GetComponent<Toggle>().isOn = false;
		}
	}

	public void objectSelected(string selectedObject){
		switch (selectedObject) {
		case "":
		break;
		}
	}
}
