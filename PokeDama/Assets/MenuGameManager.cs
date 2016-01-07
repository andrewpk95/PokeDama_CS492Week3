using UnityEngine;
using System.Collections;

public class MenuGameManager : MonoBehaviour, GameManager {

	NetworkManager network;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		string imei = SystemInfo.deviceUniqueIdentifier;
		//Debug.Log (imei);

		PokeDama inkachu = new PokeDama (imei, 1);


		//network.RequestData (imei);
		network.RequestCreation(inkachu);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void handleResponse(string data) {
		
		JSONObject jsonData = new JSONObject (data);
		/*
		bool successful = jsonData.GetField ("successful").b;
		Debug.Log(successful);
		if (successful) {
			Debug.Log ("Successfully found your PokeDama!");
			string pokeDamaJSON = jsonData.GetField ("message").ToString();
			Debug.Log (pokeDamaJSON);
		} else {
			Debug.Log ("Failed to find your PokeDama...");
			Debug.Log ("Creating new PokeDama...");

		}
		*/
		if (jsonData.GetField ("ResponseType").str.Equals ("Create")) {
			Debug.Log ("Successfully made inkachu!");
			Debug.Log (jsonData.GetField ("message").str);
		}
	}
}
