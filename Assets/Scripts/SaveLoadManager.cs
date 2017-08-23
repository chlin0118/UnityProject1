using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SavePlayer(PlayerController player){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Create);

		PlayerData data = new PlayerData (player);

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

	public PlayerData(PlayerController player){
		playerPosX = player.transform.position.x;
		playerPosY = player.transform.position.y;
		playerPosZ = player.transform.position.z;
	}
}