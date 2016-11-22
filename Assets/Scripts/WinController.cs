using UnityEngine;
using System.Collections;

public class WinController : MonoBehaviour {

	private GameObject HeroObject;

	// Use this for initialization
	void Start () {
		HeroObject = GameObject.Find("Hero");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			HeroController heroController = (HeroController) HeroObject.GetComponent<HeroController> ();
			heroController.levelCompleted ();
		}
	}
}