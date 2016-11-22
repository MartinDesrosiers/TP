using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public GameObject[] buttonText;

	void Start(){
		updateLanguage();
	}

	void updateLanguage (){
		
		Localization.current = new Localization ();
		//List of buttons to translate
		string[] textIDs = new string[6] {"locRun","locOnline","locLevels","locScores","locGym","locStore"};

		//The text needs to be vertical. So each caracters are separated with \n. 
		int z = 0;
		foreach(string txt in textIDs){
			//Instantiate, capitalize and translate IDs
			string newTXT = Localization.current.Translate(txt).ToUpper();
			//Store in int to avoid infinite loop(-1 one to avoid an extra line at the end)
			int newTXTLenght = newTXT.Length;
			for(int i = 1; i <= newTXTLenght-1; i+=2) {
				newTXT = newTXT.Insert (i,"\n");
				newTXTLenght += 1;
			}
			buttonText[z].GetComponent<Text>().text = newTXT;
			z++;
		}
	}

	//When the user clicks on a Main menu button, it returns a string that triggers one of these behaviours.
	//The strings are assigned in the inspector.
	public void OnClick (string selectedButton){
		Debug.Log (selectedButton+" was clicked");
		switch (selectedButton) {
		case "Run":
			SceneManager.LoadScene("RunMap");
			break;
		case "Online":
			SceneManager.LoadScene("Online");
			break;
		case "Levels":
			SceneManager.LoadScene("Levels");
			break;
		case "Scores":
			SceneManager.LoadScene("Scores");
			break;
		case "Gym":
			SceneManager.LoadScene("Gym");
			break;
		case "Store":
			SceneManager.LoadScene("Store");
			break;
		case "Facebook":
			Application.OpenURL ("https://www.facebook.com/");
			break;
		case "Twitch":
			Application.OpenURL ("https://www.twitch.tv/");
			break;
		case "Youtube":
			Application.OpenURL ("https://gaming.youtube.com/");
			break;
		}
	}
}
