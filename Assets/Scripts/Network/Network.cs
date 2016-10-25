using UnityEngine;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {

	public const string PlayerId = "id";
	public const string PositionX = "x";
	public const string PositionY = "y";
	public const string Rotation = "rot";
	public const string IsWalking = "walking";
	
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
		socket.On ("attack", OnAttack);
		socket.On ("disconnected", OnDisconnected);
		socket.On ("updatePosition", OnUpdatePosition);
	}

	void OnConnected(SocketIOEvent e) {
		Debug.Log ("connected"); 
	}

	void OnSpawned(SocketIOEvent e) {
		Debug.Log ("spawned " + e.data);

		var player = spawner.SpawnPlayer (e.data [PlayerId].str);
		UpdatePlayerData (player, e);

	}

	void OnDisconnected(SocketIOEvent e) {
		var id = e.data [PlayerId].str;
		spawner.Remove (id);
	}

	void OnUpdatePosition(SocketIOEvent e) {

		Debug.Log ("OnUpdatePosition: " + e.data);

		var targetPlayer = spawner.FindPlayer (e.data[PlayerId].str);
		UpdatePlayerData (targetPlayer, e);

	}

	void OnAttack(SocketIOEvent e) {
		Debug.Log ("OnAttack " + e.data);

		var attackingPlayer = spawner.FindPlayer (e.data[PlayerId].str);
		UpdatePlayerData (attackingPlayer, e);
		attackingPlayer.GetComponent<Attacker> ().Attack();

	}

	void OnRegister(SocketIOEvent e) {
		Debug.Log ("OnRegister " + e.data);
		string myPlayerId = e.data [PlayerId].str;
		player.GetComponent<NetworkEntity> ().id = myPlayerId;
		spawner.AddPlayer (myPlayerId, player);
	}

	public static void Attack(GameObject player, bool isWalking) {
		Debug.Log ("attacking");
		socket.Emit ("attack", PlayerDataToJSON(player.transform, isWalking));
	}

	public static void UpdatePlayerPosition(GameObject player, bool isWalking) {
		socket.Emit ("updatePosition", PlayerDataToJSON(player.transform, isWalking));
	}

	public static Vector3 GetVectorFromJSON(SocketIOEvent e) {
		return new Vector3 (e.data[PositionX].n, 0, e.data[PositionY].n);
	}

	public static void UpdatePlayerData(GameObject targetPlayer, SocketIOEvent e) {

		targetPlayer.transform.position = GetVectorFromJSON (e);
		targetPlayer.GetComponent<Animator> ().SetBool ("IsWalking", e.data [IsWalking].b);

		float y_rotation = e.data [Rotation].n;
		Vector3 attackingPlayerRotation = targetPlayer.transform.localEulerAngles;
		attackingPlayerRotation.y = y_rotation;
		
		targetPlayer.transform.localEulerAngles = attackingPlayerRotation;
	}

	public static JSONObject PlayerDataToJSON(Transform playerTransform, bool isWalking) {
		JSONObject j = new JSONObject (JSONObject.Type.OBJECT);
		j.AddField (PositionX, playerTransform.position.x);
		j.AddField (PositionY, playerTransform.position.z);
		j.AddField (Rotation, playerTransform.rotation.eulerAngles.y);
		j.AddField (IsWalking, isWalking);
		return j;
	}
}









