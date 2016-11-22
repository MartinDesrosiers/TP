using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SettingsData {

	public static SettingsData current;

	public string language;
	public int musicVolume;
	public int soundVolume;

	public int loadLevelId;

	// Use this for initialization
	public SettingsData() {
	}
}
