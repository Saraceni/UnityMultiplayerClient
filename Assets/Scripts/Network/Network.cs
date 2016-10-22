using UnityEngine;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;

	public GameObject player;

	public Spawner spawner;

	// Use this for initialization
	void Start () {

		initSocket ();
		spawner.socket = socket;
	}

	void initSocket() {
		socket = GetComponent<SocketIOComponent> ();
		socket.On("open", OnConnected);
		socket.On("register", OnRegister);
		socket.On ("spawn", OnSpawned);
		socket.On ("move", OnMove);
		socket.On ("follow", OnFollow);
		socket.On ("attack", OnAttack);
		socket.On ("disconnected", OnDisconnected);
		socket.On ("requestPosition", OnRequestPosition);
		socket.On ("updatePosition", OnUpdatePosition);
	}

	void OnMove(SocketIOEvent e) {
		Debug.Log ("player is moving " + e.data);

		var pos = GetVectorFromJSON (e);
		var player = spawner.FindPlayer(e.data ["id"].str);
		var navigatePos = player.GetComponent<Navigator> ();

		navigatePos.NavigateTo (pos);
	}

	void OnConnected(SocketIOEvent e) {
		Debug.Log ("connected"); 
	}

	void OnSpawned(SocketIOEvent e) {
		Debug.Log ("spawned " + e.data);

		var player = spawner.SpawnPlayer (e.data ["id"].str);

		if (e.data ["x"]) {
			var movePosition = GetVectorFromJSON(e);
			var navigatePos = player.GetComponent<Navigator> ();
			navigatePos.NavigateTo (movePosition);
		}

	}

	void OnDisconnected(SocketIOEvent e) {
		var id = e.data ["id"].str;
		spawner.Remove (id);
	}

	void OnRequestPosition(SocketIOEvent e) {
		socket.Emit ("updatePosition", VectorToJSON(player.transform.position));
	}

	void OnUpdatePosition(SocketIOEvent e) {
		Debug.Log ("updating position: " + e.data);

		var pos = GetVectorFromJSON(e);
		var player = spawner.FindPlayer(e.data ["id"].str);

		player.transform.position = pos;
	}

	void OnFollow(SocketIOEvent e) {
		Debug.Log ("follow request: " + e.data);
		var targetTransform = spawner.FindPlayer (e.data["targetId"].str).transform;
		var player = spawner.FindPlayer (e.data["id"].str);
		player.GetComponent<Targeter> ().target = targetTransform;
	}

	void OnAttack(SocketIOEvent e) {
		Debug.Log ("received attack: " + e.data);

		var attackingPlayer = spawner.FindPlayer (e.data["id"].str);
		var targetPlayer = spawner.FindPlayer (e.data["targetId"].str);

		Attacker.Attack (attackingPlayer, targetPlayer);

	}

	void OnRegister(SocketIOEvent e) {
		Debug.Log ("register: " + e.data);
		string myPlayerId = e.data ["id"].str;
		spawner.AddPlayer (myPlayerId, player);
	}

	public static void Attack(string targetId) {
		Debug.Log ("attacking player: " + Network.PlayerIdToJson (targetId));
		socket.Emit ("attack", new JSONObject(Network.PlayerIdToJson (targetId)));
	}

	public static void Follow(string id) {
		// send position to node
		Debug.Log ("sending follow player id: " + Network.PlayerIdToJson(id));
		socket.Emit ("follow", new JSONObject(Network.PlayerIdToJson(id)));
	}

	public static void Move(Vector3 position) {
		Debug.Log ("sending move: " + Network.VectorToJSON (position));
		socket.Emit ("move", Network.VectorToJSON (position));
	}

	public static Vector3 GetVectorFromJSON(SocketIOEvent e) {
		return new Vector3 (e.data["x"].n, 0, e.data["y"].n);
	}

	public static Vector3 GetVectorFromJSON(SocketIOEvent e, string x, string y) {
		return new Vector3 (e.data[x].n, 0, e.data[y].n);
	}

	public static JSONObject VectorToJSON(Vector3 vector) {
		JSONObject j = new JSONObject (JSONObject.Type.OBJECT);
		j.AddField ("x", vector.x);
		j.AddField ("y", vector.z);
		return j;
	}

	public static string PlayerIdToJson(string id) {
		return string.Format (@"{{""targetId"":""{0}"",}}", id);
	}
}









