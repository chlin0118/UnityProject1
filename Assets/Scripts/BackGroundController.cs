using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundController : MonoBehaviour {

	public Sprite grass;
	public Sprite desert;
	public Sprite rock;
	public Sprite ice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		SceneManager.activeSceneChanged += changeBackground;
	}

	void OnDisable()
	{
		SceneManager.activeSceneChanged -= changeBackground;
	}

	void changeBackground(Scene previousScene, Scene nextScene){
		Debug.Log ("previousScene  " + previousScene.name);
		switch (previousScene.name) {
		case "battlegroundUp":
			gameObject.GetComponent<SpriteRenderer> ().sprite = grass;
			break;

		case "battlegroundRight":
			gameObject.GetComponent<SpriteRenderer> ().sprite = desert;
			break;

		case "battlegroundLeft":
			gameObject.GetComponent<SpriteRenderer> ().sprite = ice;
			break;

		case "battlegroundDown":
			gameObject.GetComponent<SpriteRenderer> ().sprite = rock;
			break;

		default:
			gameObject.GetComponent<SpriteRenderer> ().sprite = grass;
			break;
		}
	}
}
