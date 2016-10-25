using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Navigator : MonoBehaviour {

	public float updateRate = 0.1f;

	public bool IsWalking {
		get { return isWalking; }
	}

	Animator animator;
	float lastUpdateTime = 0;
	bool isWalking = false;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public void NavigateTo(Vector3 position) {
		animator.SetBool("Attack", false);
	}

	void Update() {

		var horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		var vertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 lookAtDirection = new Vector3 (horizontal, 0, vertical);
		Vector3 lookAtVector = transform.position + lookAtDirection;
		transform.LookAt (lookAtVector);

		isWalking = (horizontal != 0 || vertical != 0);
		animator.SetBool ("IsWalking", isWalking);

		if (shouldUpdate ()) {
			updatePosition();
		}

	}

	bool shouldUpdate() {
		return Time.time - lastUpdateTime > updateRate;
	}

	void updatePosition() {
		Network.UpdatePlayerPosition (gameObject, isWalking);
		lastUpdateTime = Time.time;
	}


}
