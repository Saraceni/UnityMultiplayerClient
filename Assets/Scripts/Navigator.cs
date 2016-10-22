using UnityEngine;
using System.Collections;

public class Navigator : MonoBehaviour {

	NavMeshAgent agent;
	Follower follower;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		follower = GetComponent<Follower> ();
	}

	public void NavigateTo(Vector3 position) {
		agent.SetDestination (position);
		follower.target = null;
	}

	void Update() {
		GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);
	}
}
