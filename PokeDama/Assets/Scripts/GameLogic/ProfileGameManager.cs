using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour, GameManager {

	public static Vector3 spawnPos;

	NetworkManager network;
	ProfileAnimationPlayer AnimationPlayer;
	ProfileUIManager UI;
	AudioManager audio;
	PokeDamaManager pokeDamaManager;
	ShakeDetector shakeDetector;

	PokeDama myPokeDama;

	// Use this for initialization
	void Start () {
		//Define spawning position for PokeDama
		spawnPos = new Vector3(0f, 2f, 0f);

		string imei = SystemInfo.deviceUniqueIdentifier;
		network = FindObjectOfType<NetworkManager> ();
		AnimationPlayer = FindObjectOfType<ProfileAnimationPlayer> ();
		UI = FindObjectOfType<ProfileUIManager> ();
		audio = FindObjectOfType<AudioManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		shakeDetector = FindObjectOfType<ShakeDetector> ();

		if (audio != null) {
			audio.PlayMenuMusic ();
		}

		//Debug purposes;
		network.RequestData(imei);
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeDetector.isShaked ()) {
			Debug.Log ("Device Shaken!");
			Exercise ();
		}
	}

	public void Pet() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just pet " + myPokeDama.name + "!", 3);
		myPokeDama.friendliness += 2;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		StartCoroutine (AnimationPlayer.PlayOnFriendliness (myPokeDama.friendliness));
		UI.UIUpdate ();
	}

	public void Exercise() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just exercised " + myPokeDama.name + "!", 3);
		myPokeDama.friendliness += 10;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		StartCoroutine (AnimationPlayer.PlayOnFriendliness (myPokeDama.friendliness));
		UI.UIUpdate ();
	}

	public void Feed() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just fed " + myPokeDama.name + "!", 3);
		myPokeDama.damage (-1000);
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
		StartCoroutine (AnimationPlayer.PlayOnHeal (((float) myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
		UI.UIUpdate ();
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
				pokeDamaManager.DisplayMyPokeDama (spawnPos);

				AnimationPlayer.enabled = true;
				UI.enabled = true;
				//SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				//SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
