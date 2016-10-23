using UnityEngine;
using System.Collections;

public class Hittable : MonoBehaviour {

	public float health = 100;
	public float respawnTime = 3;
	public bool IsDead {
		get { return health <= 0; }
	}

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	public void OnHit() {
		health -= 10;

		if (IsDead) {
			animator.SetTrigger("Dead");
			Invoke ("Spawn", respawnTime);
		}
	}

	void Spawn() {
		Debug.Log ("Spawning my player");
		GetComponent<Follower> ().ClearPositionAndTarget ();
		transform.position = Vector3.zero;
		health = 100;
		animator.SetTrigger ("Spawn");
	}
}
