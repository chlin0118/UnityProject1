using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeInSpeed = 0.66f;
	public PlayerStatus ps;
	public GameObject canvas;
	public GameObject camera;
	// Use this for initialization
	void Start () {
		//Grab the only CanvasGroup in the scene
		fadeGroup = FindObjectOfType<CanvasGroup>();

		//Start with a white screen
		fadeGroup.alpha = 1;

		//ps.Load ();
	}
	
	// Update is called once per frame
	void Update () {
		//Fade-in
		fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
	}

	public void loadscene(){
		canvas.GetComponent<Canvas> ().enabled = true;
		canvas.transform.GetChild (14).gameObject.SetActive(true);
		camera.GetComponent<Camera> ().enabled = true;
		if (ps.firstPlay) {
			SceneManager.LoadScene ("room");
			ps.firstPlay = false;
		} else {
			string sceneName = ps.currentScene;
			SceneManager.LoadScene (sceneName);
		}
	}
}
