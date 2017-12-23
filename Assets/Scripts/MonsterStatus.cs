using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour {

	public RuntimeAnimatorController animatorInFight;

	private bool hasBeenBattled = false;

	public int health;
	public int damage;
	public int expToGive;

	public int type;
	public int bossID = 0;

	// Use this for initialization
	void Start () {
		if(bossID!=0){
			if (FindObjectOfType<PlayerStatus> ().gameState >= bossID) {
				gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setHasBeenBattled(bool TF){
		hasBeenBattled = TF;
	}
	public bool getHasBeenBattled(){
		return hasBeenBattled;
	}
}
