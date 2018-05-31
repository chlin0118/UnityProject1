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
    public string userId;

    public int totalPlayedTime;
    public int correctBy1Time;
    public int correctBy2Times;
    public int correctBy3Times;
    public int correctBy4Times;

    private float secondsCount = 0;
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

        secondsCount += Time.deltaTime;

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

    public void setAnswerTimes(int[] array){
        correctBy1Time += array[0];
        correctBy2Times += array[1];
        correctBy3Times += array[2];
        correctBy4Times += array[3];
    }

	public void Save(){
		currentScene = SceneManager.GetActiveScene ().name;
        totalPlayedTime += (int)secondsCount;
        secondsCount = 0;
        SaveLoadManager.SavePlayer (this);
        FirebaseScript.writeToDB(userId, gameState, totalPlayedTime, correctBy1Time, correctBy2Times, correctBy3Times, correctBy4Times);
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
            userId = data.userId;

            totalPlayedTime = data.totalPlayedTime;
            correctBy1Time = data.correctBy1Time;
            correctBy2Times = data.correctBy2Times;
            correctBy3Times = data.correctBy3Times;
            correctBy4Times = data.correctBy4Times;

            checkState ();
		} else {
			transform.position = new Vector3 (0, -1, 0);
		}
	}
}
