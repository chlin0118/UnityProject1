using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	private static bool sceneControllerExists;

	private GameObject[] gameObjectArray;

	// Use this for initialization
	void Start () {
		if (!sceneControllerExists) {
			sceneControllerExists = true;
			DontDestroyOnLoad (gameObject);
			SceneManager.sceneUnloaded += OnUnloadBattleScene;
			SceneManager.activeSceneChanged += OnActiveSceneChanged;
		} else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("active:" + SceneManager.GetActiveScene().name);
	}

	//進入戰鬥場景(由playerController的OnCollisionEnter2D())
	public IEnumerator loadBattleScene(string sceneName){
		yield return null;
		AsyncOperation async = SceneManager.LoadSceneAsync (sceneName,LoadSceneMode.Additive);

		async.allowSceneActivation = false;

		while(async.progress < 0.9f){
			Debug.Log ("async.progress: " + async.progress);
		}

		GameObject[] rootGameObjectOfSpecificScene = SceneManager.GetActiveScene ().GetRootGameObjects();
		foreach(GameObject go in rootGameObjectOfSpecificScene) {
			go.SetActive (false);
		}

		//暫時隱藏(SetActive (false))原本場景的物件
		/*gameObjectArray = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject go in gameObjectArray) {
			go.SetActive (false);
		}*/

		async.allowSceneActivation = true;

		while (SceneManager.GetSceneByName (sceneName).isLoaded != true) {
			yield return null;
			Debug.Log ("load?? :  " + SceneManager.GetSceneByName (sceneName).isLoaded);
		}

		//AllowSceneActivation = true 之後，等到下個Scene.isLoaded等於true，才能SetActiveScene()
		SceneManager.SetActiveScene (SceneManager.GetSceneByName(sceneName));
		Debug.Log ("loaddddddddddd ");

		var go1 = new GameObject("objectForFindDDOL");
		DontDestroyOnLoad(go1);
		foreach(GameObject go in go1.scene.GetRootGameObjects()) {
			if (go.name != "SceneController") {
				go.SetActive (false);
			}
		}

	}

	public IEnumerator unLoadBattleScene(string sceneName){
		Debug.Log ("start Unload:");
		yield return null;

		AsyncOperation async2 = SceneManager.UnloadSceneAsync (sceneName);

		Debug.Log ("Unloaded!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ");
	}

	void OnUnloadBattleScene(Scene scene){
		Debug.LogFormat ("Scene : {0}", scene.name);
	}

	void OnActiveSceneChanged(Scene previousScene, Scene nextScene)
	{
		Debug.LogFormat("[previousScene]{0} [nextScene]{1}", previousScene.buildIndex, nextScene.buildIndex);
		Debug.Log ("active:" + SceneManager.GetActiveScene().name);

		if (previousScene.name == "QA") {
			/*GameObject[] rootGameObjectOfSpecificScene = SceneManager.GetActiveScene ().GetRootGameObjects();
			foreach(GameObject go in rootGameObjectOfSpecificScene) {
				go.SetActive (true);
			}*/

			/*var go1 = new GameObject("objectForFindDDOL");
			DontDestroyOnLoad(go1);
			foreach(GameObject go in go1.scene.GetRootGameObjects()) {
				if (go.name != "SceneController") {
					go.SetActive (true);
				}
			}*/
		}
	}


}
