using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// public class GameManager : Singleton<GameManager>
// {
//     public Transform player;
//     public Transform focus;
// 	[SerializeField] Key resetKey = Key.R;

// 	[Header("Area Loading")]
// 	[SerializeField] Area[] areas;
// 	[SerializeField] Area currentArea;
// 	public Savepoint CurrentSavepoint;
//     [SerializeField] List<Area> loadedAreas = new();

// 	Random.State mainRandomState;


// 	Area GetArea(string name) {
// 		foreach (Area area in areas) if (area.name == name) return area;
// 		return null;
// 	}

// 	Scene GetScene(string name) {
// 		return SceneManager.GetSceneByName(name);
// 	}

//     void Start() {
// 		QualitySettings.vSyncCount = 0;
// 		Application.targetFrameRate = 60;
// 		mainRandomState = Random.state;

// 		if (Application.isEditor) {
// 			for (int i = 0; i < SceneManager.sceneCount; i++) {
// 				Scene loadedScene = SceneManager.GetSceneAt(i);
// 				if (loadedScene.name.Contains("Area ")) {
// 					loadedAreas.Add(GetArea(loadedScene.name));
// 				}
// 			}
// 			if (loadedAreas.Count > 0) return;
// 		}

// 		BeginNewGame();
// 		// StartCoroutine(SetCurrentArea(areas[0]));
// 		CurrentSavepoint = areas[0].savepoints[0];
// 		StartCoroutine(LoadSavepointRoutine(CurrentSavepoint));
// 	}


//     void BeginNewGame() {
// 		Random.state = mainRandomState;
// 		int seed = Random.Range(0, int.MaxValue) ^ (int)Time.unscaledTime;
// 		mainRandomState = Random.state;
// 		Random.InitState(seed);
// 	}

// 	// #if UNITY_EDITOR
// 	// void Update() {
// 	// 	if (Keyboard.current[resetKey].wasPressedThisFrame) {
// 	// 		ReloadLevel();
// 	// 	}
// 	// }
// 	// #endif

//     // Level Loading Stuff

// 	public IEnumerator SetCurrentAreaRoutine(Area area) {
// 		if (currentArea == area) yield break;
// 		currentArea = area;

// 		yield return LoadAreaRoutine(area);
// 		if (!loadedAreas.Contains(area)) loadedAreas.Add(area);

// 		SceneManager.SetActiveScene(SceneManager.GetSceneByName(area.name));

// 		// unload NOT neighbors
// 		// foreach (Area loadedArea in loadedAreas) {
// 		// 	if (!area.Neighbors.Contains(loadedArea) && currentArea!=loadedArea) yield return UnloadAreaRoutine(loadedArea);
// 		// }
// 		for (int i=0; i<loadedAreas.Count; i++) {
// 			Area loadedArea = loadedAreas[i];
// 			if (!area.Neighbors.Contains(loadedArea) && currentArea!=loadedArea) {
// 				yield return UnloadAreaRoutine(loadedArea);
// 				loadedAreas.RemoveAt(i);
// 				i--;
// 			}
// 		}

// 		// load neighbors
// 		foreach (Area neighbor in area.Neighbors) {
// 			yield return LoadAreaRoutine(neighbor);
// 			if (!loadedAreas.Contains(neighbor)) loadedAreas.Add(neighbor);
// 		}

// 		// bro JUST loaded in bro
// 		// if (CurrentSavepoint == null) {
// 		// 	CurrentSavepoint = currentArea.savepoints[0];
// 		// 	player.transform.position = CurrentSavepoint.Position;
// 		// }
// 	}

// 	IEnumerator LoadAreaRoutine(Area area) {
// 		if (GetScene(area.name).isLoaded) yield break;

//         AsyncOperation op = SceneManager.LoadSceneAsync(area.name, LoadSceneMode.Additive);
//         while (!op.isDone) yield return null;
// 	}

// 	IEnumerator UnloadAreaRoutine(Area area) {
// 		if (!GetScene(area.name).isLoaded) yield break;
// 		if (currentArea == area) currentArea = null;

//         AsyncOperation op = SceneManager.UnloadSceneAsync(area.name, UnloadSceneOptions.None);
//         while (!op.isDone) yield return null;
// 	}

// 	public IEnumerator LoadSavepointRoutine(Savepoint savepoint) {
// 		// UI.FadeToBlack();

// 		// unload current area, also reloads current area if the current area is the area that the savepoint's area is the area in
// 		foreach (Area loadedArea in loadedAreas) {
// 			yield return UnloadAreaRoutine(loadedArea);
// 		}
// 		loadedAreas.Clear();

// 		yield return SetCurrentAreaRoutine(savepoint.Area);
// 		player.transform.position = savepoint.Position;

// 		// UI.FadeFromBlack();
// 	}

// }
