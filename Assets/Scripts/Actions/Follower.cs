using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public Targeter targeter;

	public float scanFrequency = 0.5f;
	public float stopFollowDistance = 2;

	NavMeshAgent agent;

	float lastScanTime = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		targeter = GetComponent<Targeter> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isReadyToScan ()) {
			if(targeter.target.gameObject.GetComponent<Hittable>().IsDead) {
				targeter.target = null;
			}
			else if(!targeter.IsInRange(stopFollowDistance)) {
				agent.SetDestination(targeter.target.position);
			} else if(agent.hasPath) {
				agent.SetDestination(transform.position);
			}
		}
	
	}

	bool isReadyToScan ()
	{
		return Time.time - lastScanTime > scanFrequency && targeter.target;
	}

	public void ClearPositionAndTarget() {
		agent.ResetPath ();
		targeter.target = null;
	}

}
