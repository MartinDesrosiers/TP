using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	private GameObject HeroObject;

	// Use this for initialization
	void Start () {
		HeroObject = GameObject.Find("Hero");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			HeroController heroController = (HeroController) HeroObject.GetComponent<HeroController> ();
			heroController.levelCompleted ();
		}
	}
}