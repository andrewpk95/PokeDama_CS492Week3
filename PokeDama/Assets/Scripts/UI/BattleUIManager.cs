﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	BattleGameManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<BattleGameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}

	public void OnKickButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Kick command!");
			gameManager.Kick ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnThrowButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Throw command!");
			gameManager.Throw ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnSleepButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Sleep command!");
			gameManager.Sleep ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnRunButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Run command!");
			gameManager.Run ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	void OnEscapeKeyPress() {
		Debug.Log ("You forfeit battle!");
		gameManager.onForfeit ();
	}
}
