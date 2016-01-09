using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour, GameManager {

	NetworkManager network;
	PokeDamaManager pokeDamaManager;
	ShakeDetector shakeDetector;

	PokeDama myPokeDama;

	// Use this for initialization
	void Start () {
		string imei = SystemInfo.deviceUniqueIdentifier;

		network = FindObjectOfType<NetworkManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		shakeDetector = FindObjectOfType<ShakeDetector> ();

		//Debug purposes
		//pokeDamaManager.SaveMyPokeDama (new PokeDama (imei, 1, "inkachu"));
		network.RequestData(imei);
		//myPokeDama = pokeDamaManager.GetMyPokeDama ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeDetector.isShaked ()) {
			Debug.Log ("Device Shaken!");
			Exercise ();
		}
	}

	public void Pet() {
		myPokeDama.friendliness += 2;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
	}

	public void Exercise() {
		myPokeDama.friendliness += 10;
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
	}

	public void Feed() {
		myPokeDama.damage (-1000);
		myPokeDama.recalculateStat ();
		Save (myPokeDama);
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
				pokeDamaManager.DisplayMyPokeDama (new Vector3(0, 2, 0));
				//SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				//SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
