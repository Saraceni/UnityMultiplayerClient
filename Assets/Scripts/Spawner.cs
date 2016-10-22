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
		player.GetComponent<ClickFollow> ().myPlayerFollower = myPlayer.GetComponent<Follower>();
		player.GetComponent<NetworkFollow> ().socket = socket;
		player.GetComponent<NetworkEntity> ().id = id;
		AddPlayer (id, player);

		return player;
	}

	public void AddPlayer(string id, GameObject player) {
		players.Add (id, player);
	}

	public GameObject FindPlayer(string id) {
		return players [id];
	}

	public void Remove(string id) {
		Destroy (players[id]);
		players.Remove (id);
	}
}
