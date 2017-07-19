using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	bool fingerHold = false;
	Vector3 startPos;
	Vector2 touchPos;
	Vector3 endPos;
	float realSpeed;
	Camera camera;


	// Use this for initialization
	void Start () {
		startPos = new Vector3(0,0,0);
		endPos = new Vector3(800,1800,0);
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.touchCount > 0){                      // get total touches on screen
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began){       // when finger starts touching
				//startPos = touch.position;             // get its position
				startPos = gameObject.transform.position;
				endPos = Camera.main.ScreenToWorldPoint(touch.position);
				fingerHold = true;
			}
			else if(touch.phase == TouchPhase.Moved){  // when finger moves
				//endPos = touch.position;               // get its position too
			}
			else if(touch.phase == TouchPhase.Ended){  // when finger released
				fingerHold = false;                    
			}
		}*/
		float height = Camera.main.orthographicSize * 2;
		float width = height * Camera.main.aspect;
		realSpeed = Screen.width / width * moveSpeed;
		Debug.Log ("height: " + height);
		Debug.Log ("width: " + width);
		Debug.Log ("realSpeed: " + realSpeed);
		Debug.Log ("Screen.width: " + Screen.width);

		Debug.Log ("Camera.main.ScreenToWorldPoint(endPos) x:" + Camera.main.ScreenToWorldPoint(endPos).x);
		Debug.Log ("Camera.main.ScreenToWorldPoint(endPos) y:" + Camera.main.ScreenToWorldPoint(endPos).y);
		Debug.Log ("Camera.main.ScreenToWorldPoint(endPos) z:" + Camera.main.ScreenToWorldPoint(endPos).z);
		Vector3 positionForXY = Camera.main.ScreenToWorldPoint(Vector3.MoveTowards(Camera.main.WorldToScreenPoint(transform.position), endPos, realSpeed * Time.deltaTime));
		transform.position = new Vector3 (positionForXY.x, positionForXY.y, 0);

		if(fingerHold){
			Debug.Log ("start x:" + startPos.x);
			Debug.Log ("start y:" + startPos.y);
			Debug.Log ("start z:" + startPos.z);
			Debug.Log ("end x:" + endPos.x);
			Debug.Log ("end y:" + endPos.y);

			float deltaX = endPos.x - startPos.x;      // get the difference of x position
			float deltaY = endPos.y - startPos.y;      // and the y position
			bool horizontal = false;

			/*if(Mathf.Abs (deltaX) > Mathf.Abs (deltaY)) // if difference of x is bigger than y
				horizontal = true;                      // meaning we want it to move horizontally

			if(horizontal){
				if(deltaX > 0) // if difference of x is bigger than 0, meaning finger is moving right, translate it to right
					transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
				else if(deltaX < 0) // the reverse, move left
					transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
			}
			else{ // and so on
				if(deltaY > 0)
					transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
				else if(deltaY < 0)
					transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
			}*/
			//transform.position = Camera.main.ScreenToWorldPoint(Vector3.MoveTowards(startPos, endPos, moveSpeed * Time.deltaTime ));
			//Vector3.MoveTowards(startPos, endPos, moveSpeed * Time.deltaTime);
			/*
			if(deltaX > 0) // if difference of x is bigger than 0, meaning finger is moving right, translate it to right
				transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
			else if(deltaX < 0) // the reverse, move left
				transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
			if(deltaY > 0)
				transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
			else if(deltaY < 0)
				transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);*/
		}
	}
}
