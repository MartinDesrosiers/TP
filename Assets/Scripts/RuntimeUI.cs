using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RuntimeUI : MonoBehaviour {

	public GameObject heroImporter;
	public Text coinsDisplay;

	public GameObject timer;

	private string chrono;

	private int minutes;
	private float seconds;

	private float time = 0f;

	void Start(){
		//HeroController characterControls = heroImporter.GetComponent<HeroController>();
		//characterControls.playable = true;
	}

	void Update() {
		setChrono ();
	}

	void setChrono(){
		time += Time.deltaTime;
		if (seconds < 60) {
			seconds = Mathf.Floor (time);
		}
		else if (seconds >= 60) {
			time = 0;
			seconds = 0;
			minutes += 1;
		}

		if (seconds < 10) {
			chrono = minutes + ":0" + seconds;
		} else {
			chrono = minutes + ":" + seconds;
		}
		timer.GetComponent<Text>().text = chrono;
	}

	void OnGUI() {
		MainScript mainScript = heroImporter.GetComponent<MainScript> ();
		coinsDisplay.text= "Coins: " + mainScript.coins.ToString();
	}
}
