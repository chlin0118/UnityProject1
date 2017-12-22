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


	//double[][] args = new double[][]{new double[]{19.2,1.2}};

	int type = 0;//題目類型
	int argc = 0;
	double[] args = {};
	string[] statements = {};
	float answer = 0.0f;//答案
	float choice1 = 0.0f;//選項1
	float choice2 = 0.0f;//選項2
	float choice3 = 0.0f;//選項3
	string finalProblem = "456";//題目

	public Problems(int type){
		this.type = type;
		setAnswer();

	}
		
	public string getFinalProblem(){

	
		if (type == 1) {//小數除整數

			float divisor = Random.Range(2.0f, 10.0f);
			string div = divisor.ToString("0.0");
			float divisor1 = getFloat (div, 0.0f);

			float dividend = answer * divisor1;

			finalProblem = dividend.ToString ("0.00") + "÷" + divisor1 + "=" ;

			return finalProblem;
		
		} 
		else {
			finalProblem = "";
			string[] args = new string[]{"0","0"};



				args[0] = Random.Range(2.0f, 10.0f).ToString("0.0");
				args[1] = Random.Range(2.0f, 10.0f).ToString("0.0");

				for (int i = 0; i < 2; i++) {
					finalProblem += bigSamllQ [1][i];
					finalProblem += args [i];
				}
				finalProblem += bigSamllQ [1][2];
				return finalProblem;

				
		}
	}
		
	void setAnswer(){

		switch (type) {

		case PUREarithmetic:
			answer = Random.Range (0.1f, 100.0f);
			break;
		case APPLICATIONformula:
			
			break;
		case APPLICATIONarithmetic:
			
			break;
		} 
	}

	public string getAnswer(){

		switch (type) {

		case PUREarithmetic:
			return answer.ToString ("0.00");

		case APPLICATIONformula:
			return answer.ToString ("0.00");

		case APPLICATIONarithmetic:
			return answer.ToString ("0.00");

		} 
		return answer.ToString ("0.00");
		 
	}

	private float getFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
}