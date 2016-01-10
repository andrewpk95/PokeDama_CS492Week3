using UnityEngine;
using System.Collections;

public class BattleAIScript : MonoBehaviour {

	BattleGameManager gameManager;
	PokeDamaManager pokeDamaManager;

	bool playedTurn;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<BattleGameManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		playedTurn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isPlayerTurn && !playedTurn && !gameManager.gameOver) {
			playedTurn = true;
			playAI ();
		}
	}

	//This is the main AI code. 
	void playAI() {
		int random = (int) Random.Range (0, 3);
		Debug.Log (random);
		if (pokeDamaManager.GetOpPokeDama ().id == 1) {
			switch (random) {
			case 0:
				On110V ();
				break;
			case 1:
				OnThrow ();
				break;
			case 2:
				OnSleep ();
				break;
			default: 
				On110V ();
				break;
			}
		} else if (pokeDamaManager.GetOpPokeDama ().id == 2) {
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
		}
		playedTurn = false;
	}

	void OnKick() {
		Debug.Log ("Opponent pressed Kick command!");
		gameManager.Kick ();
	}

	void On110V() {
		Debug.Log ("Opponent pressed 110V command!");
		gameManager.Lightning ();
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
