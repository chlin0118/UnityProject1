using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitController : MonoBehaviour {


	public float moveSpeed;

	private Rigidbody2D myRigidbody;
	private Animator animator;
	private bool moving;

	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Vector3 moveDirection;
	public Vector2 lastMove;

	public float waitToReload;
	private bool reloading;
	private GameObject thePlayer;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();

		//timeBetweenMoveCounter = timeBetweenMove;
		//timeToMoveCounter = timeToMove;

		timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f,timeBetweenMove * 1.25f) ;
		timeToMoveCounter = Random.Range(timeToMove * 0.75f,timeToMove * 1.25f) ;

		//lastMove = new Vector2 (0, -1);
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			timeToMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = moveDirection;
			lastMove = new Vector2 (moveDirection.normalized.x, moveDirection.normalized.y);

			if (timeToMoveCounter <= 0f){
				moving = false;
				//timeBetweenMoveCounter = timeBetweenMove;
				timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f,timeBetweenMove * 1.25f) ;
			}
		
		}
		else{
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;

			if(timeBetweenMoveCounter <= 0f){
				moving = true;
				//timeToMoveCounter = timeToMove;
				timeToMoveCounter = Random.Range(timeToMove * 0.75f,timeToMove * 1.25f) ;

				moveDirection = new Vector3 (Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
			}
		}

		animator.SetFloat ("MoveX", moveDirection.normalized.x);
		animator.SetFloat ("MoveY", moveDirection.normalized.y);
		animator.SetBool ("Moving", moving);
		animator.SetFloat ("LastMoveX", lastMove.x);
		animator.SetFloat ("LastMoveY", lastMove.y);

		if(reloading) {
			waitToReload -= Time.deltaTime;
			if(waitToReload <= 0) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				thePlayer.SetActive (true);
				thePlayer.transform.position = new Vector3 (0,0,0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		
		if(other.gameObject.name == "Player") {
			//Destroy (other.gameObject);

			other.gameObject.SetActive (false);
			reloading = true;
			thePlayer = other.gameObject;
		}
	}

}
