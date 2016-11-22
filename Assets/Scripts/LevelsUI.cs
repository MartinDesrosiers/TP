using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsUI : MonoBehaviour {

	public GameObject node;
	private List<GameObject> nodes = new List<GameObject>();
	public GameObject gridPattern;

	public GameObject CreatePanel;

	Vector3 MouseXY = new Vector3();

	private int mapRow;
	private int mapColumn;

	// Use this for initialization
	void Start () {
		SaveAndLoadData.LoadLevel ();	
		loadNodes();
		//drawTheGrid();
	}
	
	// Update is called once per frame
	void Update () {
		chooseSaveSlot();
	}

	void drawTheGrid (){
		for (int x = 0; x < 100; x+=2) {
			for (int y = 0; y < 100; y+=2) {
				Vector3 gridPosition = new Vector3 ();
				gridPosition.x = x-51.9f;
				gridPosition.y = y-52f;
				gridPosition.z = 0;
				Instantiate (gridPattern, gridPosition, transform.rotation);
			}
		}
	}

	private void chooseSaveSlot(){
		bool userClicked = detectInputPosition ();
		if (userClicked) {
			mapRow = (int)Mathf.Abs (MouseXY.y);
			mapColumn = (int)Mathf.Abs (MouseXY.x);

			openCreatePanel();

			userClicked = false;
		}
	}

	private bool detectInputPosition () {
		if (Input.GetButton ("Fire1")) {

			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
				return false;
			}

			MouseXY = Input.mousePosition;
			MouseXY.x = Mathf.Floor (MouseXY.x);
			MouseXY.y = Mathf.Floor (MouseXY.y);
			MouseXY.z = 0;
			return true;
		} else {
			return false;
		}
	}

	void loadNodes(){
		foreach (LevelData level in SaveAndLoadData.savedLevels) {
			addNode (level.mapPositionX, level.mapPositionY, level.name, level.offlineID);
		}
	}

	public void createNewLevel(){
		LevelData.current = new LevelData ();

		LevelData.current.mapPositionX = mapColumn;
		LevelData.current.mapPositionY = mapRow;
		LevelData.current.offlineID = SaveAndLoadData.savedLevels.Count;
		LevelData.current.name = "New Level";
		LevelData.current.newLevel = true;

		LevelData newLevel = new LevelData ();
		newLevel = LevelData.current;
		SaveAndLoadData.savedLevels.Add(newLevel);
		SaveAndLoadData.SaveLevel();

		addNode(mapColumn,mapRow,LevelData.current.name,LevelData.current.offlineID);
		loadLevel(LevelData.current.offlineID);
	}

	void addNode(int x, int y, string name, int ID){
		GameObject newNode = null;
		Vector3 spawnPosition = new Vector3();

		spawnPosition.x = x;
		spawnPosition.y = y;
		spawnPosition.z = 0;

		Debug.Log(spawnPosition.x+" "+spawnPosition.y);

		GameObject addedNode = (GameObject)Instantiate (node);
		addedNode.transform.SetParent(this.transform);
		addedNode.transform.position = spawnPosition;

		addedNode.transform.Find ("Text").GetComponent<Text> ().text = name;
		addedNode.name = ID+"";
		Debug.Log(name+ID);

		newNode = addedNode;

		nodes.Add(newNode);
	}

	public void openCreatePanel(){
		CreatePanel.SetActive (true);
	}

	public void closeCreatePanel(){
		CreatePanel.SetActive (false);
	}

	public void nodeClicked(GameObject clickedNode){
		int levelID = System.Int32.Parse(clickedNode.name);
		loadLevel(levelID);
	}

	void loadLevel(int levelID){
		SettingsData.current = new SettingsData ();
		SettingsData.current.loadLevelId = levelID;
		SaveAndLoadData.SaveSettings ();
		Debug.Log("Loading level with ID :"+levelID);
		SceneManager.LoadScene("LevelEditor");
	}
}
