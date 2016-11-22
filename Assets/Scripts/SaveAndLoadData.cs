using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveAndLoadData {

	//List of saved levels from the level editor
	public static List<LevelData> savedLevels = new List<LevelData>();
	//List of user's settings including scenes transitions
	public static SettingsData savedSettings = new SettingsData();

	//Not DRY because paths and saved/load objects change everytime
	public static void SaveLevel(){
		//Create a new instance of level data to avoid overwritting
		LevelData newLevel = new LevelData();
		//Store the current level data in this new instance
		newLevel = LevelData.current;
		Debug.Log("Saving the current level");
		//Insert to replace or add the instance in the attributed save slot (attributed on levels map)
		SaveAndLoadData.savedLevels.Insert(newLevel.offlineID,newLevel);
		//Serialize the updated list of saved levels and save it in : .../user/appdata/locallow/MartinDesrosiersJV/Toprunners/savedlevels.gd
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedLevels.gd");
		bf.Serialize(file, SaveAndLoadData.savedLevels);
		file.Close();
	}

	//Same thing but load levels and overide the current saved levels list
	public static void LoadLevel(){
		Debug.Log ("Loading levels data");
		if (File.Exists (Application.persistentDataPath + "/savedLevels.gd")) {
			Debug.Log("Succesful loading");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedLevels.gd", FileMode.Open);
			SaveAndLoadData.savedLevels = (List<LevelData>)bf.Deserialize (file);
			file.Close();
		}
	}

	public static void SaveSettings(){
		Debug.Log("Saving settings)");
		savedSettings = SettingsData.current;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedSettings.gd");
		bf.Serialize(file, SaveAndLoadData.savedSettings);
		file.Close();
	}

	public static void LoadSettings(){
		if (File.Exists (Application.persistentDataPath + "/savedSettings.gd")) {
			Debug.Log("Loading settings");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedSettings.gd", FileMode.Open);
			SaveAndLoadData.savedSettings = (SettingsData)bf.Deserialize (file);
			file.Close();
		}
	}
}
