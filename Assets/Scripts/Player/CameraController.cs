using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Rigidbody player;
	private Vector3 offset;

	public float camSpeedH = 5.0f;
	public float camSpeedV = 5.0f;

	private float camRotationH = 0.0f;
	private float camRotationV = 0.0f;
	private float camViewLimit = 60.0f;

	void Start() {
		offset = transform.position - player.transform.position;
		Cursor.lockState = CursorLockMode.Locked;
	}
		
	void Update () {
		// Mouse first person look
		camRotationH += camSpeedH * Input.GetAxis("Mouse X");
		camRotationV -= camSpeedV * Input.GetAxis("Mouse Y");

		// Limit camera rotation looking up/down (Y-Axis)
		if (camRotationV < -camViewLimit) {
			camRotationV = -camViewLimit;
            
		} else if (camRotationV > camViewLimit) {
			camRotationV = camViewLimit;
		}
  
		transform.eulerAngles = new Vector3(camRotationV, camRotationH, 0.0f);
	}
		
	void LateUpdate () {

		transform.position = player.transform.position + offset;
	}

}