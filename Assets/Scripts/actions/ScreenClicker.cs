﻿using UnityEngine;
using System.Collections;

public class ScreenClicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire2")) {
			Clicked ();
		}
	}

	void Clicked() {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (ray, out hit)) {

			var clickMove = hit.collider.gameObject.GetComponent<IClickable>();
			clickMove.OnClick(hit);
		}
	}
}
