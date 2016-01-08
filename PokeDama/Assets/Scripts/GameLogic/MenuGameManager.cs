using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuGameManager : MonoBehaviour, GameManager {

	NetworkManager network;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		string imei = SystemInfo.deviceUniqueIdentifier;
		//Debug.Log (imei);

		network.RequestData (imei);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void handleResponse(string data) {
		
		JSONObject jsonData = new JSONObject (data);

		//Handling IMEI find Request here
		if (jsonData.GetField ("ResponseType").str.Equals ("FindByIMEI")) {
			bool successful = jsonData.GetField ("successful").b;
			Debug.Log (successful);
			if (successful) {
				Debug.Log ("Successfully found your PokeDama!");
				string pokeDamaJSON = jsonData.GetField ("message").ToString();
				Debug.Log (pokeDamaJSON);
				PokeDama inkachu = JsonUtility.FromJson<PokeDama> (pokeDamaJSON);
				PokeDamaManager pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
				pokeDamaManager.SaveMyPokeDama (inkachu);
				SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
