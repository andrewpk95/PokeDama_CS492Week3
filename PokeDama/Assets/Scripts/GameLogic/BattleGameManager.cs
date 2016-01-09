using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleGameManager : MonoBehaviour, GameManager {

	public enum BattleState {
		Start,
		Battle,
		Wait,
		Faint,
		Result,
		End
	}

	NetworkManager network;
	BattleAnimationPlayer AnimationPlayer;
	BattleUIManager UI;
	PokeDamaManager pokeDamaManager;
	public GameObject opponentAI;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public BattleState battleState;
	public bool isPlayerTurn;
	public bool gameOver;

	// Use this for initialization
	void Start () {
		string imei = SystemInfo.deviceUniqueIdentifier;
		network = FindObjectOfType<NetworkManager> ();
		AnimationPlayer = FindObjectOfType<BattleAnimationPlayer> ();
		UI = FindObjectOfType<BattleUIManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		battleState = BattleState.Start;
		isPlayerTurn = true;
		gameOver = false;

		//Debug purposes
		network.RequestData(imei);

		pokeDamaManager.SaveOpPokeDama (new PokeDama (imei, 1, "opponent"));
		opPokeDama = pokeDamaManager.GetOpPokeDama ();
		pokeDamaManager.DisplayOpPokeDama (new Vector3(1, 3, 0));

		Instantiate (opponentAI, Vector3.zero, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Kick() { //Double-edged sword: Deals damage to both you and your opponent. 
		int opDamage = 0;
		int myDamage = 0;
		if (isPlayerTurn) {
			UI.SystemMessage (myPokeDama.name + " used Kick!");
			StartCoroutine (AnimationPlayer.PlayOnPlayerKick ());
			opDamage = 3000;
			myDamage = 1500;
		} else {
			UI.SystemMessage (opPokeDama.name + " used Kick!");
			StartCoroutine (AnimationPlayer.PlayOnOpponentKick ());
			opDamage = 1500;
			myDamage = 3000;
		}
		DamageOpponent (opDamage);
		DamageMine (myDamage);
		StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float) opPokeDama.health) / opPokeDama.maxHealth));
		StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth));
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void Throw() {
		int damage = 0;
		if (isPlayerTurn) {
			UI.SystemMessage (myPokeDama.name + " used Throw!");
			StartCoroutine (AnimationPlayer.PlayOnPlayerThrow ());
			damage = 1500;
			DamageOpponent (damage);
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float)opPokeDama.health) / opPokeDama.maxHealth));
		} else {
			UI.SystemMessage (opPokeDama.name + " used Throw!");
			StartCoroutine (AnimationPlayer.PlayOnOpponentThrow ());
			damage = 1500;
			DamageMine (damage);
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth));
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void Sleep() {
		int heal = 0;
		if (isPlayerTurn) {
			UI.SystemMessage (myPokeDama.name + " used Sleep!");
			StartCoroutine (AnimationPlayer.PlayOnPlayerSleep ());
			heal = -1000;
			DamageMine (heal);
			StartCoroutine(AnimationPlayer.PlayOnPlayerHeal (((float)myPokeDama.health) / myPokeDama.maxHealth));
		} else {
			UI.SystemMessage (opPokeDama.name + " used Sleep!");
			StartCoroutine (AnimationPlayer.PlayOnOpponentSleep ());
			heal = -1000;
			DamageOpponent (heal);
			StartCoroutine(AnimationPlayer.PlayOnOpponentHeal (((float)opPokeDama.health) / opPokeDama.maxHealth));
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void Run() {
		if (true) {
			Debug.Log ("Successfully ran away!");
			SceneManager.LoadScene ("MapScene");
		} else {
			Debug.Log ("Failed to run away...");
		}
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void DamageOpponent(int damage) {
		opPokeDama.damage (damage);
	}

	public void DamageMine(int damage) {
		myPokeDama.damage (damage);
	}

	void CheckDeath() {
		Debug.Log ("My PokeDama's Health: " + myPokeDama.health);
		Debug.Log ("Op PokeDama's Health: " + opPokeDama.health);
		if (opPokeDama.health <= 0) {
			if (myPokeDama.health <= 0) {
				onPlayerDraw (); //Both of their HP went below 0.
			} else {
				onPlayerWin (); //Only the opponent's HP went below 0. 
			}
		} else {
			if (myPokeDama.health <= 0) {
				onPlayerLose (); //Only the player's HP went below 0. 
			}
		}
	}

	void onPlayerWin() {
		Debug.Log ("You won!");
		//Do Something on Win
		gameOver = true;
		SceneManager.LoadScene ("MapScene");
	}

	void onPlayerLose() {
		Debug.Log ("You lost...");
		//Do Something on Lose
		gameOver = true;
		SceneManager.LoadScene ("PokeDamaScene");
	}

	void onPlayerDraw() {
		Debug.Log ("It's a draw!");
		//Do Something on Draw
		gameOver = true;
		SceneManager.LoadScene ("PokeDamaScene");
	}

	public void onForfeit() {
		Debug.Log ("It's a forfeit!");
		//Do something on Forfeit
		gameOver = true;
		SceneManager.LoadScene ("PokeDamaScene");
	}

	void Save(PokeDama myPokeDama) {
		network.RequestSave (myPokeDama);
	}

	public void handleResponse(string data) {

		JSONObject jsonData = new JSONObject (data);

		//Handling Server Response here
		if (jsonData.GetField ("ResponseType").str.Equals ("Save")) {
			bool successful = jsonData.GetField ("successful").b;
			Debug.Log (successful);
			if (successful) {
				Debug.Log ("Successfully saved your PokeDama!");
				string pokeDamaJSON = jsonData.GetField ("message").ToString();
				Debug.Log (pokeDamaJSON);
			} else {
				Debug.Log ("Failed to save your PokeDama...");
			}
		}

		//Handling Server Response here
		if (jsonData.GetField ("ResponseType").str.Equals ("FindByIMEI")) {
			bool successful = jsonData.GetField ("successful").b;
			Debug.Log (successful);
			if (successful) {
				Debug.Log ("Successfully found your PokeDama!");
				string pokeDamaJSON = jsonData.GetField ("message").ToString();
				Debug.Log (pokeDamaJSON);
				PokeDama inkachu = JsonUtility.FromJson<PokeDama> (pokeDamaJSON);

				pokeDamaManager.SaveMyPokeDama (inkachu);
				myPokeDama = pokeDamaManager.GetMyPokeDama ();
				pokeDamaManager.DisplayMyPokeDama (new Vector3(-1, 1, 0));
				AnimationPlayer.enabled = true;
				//SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				//SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
