using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour, GameManager {

	NetworkManager network;
	PokeDamaManager pokeDamaManager;

	PokeDama myPokeDama;
	int friendliness;

	// Use this for initialization
	void Start () {
		string imei = SystemInfo.deviceUniqueIdentifier;

		network = FindObjectOfType<NetworkManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		//Debug purposes
		pokeDamaManager.SaveMyPokeDama (new PokeDama (imei, 1, "inkachu"));
		myPokeDama = pokeDamaManager.GetMyPokeDama ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pet() {
		friendliness++;
		myPokeDama.friendliness = myPokeDama.friendliness + 1;
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
	}
}
