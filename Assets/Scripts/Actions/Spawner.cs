using UnityEngine;
using System.Collections.Generic;
using SocketIO;

public class Spawner : MonoBehaviour {

	public GameObject myPlayer;
	public GameObject playerPrefab;
	public SocketIOComponent socket;

	Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

	public GameObject SpawnPlayer(string id) {
		var player = Instantiate (playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

		// Inject references
		player.GetComponent<ClickFollow> ().myPlayer = myPlayer;
		player.GetComponent<NetworkEntity> ().id = id;
		AddPlayer (id, player);

		return player;
	}

	public void AddPlayer(string id, GameObject player) {
		players.Add (id, player);
	}

	public void UpdatePlayerPosition(string id, Vector3 position) {
		players [id].transform.position = position;
	}

	public GameObject FindPlayer(string id) {
		return players [id];
	}

	public void Remove(string id) {
		Destroy (players[id]);
		players.Remove (id);
	}
}
