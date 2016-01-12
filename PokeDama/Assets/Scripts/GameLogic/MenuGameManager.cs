using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuGameManager : MonoBehaviour, GameManager {

	public GameObject g_audio;
	public GameObject g_sound;
	public GameObject g_PokeDamaManager;

	NetworkManager network;
	AudioManager audio;
	SoundManager sound;
	PokeDamaManager pokeDamaManager;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		string imei = SystemInfo.deviceUniqueIdentifier;
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
		network.RequestData (imei);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void handleResponse(string data) {
		
		JSONObject jsonData = new JSONObject (data);

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
				SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
