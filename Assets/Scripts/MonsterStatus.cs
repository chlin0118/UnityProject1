using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour {

	private bool hasBeenBattled = false;

	public int health;
	public int damage;
	public int expToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setHasBeenBattled(){
		hasBeenBattled = true;
	}
	public bool getHasBeenBattled(){
		return hasBeenBattled;
	}
}
