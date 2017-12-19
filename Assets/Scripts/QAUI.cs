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
	public Text promptArea;
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
	public Slider timerSlider;



	int btnNo;
	int characterBlood;
	int monsterBlood;
	string answer = "";

	const int PUREarithmetic = 1;//type1純算數
	const int APPLICATIONformula = 2;//type2應用題列算式
	const int APPLICATIONarithmetic = 3;//type3應用題算數
	const int AnswerCountDownTime = 60;//倒數時間

	public int quationType = 1;//設定題目type


	// Use this for initialization
	void Start () {
		characterBlood = Random.Range (50, 100);
		monsterBlood = Random.Range (20, 50);

		charaBlood.text = characterBlood+"/"+characterBlood;
		monBlood.text = monsterBlood+"/" + monsterBlood;

		characterSlider.maxValue = characterBlood;
		characterSlider.value = characterBlood;
		monsterSlider.maxValue = monsterBlood;
		monsterSlider.value = monsterBlood;

		timerSlider.maxValue = AnswerCountDownTime;
		timerSlider.value = AnswerCountDownTime;

		startProblem (quationType);


	}
	
	// Update is called once per frame
	void Update () {

		timer_f += Time.deltaTime;
		timer = (int)timer_f;

		timerSlider.value = AnswerCountDownTime - timer;
		timerText.text =  (AnswerCountDownTime - timer)+"/"+ timerSlider.maxValue+"s";

		if (monsterBlood <= 0) {
			Debug.Log ("win:");
			win ();
		}

	}

	public void startProblem(int type){
		switch (type) {
		case PUREarithmetic:
			Type1Problem ();
			break;
		case APPLICATIONformula:
			Type2Problem ();
			break;
		case APPLICATIONarithmetic:
			
			break;
		} 

	}

	void Type1Problem(){//純數運算
		timer_f = 0;
		answerNo = 0;

		Problems p1 = new Problems(PUREarithmetic);
		roundPrompt.text ="(答案取至小數點後第二位並四捨五入)";
		questionArea.fontSize = 79;
		questionArea.text = p1.getFinalProblem ();
		answerArea.text = "答案：" + p1.getAnswer();
		answer = p1.getAnswer();

		btn1.GetComponent<RectTransform> ().sizeDelta = new Vector2 (480,150);
		btn3.GetComponent<RectTransform> ().sizeDelta = new Vector2 (480,150);
		btn2.gameObject.SetActive(true);
		btn4.gameObject.SetActive(true);

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
			Text2.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text3.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text4.text = (getFloat(answer,0.00f)+1).ToString("0.00");
			btn1.tag = "answer";

		} else if (btnNo%4 == 1) {
			Text1.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text2.text = p1.getAnswer ();
			Text3.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text4.text = (getFloat(answer,0.00f)+2).ToString("0.00");
			btn2.tag = "answer";

		} else if (btnNo%4 == 2) {
			Text1.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text2.text = (getFloat(answer,0.00f)+1).ToString("0.00");
			Text3.text = p1.getAnswer ();
			Text4.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			btn3.tag = "answer";
		} else if (btnNo%4 == 3) {
			Text1.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text2.text = (getFloat(answer,0.00f)+2).ToString("0.00");
			Text3.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text4.text = p1.getAnswer ();
			btn4.tag = "answer";
		}

	}

	void Type2Problem(){//列式
		timer_f = 0;
		answerNo = 0;

		Problems p1 = new Problems(APPLICATIONformula);
		//roundPrompt.text ="(答案取至小數點後第二位並四捨五入)";
		questionArea.fontSize = 60;
		questionArea.text = "老師把 14 公斤重的糖果平分給 200 個小朋友，每個小朋友可得幾公斤重的糖果？";
		answerArea.text = "答案：14÷200" ;
		answer = "14÷200";
		btn1.tag = "options";
		btn1.interactable = true;
		btn3.tag = "options";
		btn3.interactable = true;

		btn1.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000,150);
		btn3.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000,150);
		btn2.gameObject.SetActive(false);
		btn4.gameObject.SetActive(false);
		btnNo = Random.Range (0,100);

		if (btnNo%2 == 0) {
			Text1.text = answer;
			Text3.text = "200÷14";
			btn1.tag = "answer";

		} else if (btnNo%2 == 1) {
			Text1.text = "1.05÷15";
			Text3.text = answer;
			btn3.tag = "answer";

		} 

	}

	public void CheckAnswerBtn1(){

		if (btn1.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
			characterSlider.value = characterBlood;

			if (monsterBlood > 0) {
				startProblem (quationType);
			}
			
			//else-結束答題

		} else {

			btn1.interactable = false;
			answerNo++;
			if (answerNo == 1) {
				//promptArea.text = ;
			}else if(answerNo == 2){
			}else if(answerNo == 3){
			}

		}
	}
	public void CheckAnswerBtn2(){
		
		if (btn2.CompareTag ("answer")) {
			monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				startProblem (quationType);
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
			monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				startProblem (quationType);
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
			monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
			characterSlider.value = characterBlood;
			if (monsterBlood > 0) {
				startProblem (quationType);
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

	void win(){
		SceneController sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

		if (sceneController != null) {
			StartCoroutine (sceneController.unLoadBattleScene ("QA"));
		}

	}

	public void setType(int type){
		quationType = type;
	}

	private float getFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
}
