using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine; 

public class QAProblemGenerator : MonoBehaviour {

	//int[] typeCutLine = new int[] {1,3};//type1*1題(<1),type2*2題(>=1&&<3),type3*1題(>=3)

	/*
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		//finalAnswerArea.text = "def";
		//Problems p2 = new Problems(APPLICATIONarithmetic,question[0],args[0]);
	}*/
}

public class Problems{

	const int PUREarithmetic = 1;//type1純算數
	const int APPLICATIONformula = 2;//type2應用題列式
	const int APPLICATIONarithmetic = 3;//type3應用題算數

	string[][] formulaOnlyQ = new string[][]{//列式4選項乘變大、除變小-tips乘法和除法的意義、大數除小數
		new string[] {"如果","公斤的麵粉賣","元，買一公斤要付多少元？"},
		new string[] {"如果一包糖果重","公斤，","公斤重的糖果是幾包？"}};

	string[][] bigSamllQ = new string[][]{//大數除小數-tips思考除數與被除數的意義
		new string[] {"有","條相同的繩子共長","公尺，請問一條繩子長_________公尺"},
		new string[]{"如果將一條","公尺長的繩子等分成","份，請問其中的一份繩子長多少公尺？"}};		

	string[][] easyQ = new string[][]{//計算錯誤-tips驗算
		new string[] {"將","公升的可樂分裝在","個大杯子，每個大杯子裝________公升的可樂"},
		new string[] {"把","公斤重的糖果平分給","個小朋友，每個小朋友可得_________公斤重的糖果"},
		new string[] {"每個容器可以裝", "公升的果汁，", "公升的果汁可以裝滿幾個容器？" },
		new string[] {"一桶","公升的果汁重","公斤，1公升果汁重幾公斤？"},
		new string[] {"一包白米的重量是","公斤，一包糯米的重量是","公斤，一包白米的重量是一包糯米的幾倍？"},
		new string[] {"如果","條的繩子是","公尺，那一條繩子長多少公尺？"},
		new string[] {"媽媽買1瓶豆漿，小琪喝了","瓶的容量是","毫升，1瓶豆漿的容量是幾毫升？"},
		new string[] {"1平方公尺的花圃需要","公升的農藥，","公升的農藥可以供給幾平方公尺的花圃？"},	
		new string[] {"汽水工廠","分鐘製造出","公升的汽水，平均每分鐘製造出多少公升的汽水？"},
		new string[] {"1歐元等於39.5203元新台幣，李伯伯想到歐洲旅遊，","元新台幣可以換到多少歐元？"}};	

	string[][] quotientPlusOneQ = new string[][]{//題目理解-tips還有剩下的要+1
		new string[] {"一桶果汁有","公升，分裝在每瓶容量","公升的瓶子裡，可以裝成幾瓶？"},
		new string[] {"一捆麻繩長","公尺，每","公尺剪成一段，可以剪成幾段？"},
		new string[] {"一面牆的面積是","平方公尺，一桶油漆可以粉刷","平方公尺，則這面牆要幾桶油漆才可以粉刷完？"},
		new string[] {"一輛貨車一次可運","公斤的飼料，今天有","公斤的飼料，則至少要幾輛貨車才能運完？"}};

	int type = 0;//題目類型
	int argc = 0;
	double[] args = {};
	string[] statements = {};
	string answer = "";//答案
	string wrongAnswer = "";
	float floatAnswer = 0.0f;//選項1
	string finalProblem = "456";//題目

	float dividend = 0;//被除數
	float divisor = 0;//除數
	int randomNo = 0;

	public Problems(int type){
		this.type = type;
	}
		
