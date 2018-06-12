using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherScene : MonoBehaviour {

    public GameObject panelPrefab;
    public GameObject contentInScrollView;

    public GameObject panelPrefab2;
    public GameObject contentInScrollViewInType1;
    public GameObject contentInScrollViewInType2;
    public GameObject contentInScrollViewInType3;

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

        GameObject studentInfoPanel2 = (GameObject)Instantiate(panelPrefab2);
        studentInfoPanel2.transform.SetParent(contentInScrollViewInType1.transform);//Setting panel parent in type 1

        studentInfoPanel2.transform.GetChild(0).GetComponent<Text>().text = user.username;
        studentInfoPanel2.transform.GetChild(1).GetComponent<Text>().text = user.number.ToString();
        studentInfoPanel2.transform.GetChild(2).GetComponent<Text>().text = user.correctBy1TimeInType1.ToString();
        studentInfoPanel2.transform.GetChild(3).GetComponent<Text>().text = user.correctBy2TimesInType1.ToString();
        studentInfoPanel2.transform.GetChild(4).GetComponent<Text>().text = user.correctBy3TimesInType1.ToString();
        studentInfoPanel2.transform.GetChild(5).GetComponent<Text>().text = user.correctBy4TimesInType1.ToString();
        studentInfoPanel2.transform.GetChild(6).GetComponent<Text>().text = user.totalProblemsInType1.ToString();
        studentInfoPanel2.transform.GetChild(7).GetComponent<Text>().text = user.correctRateInType1.ToString("P0");

        GameObject studentInfoPanel3 = (GameObject)Instantiate(panelPrefab2);
        studentInfoPanel3.transform.SetParent(contentInScrollViewInType2.transform);//Setting panel parent in type 2

        studentInfoPanel3.transform.GetChild(0).GetComponent<Text>().text = user.username;
        studentInfoPanel3.transform.GetChild(1).GetComponent<Text>().text = user.number.ToString();
        studentInfoPanel3.transform.GetChild(2).GetComponent<Text>().text = user.correctBy1TimeInType2.ToString();
        studentInfoPanel3.transform.GetChild(3).GetComponent<Text>().text = user.correctBy2TimesInType2.ToString();
        studentInfoPanel3.transform.GetChild(4).GetComponent<Text>().text = "";
        studentInfoPanel3.transform.GetChild(5).GetComponent<Text>().text = "";
        studentInfoPanel3.transform.GetChild(6).GetComponent<Text>().text = user.totalProblemsInType2.ToString();
        studentInfoPanel3.transform.GetChild(7).GetComponent<Text>().text = user.correctRateInType2.ToString("P0");

        GameObject studentInfoPanel4 = (GameObject)Instantiate(panelPrefab2);
        studentInfoPanel4.transform.SetParent(contentInScrollViewInType3.transform);//Setting panel parent in type 3

        studentInfoPanel4.transform.GetChild(0).GetComponent<Text>().text = user.username;
        studentInfoPanel4.transform.GetChild(1).GetComponent<Text>().text = user.number.ToString();
        studentInfoPanel4.transform.GetChild(2).GetComponent<Text>().text = user.correctBy1TimeInType3.ToString();
        studentInfoPanel4.transform.GetChild(3).GetComponent<Text>().text = user.correctBy2TimesInType3.ToString();
        studentInfoPanel4.transform.GetChild(4).GetComponent<Text>().text = user.correctBy3TimesInType3.ToString();
        studentInfoPanel4.transform.GetChild(5).GetComponent<Text>().text = user.correctBy4TimesInType3.ToString();
        studentInfoPanel4.transform.GetChild(6).GetComponent<Text>().text = user.totalProblemsInType3.ToString();
        studentInfoPanel4.transform.GetChild(7).GetComponent<Text>().text = user.correctRateInType3.ToString("P0");
    }
}
