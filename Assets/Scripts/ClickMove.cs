using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour, IClickable {

	public GameObject player;
	
	// Update is called once per frame
	public void OnClick (RaycastHit hit) {
		var navigator = player.GetComponent<Navigator> ();
		var netMove = player.GetComponent<NetworkMove> ();
		navigator.NavigateTo (hit.point);

		netMove.OnMove (hit.point);
	}
}
