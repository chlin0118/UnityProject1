using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {

	public Sprite sword;

	public int currentLevel;
	public int currentExp;
	public int[] toLevelUp;

	public int currentHealth;
	public int [] Healths;

	public int currentAttack;
	public int [] Attacks;

	public int gameState = 0;
	public bool firstPlay = true;
	public string currentScene = "room";

    public string accountInput;
    public string passwordInput;
    public bool isToggleOn;
    public string userName;

    private float secondsCount;
	private float perSecond = 1;

	// Use this for initialization
	void Start () {
		currentLevel = 1;
		currentHealth = Healths[currentLevel];
		currentAttack = Attacks[currentLevel];
	}
	
	// Update is called once per frame
	void Update () {
		if (currentExp >= toLevelUp [currentLevel]) {
			currentLevel++;
			currentHealth = Healths[currentLevel];
			currentAttack = Attacks[currentLevel];
		}

	}
		
	public void AddExperience(int experienceToAdd){
		currentExp += experienceToAdd;
	}

	public void addGameState(){
		gameState+=1;
		checkState ();
	}

	public void checkState(){
		if (gameState >= 1) {
			transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ().sprite = sword;
		}
		if (gameState >= 2){
			transform.GetChild (1).gameObject.SetActive (true);
		}
		if (gameState >= 3){
			transform.GetChild (2).gameObject.SetActive (true);
		}
	}

	public void setPosition(Vector3 v3){
		transform.position = v3;
	}

    public void setAccountAndPassword(string ac, string pw) {
        accountInput = ac;
        passwordInput = pw;
    }

    public void setToggleOn(bool isOn) {
        isToggleOn = isOn;
    }

	public void Save(){
		currentScene = SceneManager.GetActiveScene ().name;
		SaveLoadManager.SavePlayer (this);
	}

	public void Load(){
		PlayerData data = SaveLoadManager.LoadPlayer ();
		if (data != null) {
			transform.position = new Vector3 (data.playerPosX, data.playerPosY, data.playerPosZ);

			currentLevel = data.currentLevel;
			currentExp = data.currentExp;
			currentHealth = data.currentHealth;
			currentAttack = data.currentAttack;
			gameState = data.gameState;
			firstPlay = data.firstPlay;
			currentScene = data.currentScene;
            accountInput = data.accountInput;
            passwordInput = data.passwordInput;
            isToggleOn = data.isToggleOn;
            userName = data.userName;

            checkState ();
		} else {
			transform.position = new Vector3 (0, -1, 0);
		}
	}
}
