using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(waitLoadingLevel());
	}
	
	// Update is called once per frame
	IEnumerator waitLoadingLevel() {
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MainMenu");
	}
}
