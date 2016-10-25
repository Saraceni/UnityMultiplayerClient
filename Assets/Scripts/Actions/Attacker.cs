using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Attacker : MonoBehaviour {

	public float attackRate = 1;

	float lastAttackTime = 0;

	Animator animator;
	Navigator navigator;

	private bool isAttacking = false;

	// Use this for initialization
	void Start () {
		navigator = GetComponent<Navigator> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(!isAttacking && CrossPlatformInputManager.GetButtonDown("Attack")) {
			isAttacking = true;
			Network.Attack(gameObject, navigator.IsWalking);
		}
	}	

	public void Attack() {
		animator.SetTrigger ("Attack");
		Invoke("resetIsAttacking", 1);
	}

	private void resetIsAttacking() {
		isAttacking = false;
	}

	bool isReadyToAttack() {
		return Time.time - lastAttackTime > attackRate;
	}


}
