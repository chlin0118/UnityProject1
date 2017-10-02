using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

	public Slider expBar;
	public Text levelText;
	public PlayerStatus playerStatus;

	private static bool UIExists;

	// Use this for initialization
	void Start () {
		if (!UIExists) {
			UIExists = true;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		expBar.maxValue = playerStatus.toLevelUp [playerStatus.currentLevel];
		expBar.value = playerStatus.currentExp;
		levelText.text = playerStatus.currentLevel.ToString();
	}
}
