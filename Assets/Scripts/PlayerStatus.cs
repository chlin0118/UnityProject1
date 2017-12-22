using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {

	public int currentLevel;

	public int currentExp;

	public int[] toLevelUp;

	public int currentHealth;
	public int [] Healths;

	public int currentAttack;
	public int [] Attacks;

	public int gameState;

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
}
