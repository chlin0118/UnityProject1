using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QAController : MonoBehaviour {
	
	float timer_f = 0f;
	int timer = 0;

	public Text timerText;

	const int AnswerCountDownTime = 30;//倒數時間


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		/*//計時
		timer_f += Time.deltaTime;
		timer = (int)timer_f;

		timerText.text = "時間：" + (AnswerCountDownTime-timer) + "s";*/
		
	}

}
