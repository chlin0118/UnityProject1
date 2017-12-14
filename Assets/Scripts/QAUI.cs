using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QAUI : MonoBehaviour {

	float timer_f = 0f;
	int timer = 0;
	bool isAnswering = false;

	int answerNo;

	public Text timerText;
	public Text charaBlood;
	public Text monBlood;
	public Text questionArea;
	public Text answerArea;
	public Text roundPrompt;
	public Button btn1;
	public Button btn2;
	public Button btn3;
	public Button btn4;
	public Text Text1;
	public Text Text2;
	public Text Text3;
	public Text Text4;

	public Slider characterSlider;
	public Slider monsterSlider;


	int btnNo;
	int characterBlood;
	int monsterBlood;
	string answer = "";

	const int PUREarithmetic = 1;//type1純算數
	const int APPLICATIONformula = 2;//type2應用題列算式
	const int APPLICATIONarithmetic = 3;//type3應用題算數
	const int AnswerCountDownTime = 30;//倒數時間


	// Use this for initialization
	void Start () {
		characterBlood = Random.Range (50, 100);
		monsterBlood = Random.Range (20, 50);

		charaBlood.text = "血量：" + characterBlood;
		monBlood.text = "血量：" + monsterBlood;

		characterSlider.maxValue = characterBlood;
		characterSlider.value = characterBlood;
		monsterSlider.maxValue = monsterBlood;
		monsterSlider.value = monsterBlood;

		Type1Problem ();


	}
	
	// Update is called once per frame
	void Update () {
		if (isAnswering) {
			timer_f += Time.deltaTime;
			timer = (int)timer_f;

			timerText.text = "時間：" + (AnswerCountDownTime - timer) + "s";
		}

		
	}

	void Type1Problem(){
		isAnswering = true;
		timer_f = 0;
		answerNo = 0;

		Problems p1 = new Problems(PUREarithmetic);
		roundPrompt.text ="(答案取至小數點後第二位並四捨五入)";
		questionArea.text = p1.getFinalProblem ();
		answerArea.text = "答案：" + p1.getAnswer();
		answer = p1.getAnswer();
		btn1.tag = "options";
		btn1.interactable = true;
		btn2.tag = "options";
		btn2.interactable = true;
		btn3.tag = "options";
		btn3.interactable = true;
		btn4.tag = "options";
		btn4.interactable = true;

		btnNo = Random.Range (0,100);

		if (btnNo%4 == 0) {
			Text1.text = p1.getAnswer ();
			Text2.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text3.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text4.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			btn1.tag = "answer";

		} else if (btnNo%4 == 1) {
			Text1.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text2.text = p1.getAnswer ();
			Text3.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text4.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			btn2.tag = "answer";

		} else if (btnNo%4 == 2) {
			Text1.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text2.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text3.text = p1.getAnswer ();
			Text4.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			btn3.tag = "answer";
		} else if (btnNo%4 == 3) {
			Text1.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text2.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text3.text = Random.Range (0.1f, 100.0f).ToString("0.00");
			Text4.text = p1.getAnswer ();
			btn4.tag = "answer";
		}

	}

	public void CheckAnswerBtn1(){

		if (btn1.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text = "血量：" + monsterBlood;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = "血量：" + characterBlood;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				Type1Problem ();
			}
			
			//else-結束答題

		} else {

			btn1.interactable = false;
			answerNo++;
			if (answerNo == 1) {
			}else if(answerNo == 2){
			}else if(answerNo == 3){
			}

		}
	}
	public void CheckAnswerBtn2(){
		
		if (btn2.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text = "血量：" + monsterBlood;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = "血量：" + characterBlood;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				Type1Problem ();
			}

			//else-結束答題
		} else {

			btn2.interactable = false;
			answerNo++;
			if (answerNo == 1) {
			}else if(answerNo == 2){
			}else if(answerNo == 3){
			}
		}

	}
	public void CheckAnswerBtn3(){
		if (btn3.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text = "血量：" + monsterBlood;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = "血量：" + characterBlood;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				Type1Problem ();
			}

			//else-結束答題
		} else {
			btn3.interactable = false;
			answerNo++;
			if (answerNo == 1) {
			}else if(answerNo == 2){
			}else if(answerNo == 3){
			}
		}
			
	}
	public void CheckAnswerBtn4(){
		if (btn4.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text = "血量：" + monsterBlood;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = "血量：" + characterBlood;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				Type1Problem ();
			}

			//else-結束答題
		} else {

			btn4.interactable = false;
			answerNo++;
			if (answerNo == 1) {
			}else if(answerNo == 2){
			}else if(answerNo == 3){
			}
		}
			
	}
}
