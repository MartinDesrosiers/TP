using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dressingRoomUI : MonoBehaviour {

	public GameObject heroImporter;
	public GameObject bottomMenu;
	public GameObject categoryBTN;

	public Color[] menuColors;

	public GameObject[] ObjectToggles;
	public Sprite lockSprite;
	public GameObject ethiquette;
	public GameObject buyBTN;

	public GameObject money;
	public GameObject name;
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
		CategoryChanged("hatTGL");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CategoryChanged(string categorySelected){

		MainScript mainScript = (MainScript)heroImporter.GetComponent<MainScript> ();

		int theMenuColor;

		switch (categorySelected) {
		case "hatTGL":
			theMenuColor = 0;
			updateTopMenu (theMenuColor, hatObjects, mainScript.unlockedLevelObjects);
			break;
		case "shirtTGL":
			theMenuColor = 1;
			updateTopMenu (theMenuColor, shirtObjects, mainScript.unlockedEnemyObjects);
			break;
		case "pantsTGL":
			theMenuColor = 2;
			updateTopMenu (theMenuColor, pantsObjects, mainScript.unlockedCollectibleObjects);
			break;
		case "shoesTGL":
			theMenuColor = 3;
			updateTopMenu (theMenuColor, shoesObjects, mainScript.unlockedTrapObjects);
			break;
		case "weaponsTGL":
			theMenuColor = 4;
			updateTopMenu (theMenuColor, weaponObjects, mainScript.unlockedGroundObjects);
			break;
		case "runnerTGL":
			theMenuColor = 5;
			updateTopMenu (theMenuColor, characterObjects, mainScript.unlockedEditObjects);
			break;
		}
	}

	void updateTopMenu(int theMenuColor, GameObject[] selectedToggles, string[] unlockedSelectedObjects) {
		bottomMenu.GetComponent<Image>().color = menuColors [theMenuColor];

		for (int i = 0; i < ObjectToggles.Length; i++) {
			if (selectedToggles [i] != null && unlockedSelectedObjects[i] != null) {

				Sprite selectedSprite = selectedToggles [i].GetComponent<SpriteRenderer> ().sprite;

				ObjectToggles [i].transform.Find ("Image").GetComponent<Image> ().sprite = selectedSprite;
				ObjectToggles[i].name = unlockedSelectedObjects[i];
				ObjectToggles [i].GetComponent<Toggle>().interactable = true;
				Debug.Log ("There is an object at toogle : "+i);
			} else {
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
