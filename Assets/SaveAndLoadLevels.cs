using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoadLevels : MonoBehaviour {

	public static List<levelCode> savedLevels = new List<levelCode>();

	public static void SaveLevel(){
		Debug.Log("Saving a level(at least try!)");
		savedLevels.Add(levelCode.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedLevels.gd");
		bf.Serialize(file, SaveAndLoadLevels.savedLevels);
		file.Close();
	}

	public static void LoadLevel(){
		Debug.Log ("Load level called.");
		if (File.Exists (Application.persistentDataPath + "/savedLevels.gd")) {
			Debug.Log("Loading a level");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedLevels.gd", FileMode.Open);
			SaveAndLoadLevels.savedLevels = (List<levelCode>)bf.Deserialize (file);
			file.Close();
		}
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
