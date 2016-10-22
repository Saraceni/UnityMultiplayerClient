using UnityEngine;
using System.Collections;
using SocketIO;

public class NetworkFollow : MonoBehaviour {

	public SocketIOComponent socket;
	
	public void OnFollow(string id) {
		// send position to node
		Debug.Log ("sending follow player id: " + Network.PlayerIdToJson(id));
		socket.Emit ("follow", new JSONObject(Network.PlayerIdToJson(id)));
	}
}
