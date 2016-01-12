using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreateGameManager : MonoBehaviour, GameManager {

	public GameObject g_audio;
	public GameObject g_sound;
	public GameObject g_PokeDamaManager;

	public static Vector3 inkachuPos;
	public static Vector3 zaraboogiPos;

	NetworkManager network;
	CreateUIManager UI;
	AudioManager audio;
	SoundManager sound;
	PokeDamaManager pokeDamaManager;
	// Use this for initialization
	void Start () {
		inkachuPos = new Vector3 (-1.45f, -0.8f);
		zaraboogiPos = new Vector3 (1.42f, -1.01f);

		network = FindObjectOfType<NetworkManager> ();
		UI = FindObjectOfType<CreateUIManager> ();
		audio = FindObjectOfType<AudioManager> ();
		sound = FindObjectOfType<SoundManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		//If necessary objects are not found in the scene, create it. 
		if (audio == null)
			audio = ((GameObject)Instantiate (g_audio, Vector3.zero, Quaternion.identity)).GetComponent<AudioManager> ();
		if (sound == null)
			sound = ((GameObject)Instantiate (g_sound, Vector3.zero, Quaternion.identity)).GetComponent<SoundManager> ();
		if (pokeDamaManager == null)
			pokeDamaManager = ((GameObject)Instantiate (g_PokeDamaManager, Vector3.zero, Quaternion.identity)).GetComponent<PokeDamaManager> ();

		audio.PlayMenuMusic ();
		pokeDamaManager.DisplayInkachu (inkachuPos);
		pokeDamaManager.DisplayZaraboogi (zaraboogiPos);

		UI.enabled = true;

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
