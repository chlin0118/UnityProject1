using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QAUI : MonoBehaviour {

	public GameObject playerInFight;
	Animator animatorOfPlayer;
	public GameObject monsterInFight;
	Animator animatorOfMonster;
	public GameObject animImages;
	Animator animatorOfAnimImages;
	public GameObject resultPanel;
	Animator animatorOfResultPanel;
	public GameObject blockingPanel;

	PlayerStatus playerStatus;
	MonsterStatus monsterStatus;

	int gameState = 0;
	const int WaitforHitButton = 0; 
	const int YesOrNo = 1;
	const int PlayerAnimating = 2;
	const int MonsterAnimating = 3;
	const int BattleEnd = 4;
	bool waiting = true;
	bool haveHitted = false;


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

	public Text resultTitle;
	public Text resultExp;
	public Text resultLvUp;
	public Text resultGet;

	int btnNo;
	int characterBlood=100;
	int monsterBlood=50;
	int characterAtk=20;
	int monsterAtk=20;
	int monsterExp=20;
	int monsterBossID=0;

	string answer = "";

	const int PUREarithmetic = 1;//type1純算數
	const int APPLICATIONformula = 2;//type2應用題列算式
	const int APPLICATIONarithmetic = 3;//type3應用題算數
	const int AnswerCountDownTime = 300;//倒數時間

	public int quationType = 1;//設定題目type

	Problems p1;


	// Use this for initialization
	void Start () {
		animatorOfPlayer = playerInFight.GetComponent<Animator> ();
		animatorOfAnimImages = animImages.GetComponent<Animator> ();
		animatorOfMonster = monsterInFight.GetComponent<Animator> ();
		animatorOfResultPanel = resultPanel.GetComponent<Animator> ();;

		if (playerStatus != null) {
			playerInFight.transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ().sprite = playerStatus.transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ().sprite;
			playerInFight.transform.GetChild (1).gameObject.SetActive (playerStatus.transform.GetChild (1).gameObject.activeInHierarchy);
			playerInFight.transform.GetChild (2).gameObject.SetActive (playerStatus.transform.GetChild (2).gameObject.activeInHierarchy);
		}
		//characterBlood = Random.Range (50, 100);
		//monsterBlood = Random.Range (20, 50);

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

		if (animatorOfPlayer.GetCurrentAnimatorStateInfo (0).IsName ("Stay")) {
			Debug.Log ("stay");
		} else {
			Debug.Log ("not stay");
		}

		Debug.Log ("gameState: " + gameState);

		switch (gameState) {
		//等待玩家按按鈕
		case WaitforHitButton:
			if (waiting){
				blockingPanel.SetActive (false);
			} 
			if (!waiting){
				blockingPanel.SetActive (true);
				animatorOfAnimImages.Play ("Correct");
				gameState = YesOrNo;
				waiting = true;
			}
			break;

		case YesOrNo:
			if (waiting){
				if (animatorOfAnimImages.GetCurrentAnimatorStateInfo (0).IsName ("Stay")) {
					waiting = false;
				}
			} 
			if (!waiting){
				animatorOfPlayer.Play ("PlayerF01_attack");
				gameState = PlayerAnimating;
				waiting = true;
			}
			break;

		case PlayerAnimating:
			//等待玩家攻擊動畫完畢
			if (waiting){
				if(	animatorOfPlayer.GetCurrentAnimatorStateInfo (0).normalizedTime>=0.65f && !haveHitted) {
					playerHit();
					haveHitted = true;
				}
				if (animatorOfPlayer.GetCurrentAnimatorStateInfo (0).IsName ("Stay")) {
					waiting = false;
				}
			}
			//玩家攻擊動畫完畢要做的事
			if (!waiting){
				if (monsterBlood <= 0 ){
					Debug.Log ("win!");
					win ();
					gameState = BattleEnd;
					waiting = true;
				}  else {
					animatorOfMonster.Play ("Run", 0);
					animatorOfMonster.Play ("MonsterGO", 1);
					gameState = MonsterAnimating;
					waiting = true;
					haveHitted = false;
				}
			}
			break;

		case MonsterAnimating:
			//等待怪物攻擊動畫完畢
			if (waiting) {
				if(	animatorOfMonster.GetCurrentAnimatorStateInfo (1).normalizedTime>=0.7f && !haveHitted) {
					monsterHit();
					haveHitted = true;
				}
				if (animatorOfMonster.GetCurrentAnimatorStateInfo (0).IsName ("Stay")) {
					waiting = false;
				}
			}
			//怪物攻擊動畫完畢要做的事
			if (!waiting) {
				if (characterBlood <= 0) {
					lose ();
					Debug.Log ("lose!");
					gameState = BattleEnd;
					waiting = true;
				} else {
					startProblem (quationType);
					gameState = WaitforHitButton;
					waiting = true;
					haveHitted = false;
				}
			}
			break;

		case BattleEnd:
			if (waiting){
				if (animatorOfResultPanel.GetCurrentAnimatorStateInfo (0).IsName ("Stay")) {
					waiting = false;
				}
			} 
			if (!waiting){
				back ();
			}
			break;
		} 
	}

	public void startProblem(int type){

		answerArea.gameObject.SetActive (false);

		switch (type) {
		case PUREarithmetic:
			Type1Problem ();
			break;
		case APPLICATIONformula:
			Type2Problem ();
			break;
		case APPLICATIONarithmetic:
			Type3Problem ();
			break;
		} 

	}

	void Type1Problem(){//純數運算
		timer_f = 0;
		answerNo = 0;
		promptArea.text = "";

		p1 = new Problems(PUREarithmetic);
		roundPrompt.text ="(答案取至小數點後第二位並四捨五入)";
		questionArea.fontSize = 79;
		questionArea.text = p1.getFinalProblem ();
		answerArea.text = "答案：" + p1.getAnswer();
		answer = p1.getAnswer();
		string wrongAnswer = p1.getWrongAnswer();

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
			Text3.text = (getFloat(wrongAnswer,0.00f)*0.1).ToString("0.00");
			Text4.text = p1.getWrongAnswer ();
			btn1.tag = "answer";
			btn2.tag = "1answer0.1";
			btn3.tag = "1wrongAnswer0.1";
			btn4.tag = "wrongAnswer";

		} else if (btnNo%4 == 1) {
			Text1.text = (getFloat(wrongAnswer,0.00f)*10).ToString("0.00");
			Text2.text = p1.getAnswer ();
			Text3.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text4.text = p1.getWrongAnswer ();
			btn1.tag = "1wrongAnswer10";
			btn2.tag = "answer";
			btn3.tag = "1answer10";
			btn4.tag = "wrongAnswer";

		} else if (btnNo%4 == 2) {
			Text1.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text2.text = p1.getWrongAnswer ();
			Text3.text = p1.getAnswer ();
			Text4.text = (getFloat(wrongAnswer,0.00f)*0.1).ToString("0.00");
			btn1.tag = "1answer0.1";
			btn2.tag = "wrongAnswer";
			btn3.tag = "answer";
			btn4.tag = "1wrongAnswer0.1";
		} else if (btnNo%4 == 3) {
			Text1.text = (getFloat(wrongAnswer,0.00f)*10).ToString("0.00");
			Text2.text = p1.getWrongAnswer ();
			Text3.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text4.text = p1.getAnswer ();
			btn1.tag = "1wrongAnswer10";
			btn2.tag = "wrongAnswer";
			btn4.tag = "1answer10";
			btn4.tag = "answer";

		}

	}

	void Type2Problem(){//列式
		timer_f = 0;
		answerNo = 0;
		promptArea.text = "";

		p1 = new Problems(APPLICATIONformula);
		questionArea.fontSize = 60;
		questionArea.text = p1.getFinalProblem();
		answerArea.text = "答案：" +p1.getAnswer();
		answer = p1.getAnswer();

		btn1.tag = "options";
		btn1.interactable = true;
		btn3.tag = "options";
		btn3.interactable = true;

		btn1.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000,150);
		btn3.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000,150);
		btn2.gameObject.SetActive(false);
		btn4.gameObject.SetActive(false);
		btnNo = Random.Range (0,100);

		if (btnNo%2== 0) {
			Text1.text = answer;
			Text3.text = p1.getWrongAnswer();
			btn1.tag = "answer";
			btn3.tag = "wrongAnswer";

		} else if (btnNo%2== 1) {
			Text1.text = p1.getWrongAnswer();
			Text3.text = answer;
			btn1.tag = "wrongAnswer";
			btn3.tag = "answer";
		} 

	}
	void Type3Problem(){//應用題
		timer_f = 0;
		answerNo = 0;
		promptArea.text = "";
		roundPrompt.text ="(答案取至小數點後第二位並四捨五入)";

		p1 = new Problems(APPLICATIONarithmetic);
		questionArea.fontSize = 60;
		questionArea.text = p1.getFinalProblem();
		answerArea.text = "答案：" +p1.getAnswer();
		answer = p1.getAnswer();
		string wrongAnswer = p1.getWrongAnswer ();

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
			Text2.text = (getFloat(wrongAnswer,0.00f)*10).ToString("0.00");
			Text3.text = p1.getWrongAnswer ();
			Text4.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			btn1.tag = "answer";
			btn2.tag = "3wrongAnswer10";
			btn3.tag = "wrongAnswer";
			btn4.tag = "3answer10";

		} else if (btnNo%4 == 1) {
			Text1.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text2.text = p1.getAnswer ();
			Text3.text = p1.getWrongAnswer ();
			Text4.text = (getFloat(wrongAnswer,0.00f)*0.1).ToString("0.00");
			btn1.tag = "3answer0.1";
			btn2.tag = "answer";
			btn3.tag = "wrongAnswer";
			btn4.tag = "3wrongAnswer0.1";

		} else if (btnNo%4 == 2) {
			Text1.text = (getFloat(answer,0.00f)*10).ToString("0.00");
			Text2.text = (getFloat(wrongAnswer,0.00f)*10).ToString("0.00");
			Text3.text = p1.getAnswer ();
			Text4.text = p1.getWrongAnswer ();
			btn1.tag = "3answer10";
			btn2.tag = "3wrongAnswer10";
			btn3.tag = "answer";
			btn4.tag = "wrongAnswer";
		} else if (btnNo%4 == 3) {
			Text1.text = (getFloat(answer,0.00f)*0.1).ToString("0.00");
			Text2.text = p1.getWrongAnswer ();
			Text3.text = (getFloat(wrongAnswer,0.00f)*0.1).ToString("0.00");
			Text4.text = p1.getAnswer ();
			btn1.tag = "3answer0.1";
			btn2.tag = "wrongAnswer";
			btn3.tag = "3wrongAnswer0.1";
			btn4.tag = "answer";

		}


	}

	public void BtnAnswerOnclick(Button btn){

		if (btn.CompareTag ("answer")) {
			/*monsterBlood = monsterBlood - Random.Range (5, 15);
			monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
			monsterSlider.value = monsterBlood;
			characterBlood = characterBlood - Random.Range (1, 5);
			charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
			characterSlider.value = characterBlood;*/

			answerArea.gameObject.SetActive (true);

			waiting = false;

		} else {

			btn.interactable = false;

			if (btn.CompareTag ("1answer0.1")) {
				promptArea.text = "提示:\n"+p1.prompt ();
			} else if (btn.CompareTag ("1answer10")) {
				promptArea.text = "提示:\n"+p1.prompt ();
			} else if (btn.CompareTag ("1wrongAnswer0.1")) {
				promptArea.text = "提示:\n"+p1.promptWrong ();
			} else if (btn.CompareTag ("1wrongAnswer10")) {
				promptArea.text = "提示:\n"+p1.promptWrong ();
			} else if (btn.CompareTag ("3answer10")) {
				promptArea.text = "提示:\n"+p1.prompt ();
			} else if (btn.CompareTag ("3answer0.1")) {
				promptArea.text = "提示:\n"+p1.prompt ();
			} else if (btn.CompareTag ("3wrongAnswer10")) {
				promptArea.text = "提示:\n"+p1.promptWrong ();
			} else if (btn.CompareTag ("3wrongAnswer0.1")) {
				promptArea.text = "提示:\n"+p1.promptWrong ();
			} else if (btn.CompareTag ("wrongAnswer")) {
				promptArea.text = "提示:\n"+p1.promptWrong ();
			}



		}
	}

	void playerHit(){
		monsterBlood = monsterBlood - characterAtk;
		//monsterBlood = monsterBlood - 2000;
		if (monsterBlood<=0){
			monsterBlood = 0;
		}
		monBlood.text =  monsterBlood+"/"+ monsterSlider.maxValue;
		monsterSlider.value = monsterBlood;
	}

	void monsterHit(){
		characterBlood = characterBlood - monsterAtk;
		if (characterBlood<=0){
			characterBlood = 0;
		}
		charaBlood.text = characterBlood+"/"+ characterSlider.maxValue;
		characterSlider.value = characterBlood;
	}

	void win(){
		animatorOfMonster.Play ("MonsterDie");

		resultTitle.text = "戰鬥勝利";
		resultExp.text = "獲得 " + monsterExp + " 經驗值";
		resultLvUp.text = "";
		resultGet.text = "";
		if (playerStatus.currentExp + monsterExp >= playerStatus.toLevelUp [playerStatus.currentLevel]) {
			resultLvUp.text = "等級提升了!!";
		}

		if (monsterBossID > 0) {
			playerStatus.addGameState ();
			if (monsterBossID <= 3) {
				resultGet.text = "~獲得了神器~";
			}
		}

		resultPanel.SetActive (true);
		animatorOfResultPanel.Play("Result");
		playerStatus.AddExperience(monsterExp);
	}

	void lose(){
		resultTitle.text = "戰鬥失敗";
		resultExp.text = "獲得 0 經驗值";
		resultLvUp.text = "";
		resultGet.text = "";
		resultPanel.SetActive (true);
		animatorOfResultPanel.Play("Result");

		if (monsterBossID > 0) {
			monsterStatus.setHasBeenBattled (false);
			Vector3 v3 = monsterStatus.transform.position - playerStatus.transform.position;
			playerStatus.setPosition (playerStatus.transform.position - v3.normalized);
		}
	}

	void back(){
		SceneController sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

		if (sceneController != null) {
			StartCoroutine (sceneController.unLoadBattleScene ("QA"));
		}
	}

	public void setType(int type){
		quationType = type;
	}

	public void setPlayerAndMonsterStatus(PlayerStatus ps, MonsterStatus ms){
		characterBlood = ps.currentHealth;
		characterAtk = ps.currentAttack;
		monsterBlood = ms.health;
		monsterAtk = ms.damage;
		monsterExp = ms.expToGive;
		monsterBossID = ms.bossID;

		monsterInFight.GetComponent<Animator> ().runtimeAnimatorController = ms.animatorInFight as RuntimeAnimatorController;
		playerStatus = ps;
		monsterStatus = ms;
	}

	private float getFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
}
