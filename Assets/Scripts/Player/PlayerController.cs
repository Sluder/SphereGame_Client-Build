using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {

	private Rigidbody player;
	public Camera playerCamera;

	public float maxRollSpeed;
	public float jumpHeight;

	private bool up, down, left, right, jump;

	void Start() {
		player = this.GetComponent<Rigidbody>();
	}
		
	void Update () {
		player.transform.forward = (playerCamera.transform.forward - player.transform.position);

		if (Input.GetKey (KeyCode.W)) {
			up = true;
		} else {
			up = false;
		}
		if (Input.GetKey (KeyCode.S)) {
			down = true;
		} else {
			down = false;
		}
		if (Input.GetKey (KeyCode.D)) {
			right = true;
		} else {
			right = false;
		}
		if (Input.GetKey (KeyCode.A)) {
			left = true;
		} else {
			left = false;
		}
		if (Input.GetKey (KeyCode.Space)) {
			jump = true;
		} else {
			jump = false;
		}
	}

	void FixedUpdate () {

		// Move player relative to PlayerCamera's view
		if (up) {
			var forwardDir = playerCamera.transform.TransformDirection (Vector3.forward);
			player.AddForce (forwardDir * maxRollSpeed);
		}
		if (down) {
			var backDir = playerCamera.transform.TransformDirection (Vector3.back);
			player.AddForce (backDir * maxRollSpeed);
		}
		if (left) {
			var leftDir = playerCamera.transform.TransformDirection (Vector3.left);
			player.AddForce (leftDir * maxRollSpeed);
		}
		if (right) {
			var rightDir = playerCamera.transform.TransformDirection (Vector3.right);
			player.AddForce (rightDir * maxRollSpeed);
		}
		if (jump) {
			player.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
			maxRollSpeed /= 3;
		}

        // Check player speed
        if (player.velocity.magnitude > maxRollSpeed) {
			player.velocity = player.velocity.normalized * maxRollSpeed;
		}
	}

}