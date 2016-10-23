using UnityEngine;
using System.Collections;

public class ScreenClicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Debug.Log ("Received Input");
			Clicked ();
		}
	}

	void Clicked() {

		var touch = Input.GetTouch (0);

		var ray = Camera.main.ScreenPointToRay (touch.position);
		RaycastHit hit = new RaycastHit ();
		
		if (Physics.Raycast (ray, out hit)) {

			Debug.Log (hit.collider.gameObject.name);
			var clickMove = hit.collider.gameObject.GetComponent<IClickable>();
			clickMove.OnClick(hit);
		}
	}

}
