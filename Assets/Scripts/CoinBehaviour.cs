﻿using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	private GameObject HeroObject;

	void Start () {
		HeroObject = GameObject.Find("Hero");
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")) {
			Debug.Log ("One coin was collected");
			MainScript mainScript = (MainScript) HeroObject.GetComponent<MainScript> ();
			mainScript.coinCollected();
			Destroy(gameObject);
		}
	}
}
