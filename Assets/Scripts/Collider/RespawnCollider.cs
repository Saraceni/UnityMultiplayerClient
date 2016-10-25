using UnityEngine;
using System.Collections;

public class RespawnCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Respawner respawner = other.gameObject.GetComponent<Respawner> ();
		if (respawner) {
			respawner.Respawn();
		}
	}
}
