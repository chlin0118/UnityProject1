using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

	public Slider expBar;
	public Text levelText;
	public PlayerStatus playerStatus;

	public Text LV;
	public Text HP;
	public Text ATK;
	public Image im1;
	public Image im2;
	public Image im3;

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
		LV.text = "等級 : " + playerStatus.currentLevel;
		HP.text = "生命 : " + playerStatus.currentHealth;
		ATK.text = "攻擊 : " + playerStatus.currentAttack;

		if (playerStatus.gameState >= 1) {
			Color c = im1.color;
			c.a = 0f;
			im1.color = c;
		}
		if (playerStatus.gameState >= 2){
			Color c = im2.color;
			c.a = 0f;
			im2.color = c;
		}
		if (playerStatus.gameState >= 3){
			Color c = im3.color;
			c.a = 0f;
			im3.color = c;
		}

	}
}
