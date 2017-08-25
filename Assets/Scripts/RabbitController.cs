using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();

		timeBetweenMoveCounter = timeBetweenMove;
		timeToMoveCounter = timeToMove;

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
				timeBetweenMoveCounter = timeBetweenMove;
			}
		
		}
		else{
			timeBetweenMoveCounter -= Time.deltaTime;
			myRigidbody.velocity = Vector2.zero;

			if(timeBetweenMoveCounter <= 0f){
				moving = true;
				timeToMoveCounter = timeToMove;

				moveDirection = new Vector3 (Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
			}
		}

		animator.SetFloat ("MoveX", moveDirection.normalized.x);
		animator.SetFloat ("MoveY", moveDirection.normalized.y);
		animator.SetBool ("Moving", moving);
		animator.SetFloat ("LastMoveX", lastMove.x);
		animator.SetFloat ("LastMoveY", lastMove.y);
	}
}
