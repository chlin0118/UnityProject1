using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	public GameObject backgroundImage;
	//public GameObject joystickImage;
	public Image bgImg;
	public Image joystickImg;
	private Vector3 inputVector;

	private static bool joystickExists;
	private GameObject canvas;
	PointerEventData eventData;

	// Use this for initialization
	void Start () {
		bgImg = backgroundImage.GetComponent<Image> ();
		joystickImg = backgroundImage.transform.GetChild(0).GetComponent<Image>();
		bgImg.enabled = false;
		joystickImg.enabled = false;

		canvas = transform.parent.gameObject;

		if (!joystickExists) {
			joystickExists = true;
			DontDestroyOnLoad (canvas);

		} else {
			Destroy(canvas);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Level Loaded");
		Debug.Log(scene.name);
		Debug.Log(mode);
		//載入新場景取消指標的拖曳
		if (eventData != null) {
			eventData.pointerDrag = null;
			OnPointerUp (eventData);
			Debug.Log ("eventData: " + eventData.pointerId);
			//pointerCount = 0;
		}
	}

	public virtual void OnDrag(PointerEventData ped){
		if (ped.pointerId == 0 || ped.pointerId == -1) {
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform
			, ped.position, ped.pressEventCamera, out pos)) {

				//Debug.Log ("pos: " + pos);
				pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
				//Debug.Log ("pos: " + pos);
				inputVector = new Vector3 (pos.x * 2 - 1, pos.y * 2 - 1, 0);
				inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

				joystickImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 4)
				, inputVector.y * (bgImg.rectTransform.sizeDelta.y / 4));

				//Debug.Log ("inputVector: " + inputVector);
				Debug.Log ("ped: " + ped.pointerId);
			}
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		if (ped.pointerId == 0 || ped.pointerId == -1) {
			bgImg.enabled = true;
			joystickImg.enabled = true;
			bgImg.rectTransform.anchoredPosition = new Vector3 (ped.position.x - bgImg.rectTransform.sizeDelta.x * 0.5f
			, ped.position.y - bgImg.rectTransform.sizeDelta.y * 0.5f);
		
			Debug.Log ("ped ID: " + ped.pointerId);
			eventData = ped;
			OnDrag (ped);
		}
	}

	public virtual void OnPointerUp(PointerEventData ped){
		if(ped.pointerId == 0 || ped.pointerId == -1){
			bgImg.enabled = false;
			joystickImg.enabled = false;
			inputVector.x = 0;
			inputVector.y = 0;
		}
	}

	public float Horizontal(){
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return 0;
	}

	public float Vertical(){
		if (inputVector.y != 0)
			return inputVector.y;
		else
			return 0;
	}
}
