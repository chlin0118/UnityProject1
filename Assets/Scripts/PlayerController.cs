using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;

	private Animator animator;
	private Rigidbody2D myRigidbody;


	private bool fingerHold = false;
	private Vector3 startPos;
	private Vector2 touchPos;
	private Vector3 endPos;
	//private float realSpeed;

	private bool playerMoving;
	public Vector2 lastMove;
	private static bool playerExists;

	public string startPoint;
	private TouchController joystick;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		//float height = Camera.main.orthographicSize * 2;
		//float width = height * Camera.main.aspect;
		//realSpeed = Screen.width / width * moveSpeed;
		GameObject go = GameObject.Find("Touch");
		joystick = go.GetComponent<TouchController> ();
		if (!playerExists) {
			playerExists = true;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.touchCount > 0){                      // get total touches on screen
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began){       // when finger starts touching
				touchPos = touch.position;
				fingerHold = true;
			}
			else if(touch.phase == TouchPhase.Moved){  // when finger moves
				touchPos = touch.position;
			}
			else if(touch.phase == TouchPhase.Stationary){  
			}
			else if(touch.phase == TouchPhase.Ended){  // when finger released
				lastMove = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
				animator.SetFloat ("LastMoveX", lastMove.x);
				animator.SetFloat ("LastMoveY", lastMove.y);
				animator.SetFloat ("MoveX", 0f);
				animator.SetFloat ("MoveY", 0f);
				fingerHold = false;                    
			}
		}
			
		if (fingerHold) {
			endPos = Camera.main.ScreenToWorldPoint (new Vector3 (touchPos.x, touchPos.y, 0));
			startPos = transform.position;

			float deltaX = endPos.x - startPos.x;      // get the difference of x position
			float deltaY = endPos.y - startPos.y;      // and the y position

			if (Mathf.Abs (deltaX) > Mathf.Abs (deltaY)) {
				animator.SetFloat ("MoveY", 0f);
				if (deltaX > 0) {
					animator.SetFloat ("MoveX", 1f);
				} else {
					animator.SetFloat ("MoveX", -1f);
				}
			} else {
				animator.SetFloat ("MoveX", 0f);
				if (deltaY > 0) {
					animator.SetFloat ("MoveY", 1f);
				} else {
					animator.SetFloat ("MoveY", -1f);
				}
			}
				
			Vector3 moveUnitVector = (endPos - startPos).normalized;
			myRigidbody.velocity = new Vector2 (moveUnitVector.x, moveUnitVector.y) * moveSpeed;

		} else {
			myRigidbody.velocity = new Vector2 (0f, 0f);
		}

		animator.SetBool ("PlayerMoving", fingerHold);*/

		playerMoving = false;

		//Debug.Log ("joystick.Horizontal (): " + joystick.Horizontal ());

		if (joystick.Horizontal () > 0.2f || joystick.Horizontal () < -0.2f) {
			myRigidbody.velocity = new Vector2 (joystick.Horizontal() * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			//lastMove = new Vector2 (joystick.Horizontal(), 0f);
		}

		if (joystick.Vertical () > 0.2f || joystick.Vertical () < -0.2f) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, joystick.Vertical() * moveSpeed);
			playerMoving = true;
			//lastMove = new Vector2 (0f, joystick.Vertical());
		}

		if (playerMoving){
			lastMove = new Vector2 (joystick.Horizontal(), joystick.Vertical());
		}

		if(joystick.Horizontal () < 0.2f && joystick.Horizontal () > -0.2f){
			myRigidbody.velocity = new Vector2 (0f, myRigidbody.velocity.y);
		}
		if(joystick.Vertical () < 0.2f && joystick.Vertical () > -0.2f){
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, 0f);
		}

		animator.SetFloat ("MoveX", joystick.Horizontal ());
		animator.SetFloat ("MoveY", joystick.Vertical ());
		animator.SetBool ("PlayerMoving", playerMoving);
		animator.SetFloat ("LastMoveX", lastMove.x);
		animator.SetFloat ("LastMoveY", lastMove.y);
	}


	public void Save(){
		SaveLoadManager.SavePlayer (this);
	}

	public void Load(){
		PlayerData data = SaveLoadManager.LoadPlayer ();
		transform.position = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
	}
}
