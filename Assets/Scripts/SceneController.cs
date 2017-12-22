using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	private static bool sceneControllerExists;

	private GameObject[] rootGameObjectOfSpecificScene;

	public PlayerController player;
	public PlayerStatus playerStatus;
	private MonsterStatus monsterStatus;

	// Use this for initialization
	void Start () {
		if (!sceneControllerExists) {
			sceneControllerExists = true;
			DontDestroyOnLoad (gameObject);
			SceneManager.sceneUnloaded += OnUnloadBattleScene;
			SceneManager.activeSceneChanged += OnActiveSceneChanged;
			SceneManager.sceneLoaded += OnLoadBattleScene;
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

		monsterStatus = player.currentEnemy.GetComponent<MonsterStatus> ();
		monsterStatus.setHasBeenBattled (true);

		//暫時隱藏(SetActive (false))原本場景的物件
		rootGameObjectOfSpecificScene = SceneManager.GetActiveScene ().GetRootGameObjects();
		foreach(GameObject go in rootGameObjectOfSpecificScene) {
			go.SetActive (false);
		}

		//暫時隱藏Dont Destroy On Load 下的部分物件
		foreach(GameObject go in gameObject.scene.GetRootGameObjects()) {
			if (go.name == "Player") {
				go.GetComponent<SpriteRenderer>().enabled = false;
				//player 子物件隱藏
				foreach(SpriteRenderer  sr in go.GetComponentsInChildren<SpriteRenderer>()) {
					sr.enabled = false;
				}
			}
			else if (go.name == "Canvas") {
				go.SetActive (false);
			}
		}

		async.allowSceneActivation = true;

		while (SceneManager.GetSceneByName (sceneName).isLoaded != true) {
			yield return null;
			Debug.Log ("load?? :  " + SceneManager.GetSceneByName (sceneName).isLoaded);
		}

		//AllowSceneActivation = true 之後，等到下個Scene.isLoaded等於true，才能SetActiveScene()
		SceneManager.SetActiveScene (SceneManager.GetSceneByName(sceneName));
		Debug.Log ("loaddddddddddd ");
	

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

	void OnLoadBattleScene(Scene scene, LoadSceneMode mode){
		Debug.Log ("OnLoadBattleScene: " + scene.name);
		if (scene.name == "QA") {
			QAUI qaui = FindObjectOfType<QAUI> ();
			qaui.setType(monsterStatus.type);
			qaui.setPlayerAndMonsterStatus (playerStatus, monsterStatus);
		}
	}

	void OnActiveSceneChanged(Scene previousScene, Scene nextScene)
	{
		Debug.LogFormat ("[previousScene]{0} [nextScene]{1}", previousScene.buildIndex, nextScene.buildIndex);
		Debug.Log ("active:" + SceneManager.GetActiveScene ().name);

		if (nextScene.name == "QA") {
			
		}

		if (previousScene.name == "QA") {
			
			foreach (GameObject go in rootGameObjectOfSpecificScene) {
				if (go.tag == "Enemy") {
					MonsterStatus ms = go.GetComponent<MonsterStatus> ();
					if (ms.getHasBeenBattled ()) {
						continue;
					}
				}
				if (go != null) {
					go.SetActive (true);
				}
			}
			foreach(GameObject go in gameObject.scene.GetRootGameObjects()) {
				if (go.name == "Player") {
					go.GetComponent<SpriteRenderer>().enabled = true;
					//player 子物件隱藏
					foreach(SpriteRenderer  sr in go.GetComponentsInChildren<SpriteRenderer>()) {
						sr.enabled = true;
					}
				}
				else if (go.name == "Canvas") {
					go.SetActive (true);
				}
			}

			playerStatus.Save ();
		}

		/*if (previousScene.name == "menu") {
			canvas.SetActive (true);
			canvas.GetComponent<Canvas> ().enabled = true;

			Debug.Log ("canvas.GetComponent<Canvas> ().enabled = true;");
		}*/

	}
}
