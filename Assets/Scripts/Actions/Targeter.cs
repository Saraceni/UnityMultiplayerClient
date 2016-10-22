using UnityEngine;
using System.Collections;

public class Targeter : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsInRange(float stopFollowDistance) {
		return (Vector3.Distance (target.position, transform.position) < stopFollowDistance);
	}
}
