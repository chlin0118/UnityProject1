using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {

	public int currentLevel;

	public int currentExp;

	public int[] toLevelUp;
	private float secondsCount;
	private float perSecond = 1;

	// Use this for initialization
	void Start () {
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentExp >= toLevelUp [currentLevel]) {
			currentLevel++;
		}
			
		Debug.Log (Time.time);
		secondsCount += Time.deltaTime;	
		if (secondsCount >= perSecond) {
			currentExp += 5;
			perSecond++;
		}

		/*if(perSecond % 5 == 0){
			perSecond = 6;
			//SceneManager.LoadScene ("menu", LoadSceneMode.Additive);
			SceneManager.LoadScene ("main");
			//Time.timeScale = 0;
		}*/

	}

	public void AddExperience(int experienceToAdd){
		currentExp += experienceToAdd;
	}
}
