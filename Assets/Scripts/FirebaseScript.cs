using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;


public class FirebaseScript : MonoBehaviour {

    public PlayerStatus playerStatus;
    public InputField EmailAddress, Password;
    public InputField RegEmailAddress, RegPassword;
    public GameObject windowPanel;
    public Text windowPanelText;

    public Toggle toggle;

    public MenuScene menuScene;

    private static bool Exists;
    private FirebaseAuth auth;

    // Use this for initialization
    void Start()
    {
        if (!Exists)
        {
            Exists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        auth = FirebaseAuth.DefaultInstance;

        playerStatus.Load();

        toggle.isOn = playerStatus.isToggleOn;
        if (toggle.isOn) {
            EmailAddress.text = playerStatus.accountInput;
            Password.text = playerStatus.passwordInput;
        }
    }

    public void LoginButtonPressed() {
        auth.SignInWithEmailAndPasswordAsync(EmailAddress.text, Password.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                showWindow("登入失敗");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            if (toggle.isOn) {
                playerStatus.setAccountAndPassword(EmailAddress.text, Password.text);
            }
            playerStatus.setToggleOn(toggle.isOn);

            menuScene.loadscene();

        });
    }

    public void CreateNewUserButtonPressed() {
        auth.CreateUserWithEmailAndPasswordAsync(RegEmailAddress.text, RegPassword.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                showWindow("註冊失敗");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            showWindow("註冊成功");

        });
    }

    private void showWindow(string text) {
        windowPanel.SetActive(true);
        windowPanelText.text = text;
    }

}