	public string getFinalProblem(){

	
		if (type == 1) {//小數除法算數

			randomNo = Random.Range (1, 5);//決定要出哪種題目

			dividend = 0;//被除數

			divisor = 0;//除數
			switch(randomNo){
				
				case 1:  //整數除整數=小數
					dividend = Random.Range (1, 200);
					divisor = Random.Range (2, 200);
					if (dividend % divisor == 0)
						break;
					finalProblem = dividend + "÷" + divisor + "= ?";
						break;
	
				case 2:  //2位小數除1位整數
					dividend = Random.Range (0.1f, 200.0f);
					divisor = Random.Range (2, 9);
					finalProblem = dividend.ToString("0.00") + "÷" + divisor + "= ?";
					break;		

				case 3:  //2位小數除2位整數
					dividend = Random.Range (0.1f, 200.0f);
					divisor = Random.Range (10,99);
					finalProblem = dividend.ToString("0.00") + "÷" + divisor + "= ?";
					break;

				case 4:  //整數除2位小數
					dividend = Random.Range (100,200);
					divisor = Random.Range (0.1f, 200.0f);
					finalProblem = dividend + "÷" + divisor.ToString("0.00") + "= ?";
					break;

				default:  //小數除小數
					dividend = Random.Range (0.1f, 200.0f);
					divisor = Random.Range (0.1f, 200.0f);
					finalProblem = dividend.ToString("0.000") + "÷" + divisor.ToString("0.00") + "= ?";
					break;

			}

			answer = (dividend / divisor).ToString ("0.00");
			wrongAnswer = (divisor / dividend).ToString ("0.00");

		
		} else {
			finalProblem = "";
			string[] args = new string[]{ "0", "0" };

			randomNo = Random.Range (1, 18);//決定要從哪個array出題目

			if ((randomNo < 11)) {//簡單題easyQ

				int j = Random.Range (0, 9);

				switch (j) {

					case 0:
						args [0] = Random.Range (10, 99).ToString ("0");
						args [1] = Random.Range (2, 9).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [0], 00f) / getFloat (args [1], 0f);
						answer = args [0] + "÷" + args [1];
						wrongAnswer = args [1] + "÷" + args [0];
						break;
					case 1:
						args [0] = Random.Range (10, 99).ToString ("0");
						args [1] = "200";
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [0], 00f) / 200;
						answer = args [0] + "÷" + args [1];
						wrongAnswer = args [1] + "÷" + args [0];
						break;
					case 2:
						args [0] = Random.Range (0.1f, 0.9f).ToString ("0.0");
						args [1] = Random.Range (10, 99).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 00f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 3:
						args [0] = Random.Range (1.1f, 9.9f).ToString ("0.0");
						args [1] = Random.Range (10.1f, 20.9f).ToString ("0.0");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 00.0f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 4:
						args [0] = "2";
						args [1] = Random.Range (0.1f, 0.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = 2 / getFloat (args [1], 0.00f);
						answer = args [0] + "÷" + args [1];
						wrongAnswer = args [1] + "÷" + args [0];
						break;
					case 5:
						args [0] = Random.Range (0.1f, 0.9f).ToString ("0.00");
						args [1] = Random.Range (10.1f, 20.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.00f) / getFloat (args [0], 00.00f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 6:
						args [0] = Random.Range (0.1f, 0.9f).ToString ("0.0");
						args [1] = Random.Range (100, 300).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 000f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 7:
						args [0] = Random.Range (0.5f, 0.9f).ToString ("0.000");
						args [1] = Random.Range (0.1f, 0.4f).ToString ("0.0");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.000f) / getFloat (args [0], 0.0f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 8:
						args [0] = Random.Range (5.0f, 9.9f).ToString ("0.0");
						args [1] = Random.Range (1.0f, 4.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [2];
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 0.00f);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 9:
						args [0] = Random.Range (10000, 20000).ToString ("0");
						for (int i = 0; i < 1; i++) {
							finalProblem += easyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += easyQ [j] [1];
						floatAnswer = getFloat (args [0], 00000f) / 39.5203f;
						answer = args [0] + "÷" + "39.5203";
						wrongAnswer = args [1] + "÷" + "39.5203";
						break;
					}

			} else if ((randomNo < 15) && (randomNo > 10)) {//加一題quotientPlusOneQ
				int j = Random.Range (0, 3);

				switch (j) {

					case 0:
						args [0] = Random.Range (100.1f, 200.9f).ToString ("0.0");
						args [1] = Random.Range (0.1f, 9.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += quotientPlusOneQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [0], 000.0f) / getFloat (args [1], 0.00f);
						finalProblem += quotientPlusOneQ [j] [2];
						break;
					case 1:
						args [0] = Random.Range (10, 99).ToString ("0");
						args [1] = Random.Range (0.1f, 9.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += quotientPlusOneQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [0], 00f) / getFloat (args [1], 0.00f);
						finalProblem += quotientPlusOneQ [j] [2];
						break;
					case 2:
						args [0] = Random.Range (100, 300).ToString ("0");
						args [1] = Random.Range (10.0f, 20.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += quotientPlusOneQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [0], 000f) / getFloat (args [1], 00.00f);
						finalProblem += quotientPlusOneQ [j] [2];
						break;
					case 3:
						args [0] = Random.Range (100.0f, 200.0f).ToString ("0.0");
						args [1] = Random.Range (100.0f, 500.0f).ToString ("0.0");
						for (int i = 0; i < 2; i++) {
							finalProblem += quotientPlusOneQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [0], 000.0f) / getFloat (args [1], 000.0f);
						finalProblem += quotientPlusOneQ [j] [2];
						break;
					}
					answer = args [0] + "÷" + args [1];
					wrongAnswer = args [1] + "÷" + args [0];
					
			} else if ((randomNo < 17) && (randomNo > 14)) {//列式專用題formulaOnlyQ

				int j = Random.Range (0, 1);

				switch (j) {

					case 0:
						args [0] = Random.Range (0.1f, 0.9f).ToString ("0.0");
						args [1] = Random.Range (10, 99).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += formulaOnlyQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 00f);
						finalProblem += formulaOnlyQ [j] [2];
						break;
					case 1:
						args [0] = Random.Range (0.1f, 0.9f).ToString ("0.0");
						args [1] = Random.Range (0.1f, 0.9f).ToString ("0.00");
						for (int i = 0; i < 2; i++) {
							finalProblem += formulaOnlyQ [j] [i];
							finalProblem += args [i];
						}
						floatAnswer = getFloat (args [1], 0.0f) / getFloat (args [0], 0.00f);
						finalProblem += formulaOnlyQ [j] [2];
						break;
					}
					answer = args [1] + "÷" + args [0];
					wrongAnswer = args [1] + "×" + args [0];
					
			} else {//大數小數題bigSamllQ

				int j = Random.Range (0, 1);

				switch (j) {

					case 0:
						args [0] = Random.Range (200, 700).ToString ("0");
						args [1] = Random.Range (10, 99).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += formulaOnlyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += formulaOnlyQ [j] [2];
						floatAnswer = getFloat (args [1],0) / getFloat (args [0], 0);
						answer = args [1] + "÷" + args [0];
						wrongAnswer = args [0] + "÷" + args [1];
						break;
					case 1:
						args [0] = Random.Range (1.1f, 9.9f).ToString ("0.00");
						args [0] = Random.Range (10, 99).ToString ("0");
						for (int i = 0; i < 2; i++) {
							finalProblem += formulaOnlyQ [j] [i];
							finalProblem += args [i];
						}
						finalProblem += formulaOnlyQ [j] [2];
						floatAnswer = getFloat (args [0], 0.00f) / getFloat (args [1], 0);
						answer = args [0] + "÷" + args [1];
						wrongAnswer = args [1] + "÷" + args [0];
						break;
				}
			}
		}
			
		return finalProblem;
	}

