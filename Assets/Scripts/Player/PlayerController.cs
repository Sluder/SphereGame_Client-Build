using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{

	public Camera playerCamera;
	public double maxRollSpeed = 30;
	public float jumpHeight = 25;

	private Rigidbody player;
	private bool upBtn, downBtn, leftBtn, rightBtn, jumpBtn, isGrounded;

	void Start()
	{
		player = this.GetComponent<Rigidbody>();
	}
		
	void Update()
	{
		player.transform.forward = (playerCamera.transform.forward - player.transform.position);

		// Get key press, update position in physics loop (FixedUpdate)
		if (Input.GetKey (KeyCode.W)) {
			upBtn = true;
		} else {
			upBtn = false;
		}
		if (Input.GetKey (KeyCode.S)) {
			downBtn = true;
		} else {
			downBtn = false;
		}
		if (Input.GetKey (KeyCode.D)) {
			rightBtn = true;
		} else {
			rightBtn = false;
		}
		if (Input.GetKey (KeyCode.A)) {
			leftBtn = true;
		} else {
			leftBtn = false;
		}
		if (Input.GetKey (KeyCode.Space)) {
			jumpBtn = true;
		} else {
			jumpBtn = false;
		}
	}

	void FixedUpdate() 
	{
		// Move player relative to PlayerCamera's view
		if (upBtn) {
			var forwardDir = playerCamera.transform.TransformDirection (Vector3.forward);
			player.AddForce (forwardDir * (float) maxRollSpeed);
		}
		if (downBtn) {
			var backDir = playerCamera.transform.TransformDirection (Vector3.back);
			player.AddForce (backDir * (float) maxRollSpeed);
		}
		if (leftBtn) {
			var leftDir = playerCamera.transform.TransformDirection (Vector3.left);
			player.AddForce (leftDir * (float) maxRollSpeed);
		}
		if (rightBtn) {
			var rightDir = playerCamera.transform.TransformDirection (Vector3.right);
			player.AddForce (rightDir * (float) maxRollSpeed);
		}
		if (jumpBtn && isGrounded) {
			player.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);

			isGrounded = false;
		}

        // Top out player speed
		if (player.velocity.magnitude > maxRollSpeed) {
			player.velocity = player.velocity.normalized * (float) maxRollSpeed;
        }
    }

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Walkable" && !isGrounded) {
			isGrounded = true;
			maxRollSpeed *= 1.5;
		}
	}

	void OnCollisionExit(Collision coll)
	{
		if (coll.gameObject.tag == "Walkable" && !isGrounded) {
			isGrounded = false;
			maxRollSpeed /= 1.5;
		}
	}

}