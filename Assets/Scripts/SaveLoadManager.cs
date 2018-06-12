using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SavePlayer(PlayerStatus player){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Create);

		PlayerData data = new PlayerData (player);
		Debug.Log ("save: ");
		bf.Serialize (stream, data);
		stream.Close();
	}

	public static PlayerData LoadPlayer(){
		if (File.Exists (Application.persistentDataPath + "/player.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Open);

			PlayerData data = bf.Deserialize (stream) as PlayerData;

			Debug.Log ("data.playerPosX: "+ data.playerPosX);
			Debug.Log ("data.playerPosY: "+ data.playerPosY);
			Debug.Log ("currentScene: "+ data.currentScene);
			Debug.Log ("firstPlay: "+ data.firstPlay);

			if (data.currentScene.Length<=1) {
				Debug.Log ("null: "+ null);
				return null;
			}

			stream.Close ();
			return data;
		} else {
			Debug.LogError ("File does not exist");
			return null;
		}
	}
}

[Serializable]
public class PlayerData{
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;

	public int currentLevel;
	public int currentExp;
	public int currentHealth;
	public int currentAttack;
	public int gameState;
	public bool firstPlay;
	public string currentScene;

    public string accountInput;
    public string passwordInput;
    public bool isToggleOn;
    public string userId;

    public int totalPlayedTime;
    public int correctBy1Time;
    public int correctBy2Times;
    public int correctBy3Times;
    public int correctBy4Times;

    public int correctBy1TimeInType1;
    public int correctBy2TimesInType1;
    public int correctBy3TimesInType1;
    public int correctBy4TimesInType1;

    public int correctBy1TimeInType2;
    public int correctBy2TimesInType2;

    public int correctBy1TimeInType3;
    public int correctBy2TimesInType3;
    public int correctBy3TimesInType3;
    public int correctBy4TimesInType3;

    public PlayerData(PlayerStatus player){
		playerPosX = player.transform.position.x;
		playerPosY = player.transform.position.y;
		playerPosZ = player.transform.position.z;

		currentLevel = player.currentLevel;
		currentExp = player.currentExp;
		currentHealth = player.currentHealth;
		currentAttack = player.currentAttack;
		gameState = player.gameState;
		firstPlay = player.firstPlay;
		currentScene = player.currentScene;
        accountInput = player.accountInput;
        passwordInput = player.passwordInput;
        isToggleOn = player.isToggleOn;
        userId = player.userId;

        totalPlayedTime = player.totalPlayedTime;
        correctBy1Time = player.correctBy1Time;
        correctBy2Times = player.correctBy2Times;
        correctBy3Times = player.correctBy3Times;
        correctBy4Times = player.correctBy4Times;

        correctBy1TimeInType1 = player.correctBy1TimeInType1;
        correctBy2TimesInType1 = player.correctBy2TimesInType1;
        correctBy3TimesInType1 = player.correctBy3TimesInType1;
        correctBy4TimesInType1 = player.correctBy4TimesInType1;

        correctBy1TimeInType2 = player.correctBy1TimeInType2;
        correctBy2TimesInType2 = player.correctBy2TimesInType2;

        correctBy1TimeInType3 = player.correctBy1TimeInType3;
        correctBy2TimesInType3 = player.correctBy2TimesInType3;
        correctBy3TimesInType3 = player.correctBy3TimesInType3;
        correctBy4TimesInType3 = player.correctBy4TimesInType3;
    }
}