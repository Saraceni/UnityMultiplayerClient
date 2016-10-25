using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	public float respawnTime = 1;

	public void Respawn() {
		Invoke ("clearPosition", respawnTime);
	}

	private void clearPosition() {
		transform.position = Vector3.zero;
	}
}
