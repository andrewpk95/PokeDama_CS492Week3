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

	public static Vector3 playerPos;
	public static Vector3 opponentPos;
	public static Vector3 centerPos;

	NetworkManager network;
	BattleAnimationPlayer AnimationPlayer;
	BattleUIManager UI;
	AudioManager audio;
	PokeDamaManager pokeDamaManager;
	public GameObject opponentAI;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public BattleState battleState;
	public bool isPlayerTurn;
	public bool gameOver;

	bool safeToLoad = false;

	// Use this for initialization
	void Start () {
		
		//Define spawning position for PokeDama
		playerPos = new Vector3 (-1.5f, -0.0f, 0f);
		opponentPos = new Vector3 (1.5f, 2.5f, 0f);
		centerPos = Vector3.Lerp (playerPos, opponentPos, 0.5f);
		Debug.Log (centerPos);

		string imei = SystemInfo.deviceUniqueIdentifier;
		network = FindObjectOfType<NetworkManager> ();
		AnimationPlayer = FindObjectOfType<BattleAnimationPlayer> ();
		UI = FindObjectOfType<BattleUIManager> ();
		audio = FindObjectOfType<AudioManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		battleState = BattleState.Start;
		isPlayerTurn = true;
		gameOver = false;
		if (audio != null) {
			audio.PlayBattleMusic ();
		}
		//Debug purposes
		network.RequestData(imei);

		pokeDamaManager.SaveOpPokeDama (new PokeDama (imei, 2, "opponent"));
		opPokeDama = pokeDamaManager.GetOpPokeDama ();
		pokeDamaManager.DisplayOpPokeDama (opponentPos);

		Instantiate (opponentAI, Vector3.zero, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Kick() { //Double-edged sword: Deals damage to both you and your opponent. 
		int opDamage = 0;
		int myDamage = 0;
		if (isPlayerTurn) {
			StartCoroutine (UI.Mask ());
			opDamage = 3000;
			myDamage = 1500;
			DamageOpponent (opDamage);
			DamageMine (myDamage);
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " used Kick!"));
			StartCoroutine (AnimationPlayer.PlayOnPlayerKick ());
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float) opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " took recoil damage!"));
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
		} else {
			opDamage = 1500;
			myDamage = 3000;
			DamageMine (myDamage);
			DamageOpponent (opDamage);
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " used Kick!"));
			StartCoroutine (AnimationPlayer.PlayOnOpponentKick ());
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " took recoil damage!"));
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float) opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
		if (isPlayerTurn) {
			StartCoroutine (UI.unMask ());
		}
	}

	public void Lightning() { //Double-edged sword: Deals damage to both you and your opponent. 
		int opDamage = 0;
		int myDamage = 0;
		if (isPlayerTurn) {
			StartCoroutine (UI.Mask ());
			opDamage = 3000;
			myDamage = 1500;
			DamageOpponent (opDamage);
			DamageMine (myDamage);
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " used 110V!"));
			StartCoroutine (AnimationPlayer.PlayOnPlayer110V ());
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float) opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " took recoil damage!"));
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));

		} else {
			opDamage = 1500;
			myDamage = 3000;
			DamageMine (myDamage);
			DamageOpponent (opDamage);
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " used 110V!"));
			StartCoroutine (AnimationPlayer.PlayOnOpponent110V ());
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " took recoil damage!"));
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float) opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
		}

		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
		if (isPlayerTurn) {
			StartCoroutine (UI.unMask ());
		}
	}

	public void Throw() {
		int damage = 0;
		if (isPlayerTurn) {
			StartCoroutine (UI.Mask ());
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " used Throw!"));
			StartCoroutine (AnimationPlayer.PlayOnPlayerThrow ());
			damage = 1500;
			DamageOpponent (damage);
			StartCoroutine(AnimationPlayer.PlayOnOpponentDamaged (((float)opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
		} else {
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " used Throw!"));
			StartCoroutine (AnimationPlayer.PlayOnOpponentThrow ());
			damage = 1500;
			DamageMine (damage);
			StartCoroutine(AnimationPlayer.PlayOnPlayerDamaged (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
		if (isPlayerTurn) {
			StartCoroutine (UI.unMask ());
		}
	}

	public void Sleep() {
		int heal = 0;
		if (isPlayerTurn) {
			StartCoroutine (UI.Mask ());
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " used Sleep!"));
			StartCoroutine (AnimationPlayer.PlayOnPlayerSleep ());
			heal = -1000;
			DamageMine (heal);
			StartCoroutine(AnimationPlayer.PlayOnPlayerHeal (((float)myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
		} else {
			StartCoroutine (UI.SystemMessage (opPokeDama.name + " used Sleep!"));
			StartCoroutine (AnimationPlayer.PlayOnOpponentSleep ());
			heal = -1000;
			DamageOpponent (heal);
			StartCoroutine(AnimationPlayer.PlayOnOpponentHeal (((float)opPokeDama.health) / opPokeDama.maxHealth, opPokeDama.health));
		}
		CheckDeath ();
		isPlayerTurn = !isPlayerTurn; //Switch turn
		if (isPlayerTurn) {
			StartCoroutine (UI.unMask ());
		}
	}

	public void Run() {
		float random = Random.value;
		if (random < 0.7) {
			Debug.Log ("Successfully ran away!");
			gameOver = true;
			int reward = -50;
			myPokeDama.friendliness += reward;
			myPokeDama.recalculateStat ();
			Save (myPokeDama);
			if (audio != null) {
				StartCoroutine (AnimationPlayer.PlayDefeatMusic ());
			}
			StartCoroutine (UI.SystemMessage ("You successfully ran away."));
			StartCoroutine (UI.clickableMask ());
			StartCoroutine (UI.SystemMessage (myPokeDama.name + " lost " + reward + " friendliness..."));
			StartCoroutine (UI.clickableMask ());
			StartCoroutine (LoadScene ("MapScene"));
		} else {
			StartCoroutine (UI.SystemMessage ("You failed to run away..."));
			StartCoroutine (UI.clickableMask ());
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
		int reward = 200;
		myPokeDama.friendliness += reward;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		if (audio != null) {
			StartCoroutine (AnimationPlayer.PlayVictoryMusic ());
		}
		StartCoroutine (UI.SystemMessage ("You won!\n" + myPokeDama.name + " got " + reward + " friendliness!"));
		StartCoroutine (UI.clickableMask ());
		StartCoroutine (LoadScene ("MapScene"));
	}

	void onPlayerLose() {
		Debug.Log ("You lost...");
		//Do Something on Lose
		gameOver = true;
		int reward = -50;
		myPokeDama.friendliness += reward;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		if (audio != null) {
			StartCoroutine (AnimationPlayer.PlayDefeatMusic ());
		}
		StartCoroutine (UI.SystemMessage ("You lost...\n" + myPokeDama.name + " lost " + reward + " friendliness..."));
		StartCoroutine (UI.clickableMask ());
		StartCoroutine (LoadScene ("PokeDamaScene"));
	}

	void onPlayerDraw() {
		Debug.Log ("It's a draw!");
		//Do Something on Draw
		gameOver = true;
		int reward = 100;
		myPokeDama.friendliness += reward;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		if (audio != null) {
			StartCoroutine (AnimationPlayer.PlayDefeatMusic ());
		}
		StartCoroutine (UI.SystemMessage ("It's a draw!\n" + myPokeDama.name + " got " + reward + " friendliness!"));
		StartCoroutine (UI.clickableMask ());
		StartCoroutine (LoadScene ("PokeDamaScene"));
	}

	public void onForfeit() {
		Debug.Log ("It's a forfeit!");
		//Do something on Forfeit
		gameOver = true;
		int reward = -100;
		myPokeDama.friendliness += reward;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		if (audio != null) {
			StartCoroutine (AnimationPlayer.PlayDefeatMusic ());
		}
		StartCoroutine (UI.SystemMessage ("You forfeit the match...\n" + myPokeDama.name + " lost " + reward + " friendliness..."));
		StartCoroutine (UI.clickableMask ());
		StartCoroutine (LoadScene ("PokeDamaScene"));
	}

	IEnumerator LoadScene(string scene) {
		while (BattleAnimationPlayer.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		BattleAnimationPlayer.mutex = true;
		while (!safeToLoad) {
			yield return new WaitForEndOfFrame ();
		}
		SceneManager.LoadScene (scene);
		BattleAnimationPlayer.mutex = false;
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
				safeToLoad = true;
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

				inkachu.health = inkachu.maxHealth;
				pokeDamaManager.SaveMyPokeDama (inkachu);
				myPokeDama = pokeDamaManager.GetMyPokeDama ();
				pokeDamaManager.DisplayMyPokeDama (playerPos);
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
