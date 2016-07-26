using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayValue : MonoBehaviour {

	public GameObject gameScript;
	public Text coinsDisplay;

	void OnGUI() {
		MainScript mainScript = gameScript.GetComponent<MainScript> ();
		coinsDisplay.text= "Coins: " + mainScript.coins.ToString();
	}
}
