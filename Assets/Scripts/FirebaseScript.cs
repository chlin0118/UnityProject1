using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;


public class FirebaseScript : MonoBehaviour {

    public PlayerStatus playerStatus;
    public InputField EmailAddress, Password;
    public InputField RegEmailAddress, RegPassword;
    public InputField RegName, RegNumber;
    public GameObject windowPanel;
    public Text windowPanelText;

    public Toggle toggle;

    public MenuScene menuScene;

    private static bool Exists;
    private FirebaseAuth auth;
    private static DatabaseReference mDatabaseRef;
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

        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity1-703b1.firebaseio.com/");
        // Get the root reference location of the database.
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

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

            playerStatus.userId = newUser.UserId;
            //Debug.Log("user: " + playerStatus.userName + " login successfully");

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

            writeNewUser(newUser.UserId, RegName.text, RegEmailAddress.text, int.Parse(RegNumber.text));
            
            //playerStatus.userName = RegName.text;
            SaveLoadManager.SavePlayer(playerStatus);
            showWindow("註冊成功");
            
        });
    }

    private void showWindow(string text) {
        windowPanel.SetActive(true);
        windowPanelText.text = text;
    }

    private void writeNewUser(string userId, string name, string email, int number)
    {
        User user = new User(name, email, number);
        string json = JsonUtility.ToJson(user);

        mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public static void writeToDB(string userId, int state, int playedTime, int c1, int c2, int c3, int c4) {
        mDatabaseRef.Child("users").Child(userId).Child("playerStage").SetValueAsync(state);
        mDatabaseRef.Child("users").Child(userId).Child("totalPlayedTime").SetValueAsync(playedTime);
        mDatabaseRef.Child("users").Child(userId).Child("correctBy1Time").SetValueAsync(c1);
        mDatabaseRef.Child("users").Child(userId).Child("correctBy2Times").SetValueAsync(c2);
        mDatabaseRef.Child("users").Child(userId).Child("correctBy3Times").SetValueAsync(c3);
        mDatabaseRef.Child("users").Child(userId).Child("correctBy4Times").SetValueAsync(c4);

    }

}
