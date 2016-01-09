﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	BattleGameManager gameManager;
	PokeDamaManager pokeDamaManager;

	//UI GameObjects
	public GameObject g_label;
	public GameObject g_mask;
	public GameObject g_playerNameLabel;
	public GameObject g_opponentNameLabel;
	public GameObject g_playerLevelLabel;
	public GameObject g_opponentLevelLabel;
	public GameObject g_playerHPLabel;
	public GameObject g_opponentHPLabel;

	//UI Objects to manipulate
	UILabel textBox;
	UILabel playerNameText;
	UILabel opponentNameText;
	UILabel playerLevelText;
	UILabel opponentLevelText;
	UILabel playerHPText;
	UILabel opponentHPText;
	UISprite mask;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<BattleGameManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		textBox = g_label.GetComponent<UILabel> ();
		playerNameText = g_playerNameLabel.GetComponent<UILabel> ();
		opponentNameText = g_opponentNameLabel.GetComponent<UILabel> ();
		playerLevelText = g_playerLevelLabel.GetComponent<UILabel> ();
		opponentLevelText = g_opponentLevelLabel.GetComponent<UILabel> ();
		playerHPText = g_playerHPLabel.GetComponent<UILabel> ();
		opponentHPText = g_opponentHPLabel.GetComponent<UILabel> ();
		mask = g_mask.GetComponent<UISprite> ();
		StartCoroutine (LoadText ());
	}

	IEnumerator LoadText() {
		while (!pokeDamaManager.isMyPokeDamaLoaded () || !pokeDamaManager.isOpPokeDamaLoaded()) {
			yield return null;
		}
		PokeDama myPokeDama = pokeDamaManager.GetMyPokeDama ();
		PokeDama opPokeDama = pokeDamaManager.GetOpPokeDama ();
		playerNameText.text = myPokeDama.name.ToString();
		opponentNameText.text = opPokeDama.name.ToString();
		playerLevelText.text += myPokeDama.level.ToString();
		opponentLevelText.text += opPokeDama.level.ToString();
		playerHPText.text = myPokeDama.health.ToString() + "/" + myPokeDama.maxHealth.ToString();
		opponentHPText.text = opPokeDama.health.ToString() + "/" + opPokeDama.maxHealth.ToString();
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

	public void OnMaskClick() {
		//mask.depth = -10;
	}

	void OnEscapeKeyPress() {
		Debug.Log ("You forfeit battle!");
		gameManager.onForfeit ();
	}

	public IEnumerator SystemMessage(string text) {
		while (BattleAnimationPlayer.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		BattleAnimationPlayer.mutex = true;
		textBox.text = text;
		BattleAnimationPlayer.mutex = false;
	}

	public IEnumerator Mask() {
		while (BattleAnimationPlayer.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		BattleAnimationPlayer.mutex = true;
		mask.depth = 10;
		BattleAnimationPlayer.mutex = false;
	}

	public IEnumerator unMask() {
		while (BattleAnimationPlayer.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		BattleAnimationPlayer.mutex = true;
		mask.depth = -10;
		BattleAnimationPlayer.mutex = false;
	}
}
