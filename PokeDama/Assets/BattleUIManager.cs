﻿using UnityEngine;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRunButtonClick() {
		Debug.Log ("Moving to Map Scene...");
		Application.LoadLevel ("MapScene");
	}
}
