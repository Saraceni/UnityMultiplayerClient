using UnityEngine;
using System.Collections;

public class ClickFollow : MonoBehaviour, IClickable {

	public Follower myPlayerFollower;
	public NetworkEntity networkEntity;

	void Start() {
		networkEntity = GetComponent<NetworkEntity> ();
	}

	public void OnClick (RaycastHit hit)
	{
		GetComponent<NetworkFollow> ().OnFollow (networkEntity.id);
		myPlayerFollower.target = transform;
	}
}
