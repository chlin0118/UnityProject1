using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;
	private static bool cameraExists;

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
		if(transform.position.y >= 22f){
			transform.position = new Vector3 (transform.position.x, 22f, transform.position.z);
		}
		else if (transform.position.y <= -13){
			transform.position = new Vector3 (transform.position.x, -13f, transform.position.z);
		}
		if(transform.position.x >= 22f){
			transform.position = new Vector3 (22f, transform.position.y, transform.position.z);
		}
		else if (transform.position.x <= -20){
			transform.position = new Vector3 (-20f, transform.position.y, transform.position.z);
		}
	}
}
