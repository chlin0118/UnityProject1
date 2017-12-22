using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;
	private static bool cameraExists;

	private float up = 0;
	private float right = 0;
	private float down = 0;
	private float left = 0;
	private bool limit = true;


	// Use this for initialization
	void Start () {
		if (!cameraExists) {
			cameraExists = true;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

		if (SceneManager.GetActiveScene ().name == "main") {
			up = 22f;
			down = -13f;
			right = 22f;
			left = -20f;
			limit = true;
		} else if (SceneManager.GetActiveScene ().name == "battlegroundUp") {
			up = -10f;
			down = -55f;
			right = 59f;
			left = 17f;
			limit = true;
		} else if (SceneManager.GetActiveScene ().name == "battlegroundRight") {
			up = -19f;
			down = -55f;
			right = 67f;
			left = 6.56f;
			limit = true;
		} else if (SceneManager.GetActiveScene ().name == "battlegroundDown") {
			up = -13f;
			down = -59f;
			right = 57f;
			left = 15f;
			limit = true;
		} else if (SceneManager.GetActiveScene ().name == "battlegroundLeft") {
			up = -10.5f;
			down = -64.5f;
			right = 68.5f;
			left = 7f;
			limit = true;
		} else {
			limit = false;
		}


		if (limit) {
			if (transform.position.y >= up) {
				transform.position = new Vector3 (transform.position.x, up, transform.position.z);
			} else if (transform.position.y <= down) {
				transform.position = new Vector3 (transform.position.x, down, transform.position.z);
			}
			if (transform.position.x >= right) {
				transform.position = new Vector3 (right, transform.position.y, transform.position.z);
			} else if (transform.position.x <= left) {
				transform.position = new Vector3 (left, transform.position.y, transform.position.z);
			}
		}
	}
}
