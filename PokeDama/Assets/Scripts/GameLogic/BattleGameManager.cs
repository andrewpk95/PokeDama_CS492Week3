using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleGameManager : MonoBehaviour, GameManager {

	NetworkManager network;
	PokeDamaManager pokeDamaManager;
	public GameObject opponent;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public bool isPlayerTurn;

	// Use this for initialization
	void Start () {
		string imei = SystemInfo.deviceUniqueIdentifier;

		network = FindObjectOfType<NetworkManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		//Debug purposes
		network.RequestData(imei);

		pokeDamaManager.SaveOpPokeDama (new PokeDama (imei, 1, "opponent"));
		opPokeDama = pokeDamaManager.GetOpPokeDama ();
		pokeDamaManager.DisplayOpPokeDama (new Vector3(1, 3, 0));

		isPlayerTurn = true;

		Instantiate (opponent, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Kick() { //Double-edged sword: Deals damage to both you and your opponent. 
		int opDamage = 0;
		int myDamage = 0;
		if (isPlayerTurn) {
			opDamage = 2500;
			myDamage = 500;
		} else {
			opDamage = 500;
			myDamage = 2500;
		}
		DamageOpponent (opDamage);
		DamageMine (myDamage);
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void Throw() {
		int damage = 0;
		if (isPlayerTurn) {
			damage = 1000;
			DamageOpponent (damage);
		} else {
			damage = 1000;
			DamageMine (damage);
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
	}

	public void Sleep() {
		int heal = 0;
		if (isPlayerTurn) {
			heal = -1500;
			DamageMine (heal);
		} else {
			heal = -1500;
			DamageOpponent (heal);
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
		SceneManager.LoadScene ("MapScene");
	}

	void onPlayerLose() {
		Debug.Log ("You lost...");
		//Do Something on Lose
		SceneManager.LoadScene ("PokeDamaScene");
	}

	void onPlayerDraw() {
		Debug.Log ("It's a draw!");
		//Do Something on Draw
		SceneManager.LoadScene ("PokeDamaScene");
	}

	public void onForfeit() {
		Debug.Log ("It's a forfeit!");
		//Do something on Forfeit
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
				//SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				//SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
