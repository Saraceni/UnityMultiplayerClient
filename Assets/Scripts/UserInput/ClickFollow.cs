using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickFollow : MonoBehaviour, IClickable {

	public GameObject myPlayer;
	public NetworkEntity networkEntity;

	Targeter myPlayerTargeter;

	void Start() {
		networkEntity = GetComponent<NetworkEntity> ();
		myPlayerTargeter = myPlayer.GetComponent<Targeter> ();
	}

	public void OnClick (RaycastHit hit)
	{
		Network.Follow (networkEntity.id);
		myPlayerTargeter.target = transform;
	}
}
