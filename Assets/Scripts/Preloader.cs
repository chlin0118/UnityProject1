using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preloader : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minimumLogoTime = 3.3f;
	public Text text;

	// Use this for initialization
	private void Start () {
		//Grab the only CanvasGroup in the scene
		fadeGroup = FindObjectOfType<CanvasGroup>();

		// Start with a white screen
		fadeGroup.alpha = 1;

		//preload the game


		//Get a timestamp of the completion time
		//if loadtime is super, give it a small buffer time so we can appreciate the logo
		if (Time.time < minimumLogoTime) {
			loadTime = minimumLogoTime;
		} 
		else {
			loadTime = Time.time;
		}


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Set a flag here indiciating that Firebase is ready to use by your
                // application.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }
	
	// Update is called once per frame
	private void Update () {
		//Fade in
		if(Time.time < minimumLogoTime && Time.time > 0.3f){
			fadeGroup.alpha = 1 - (Time.time-0.3f);
		}

		//Fade out
		else if(Time.time > minimumLogoTime && loadTime != 0){
			fadeGroup.alpha = Time.time - minimumLogoTime;
			if (fadeGroup.alpha >= 1) {
				Debug.Log ("Change to first scene");
				SceneManager.LoadScene("menu");
			}
		}

		text.text = string.Format (Time.time.ToString());
	}
}
