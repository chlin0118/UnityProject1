using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour {

	private PlayerController thePlayer;
	private float secondsCount;
	private float perSecond = 0;
	private bool isUnloading;

	void Awake(){
		Debug.Log ("test: awake");
	}

	// Use this for initialization
	void Start () {
		//SceneManager.SetActiveScene (SceneManager.GetSceneByName("test"));
		Debug.Log ("test start");

		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!isUnloading) {
			secondsCount += Time.deltaTime;	
			if (secondsCount >= perSecond+1) {
				//Debug.Log (Time.time);
				perSecond++;
			}

			if (perSecond == 3) {
				Debug.Log ("enemy = " + thePlayer.currentEnemy.name);
				isUnloading = true;
				StartCoroutine ("Unload");
			}
		}
	}

	IEnumerator Unload(){
		Debug.Log ("start Unload:");
		yield return null;

		//test if victory or defeat
		int testnumber =  Random.Range(0,2) ;
		if (testnumber == 1) {
			Destroy (thePlayer.currentEnemy);
		}

		yield return null;
		Debug.Log ("Unloaddddddddddddddddddddd ");
		SceneManager.UnloadSceneAsync ("test");
	}

	void OnDestroy() {
		print("Script was destroyed");
	}

	void OnDisable() {
		print("script was removed");
	}
}
