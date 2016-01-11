using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreateGameManager : MonoBehaviour, GameManager {

	NetworkManager network;
	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		string imei = SystemInfo.deviceUniqueIdentifier;
		//Debug.Log (imei);

		//Use network.RequestCreation(yourPokeDama);
		//yourPokeDama is of Type PokeDama (Look at PokeDama.cs for more information).
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void handleResponse(string data) {

		JSONObject jsonData = new JSONObject (data);

		//Handling Create
		if (jsonData.GetField ("ResponseType").str.Equals ("Create")) {
			bool successful = jsonData.GetField ("successful").b;
			Debug.Log (successful);
			if (successful) {
				Debug.Log ("Successfully made PokeDama!");
				string pokeDamaJSON = jsonData.GetField ("message").str;
				PokeDama pd = JsonUtility.FromJson<PokeDama>(pokeDamaJSON);
				Debug.Log (pokeDamaJSON);
				SceneManager.LoadScene ("MenuScene");
			} else {
				Debug.Log ("Failed to create your PokeDama...");
				Debug.Log ("...?");
			}

		}
	}
}