	public string getAnswer(){

		if (type == 3) {
			
			return floatAnswer.ToString ("0.00");
		} else {
			return answer;
		}
	}

	public string getWrongAnswer(){
		if (type == 3) {
			wrongAnswer = (1.0f/floatAnswer).ToString ("0.00");
			return wrongAnswer;
		}else {
			return wrongAnswer;
		}
	}
	int countWrong = 0;

	public string promptWrong(){

		switch (type) {//題目type
		case 1://純算數

			if (countWrong == 0) {//第一次錯
				countWrong++;
				return "\t除數和被除數再想想看";
			}
		
			if (countWrong == 1) {//錯一樣的
				countWrong = 0;
				if (type == 1 && randomNo == 1) {
					return "\t被除數: " + dividend.ToString("0") + "，除數: " + divisor.ToString("0") + "\n\t" + dividend.ToString("0") + "=" + divisor.ToString("0") + "×商數";
				}
				return "\t被除數: " + dividend.ToString("0.00") + "，除數: " + divisor.ToString("0.00") + "\n\t" + dividend.ToString("0.00") + "=" + divisor.ToString("0.00") + "×商數";
			} 
			break;
				

			case 2://列式
				if (randomNo < 15) {
				return "\t被除數÷除數=?\n\t"+answer+"=?";
				} else if (randomNo == 15 || randomNo == 16) {
				return "\t被除數÷除數\n\t乘並不一定變大，除也不一定變小\n\t"+answer;
				} else if (randomNo == 17 || randomNo == 18) {
				return "\t被除數÷除數\n\t不一定大的數就是被除數\n\t"+answer;
				}
				break;
		case 3:
			if (countWrong == 0) {//第一次錯
				countWrong++;
				return "\t除數和被除數再想清楚";	
			}

			if (countWrong == 1) {//錯一樣的
				countWrong = 0;
				if (randomNo < 15) {
					return "\t被除數÷除數=?\n\t"+answer+"=?";
				} else if (randomNo == 15 || randomNo == 16) {
					return "\t被除數÷除數\n\t乘並不一定變大，除也不一定變小\n\t"+answer;
				} else if (randomNo == 17 || randomNo == 18) {
					return "\t被除數÷除數\n\t不一定大的數就是被除數\n\t"+answer;
				}
			}
			break;
		}return "";

	}
	int count = 0;//同樣概念錯幾次
	public string prompt(){

		switch (type) {//題目type
		case 1://純算數

			if (count == 0) {//第一次錯
				
				if (randomNo == 1 || randomNo == 2 || randomNo == 3) {//除整數
					count++;
					return "\t注意商的小數點";
				} else {
					count++;
					return "\t注意小數點的移位";
				}
			}

			
			if (count == 1) {//錯一樣的都選wrongAnswer
				count = 0;
				if (randomNo == 1 || randomNo == 2 || randomNo == 3)//除整數
					return "\t小數點要標在被除數被移位前的小數點位置";
				else
					return "\t依照除數的小數點後的位數，將除數與被除數小數點同時向右移位";
			}
			break;
		case 3:
			if (count == 0) {//第一次錯
				count++;
				return "\t注意小數點的移位";
			}
			if (count == 1) {//錯一樣的都選wrongAnswer
				count = 0;
				return "\t依照除數的小數點後的位數，將除數與被除數小數點同時向右移位";
			}
			break;

		}return "";

	}



	private float getFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
}