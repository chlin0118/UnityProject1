using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherScene : MonoBehaviour {

    public  GameObject panelPrefab;
    public  GameObject contentInScrollView;

    // Use this for initialization
    void Start () {
        GameObject gameObject = GameObject.Find("Firebase");
        if (gameObject)
        {
            gameObject.GetComponent<FirebaseScript>().retrievingFromDB(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createStudentInfo(User user) {
        Debug.Log("afdafd:asdfadfsddsdsds45555555555");
        GameObject studentInfoPanel = (GameObject)Instantiate(panelPrefab);

        studentInfoPanel.transform.SetParent(contentInScrollView.transform);//Setting panel parent
        studentInfoPanel.transform.GetChild(0).GetComponent<Text>().text = user.username;
        studentInfoPanel.transform.GetChild(1).GetComponent<Text>().text = user.number.ToString();
        int h = user.totalPlayedTime/3600;
        int m = (user.totalPlayedTime % 3600) / 60;
        if (h > 0)
        {
            studentInfoPanel.transform.GetChild(2).GetComponent<Text>().text = h + "時\n" + m + "分";
        }
        else
        {
            studentInfoPanel.transform.GetChild(2).GetComponent<Text>().text =  m + "分";
        }
        studentInfoPanel.transform.GetChild(3).GetComponent<Text>().text = user.correctBy1Time.ToString();
        studentInfoPanel.transform.GetChild(4).GetComponent<Text>().text = user.totalProblems.ToString();
        studentInfoPanel.transform.GetChild(5).GetComponent<Text>().text = user.correctRate.ToString("P0");
        studentInfoPanel.transform.GetChild(6).GetComponent<Text>().text = user.playerStage.ToString();

        Debug.Log("afdafd:asdfadfsddsdsds");
    }
}
