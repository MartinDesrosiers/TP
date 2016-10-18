using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelObjectives {

	public static LevelObjectives current;

	public float goldTime;
	public float silverTime;
	public float bronzeTime;

	public string rule1;
	public string rule2;
	public string rule3;

	public LevelObjectives(){
	
	}
}
