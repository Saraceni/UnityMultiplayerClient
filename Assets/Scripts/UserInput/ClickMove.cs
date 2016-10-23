using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour, IClickable {

	public GameObject player;
	
	// Update is called once per frame
	public void OnClick (RaycastHit hit) {

		if (!player.GetComponent<Hittable> ().IsDead) {
			var navigator = player.GetComponent<Navigator> ();
			navigator.NavigateTo (hit.point);
			
			Network.Move (player.transform.position, hit.point);
		}
	}
}
