using UnityEngine;
using System.Collections;

public class BattleAIScript : MonoBehaviour {

	BattleGameManager gameManager;

	bool playedTurn;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<BattleGameManager> ();

		playedTurn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isPlayerTurn && !playedTurn) {
			playedTurn = true;
			playAI ();
		}
	}

	//This is the main AI code. 
	void playAI() {
		int random = (int) Random.Range (0, 3);
		Debug.Log (random);
		switch (random) {
		case 0:
			OnKick ();
			break;
		case 1:
			OnThrow ();
			break;
		case 2:
			OnSleep ();
			break;
		default: 
			OnKick ();
			break;
		}
		playedTurn = false;
	}

	void OnKick() {
		Debug.Log ("Opponent pressed Kick command!");
		gameManager.Kick ();
	}

	void OnThrow() {
		Debug.Log ("Opponent pressed Throw command!");
		gameManager.Throw ();
	}

	void OnSleep() {
		Debug.Log ("Opponent pressed Sleep command!");
		gameManager.Sleep ();
	}
}
