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

	public Text FocusText;

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

		if(playerStatus.gameState == 0){
			FocusText.text = "去上方的森林裡，打敗守護森林的魔王，蒐集第一件神器";
		}
		else if(playerStatus.gameState == 1){
			FocusText.text = "得到了第一件神器-寶劍!接著去右邊的沙漠裡，打敗守護沙漠的魔王，蒐集第二件神器";
		}
		else if (playerStatus.gameState == 2){
			FocusText.text = "得到了第二件神器-神盾!繼續去下方的岩石區裡，打敗守護岩石區的魔王，蒐集第三件神器";
		}
		else if (playerStatus.gameState == 3){
			FocusText.text = "已蒐集齊三件神器!!!去左方的雪地，打敗最後魔王吧。";
		}


	}
}
