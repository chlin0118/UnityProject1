using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine; 

public class QAProblemGenerator : MonoBehaviour {

	const int PUREarithmetic = 1;//type1純算數
	const int APPLICATIONformula = 2;//type2應用題列式
	const int APPLICATIONarithmetic = 3;//type3應用題算數


	string[][] question = new string[][] {new string[] {"Q:一大桶果汁有","公升，每","公升分裝成一小桶，共可以分裝成幾小桶？"}};
	double[][] args = new double[][]{new double[]{19.2,1.2}};
	int[] typeCutLine = new int[] {1,3};//type1*1題(<1),type2*2題(>=1&&<3),type3*1題(>=3)

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
	
	int type = 0;//題目類型
	int argc = 0;
	double[] args = {};
	string[] statements = {};
	float answer = 0.0f;//答案
	float choice1 = 0.0f;//選項1
	float choice2 = 0.0f;//選項2
	float choice3 = 0.0f;//選項3
	string finalProblem = "456";//題目

	public Problems(int type){//type1
		this.type = type;
		setAnswer();

	}


	public Problems(int type,string[] statements,double[] args){//type2&type3
		this.type = type;
		this.argc = args.Length;

		//read in args
		for (int i = 0; i < argc; i++) {
			this.args [i] = args [i];
		}
		//read in statements
		for (int i = 0; i < statements.Length; i++) {
			this.statements [i] = statements [i];
		}
		//getFinalProblem();
	}


	public string getFinalProblem(){

	
		if (type == 1) {//小數除整數

			float divisor = Random.Range(2.0f, 10.0f);
			divisor = Mathf.Round(divisor);

			float dividend = answer * divisor;

			finalProblem = dividend.ToString ("0.00") + "÷" + divisor + "=" ;//reset final problem area

			return finalProblem;
		
		} 
		else {
			finalProblem = "";
			for (int i = 0; i < argc; i++) {
				finalProblem += statements [i];
				finalProblem += args [i];
			}
			finalProblem += statements [argc];
			return finalProblem;
		}
	}
	void setAnswer(){

		const int PUREarithmetic = 1;//type1純算數
		const int APPLICATIONformula = 2;//type2應用題列算式
		const int APPLICATIONarithmetic = 3;//type3應用題算數

		answer = Random.Range(0.1f, 100.0f);
	
	}

	public string getAnswer(){
		const int PUREarithmetic = 1;//type1純算數
		const int APPLICATIONformula = 2;//type2應用題列算式
		const int APPLICATIONarithmetic = 3;//type3應用題算數

		switch (type) {
		case PUREarithmetic:
			return answer.ToString ("0.00");
			break;
		case APPLICATIONformula:
			return answer.ToString ("0.00");
			break;
		case APPLICATIONarithmetic:
			return answer.ToString ("0.00");
			break;
		} 
		return answer.ToString ("0.00");
			
		
		 
	}
}