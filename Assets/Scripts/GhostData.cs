using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GhostData {

	public static GhostData current;

	public string playerName;
	public string medal;

	public List<InputData> GhostInput = new List<InputData>();
}
