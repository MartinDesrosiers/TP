using UnityEngine;
using System.Collections;

public class EnemyVisual : MonoBehaviour {

	public Color primaryColor;
	public Color secondaryColor;

	public GameObject enemySprite;
	private GameObject HeroObject;

	// Use this for initialization
	void Start () {
		HeroObject = GameObject.Find("Hero");
		enemySprite.GetComponent<SpriteRenderer>().color = primaryColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")) {
			Debug.Log ("Enemy was touched");
			//MainScript mainScript = (MainScript) HeroObject.GetComponent<MainScript> ();
			//Destroy(gameObject);
		}
	}
}
