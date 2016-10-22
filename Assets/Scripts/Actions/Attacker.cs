using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {

	public float attackDistance;
	public float attackRate;

	float lastAttackTime = 0;

	Targeter targeter;

	// Use this for initialization
	void Start () {
		targeter = GetComponent<Targeter> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isReadyToAttack() && targeter.IsInRange (attackDistance)) {
			Debug.Log ("Attacking " + targeter.target.name);
			var targetId = targeter.target.gameObject.GetComponent<NetworkEntity>().id;
			Network.Attack(targetId, transform.position);
			lastAttackTime = Time.time;
		}
	}	

	bool isReadyToAttack() {
		return Time.time - lastAttackTime > attackRate && targeter.target;
	}

	public static void Attack(GameObject attacking, GameObject target) {

		target.GetComponent<Hittable> ().health -= 10;
		Vector3 targetPosition = target.transform.position;

		attacking.transform.LookAt(new Vector3(targetPosition.x, 0, targetPosition.z));
		attacking.GetComponent<Animator> ().SetTrigger ("Attack");
	}
}
