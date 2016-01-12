using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour, GameManager {

	public static Vector3 spawnPos;

	public GameObject g_audio;
	public GameObject g_sound;
	public GameObject g_PokeDamaManager;

	NetworkManager network;
	ProfileAnimationPlayer AnimationPlayer;
	ProfileUIManager UI;
	AudioManager audio;
	SoundManager sound;
	PokeDamaManager pokeDamaManager;
	ShakeDetector shakeDetector;

	PokeDama myPokeDama;

	public bool safeToLoad = false;

	// Use this for initialization
	void Start () {
		//Define spawning position for PokeDama
		spawnPos = new Vector3(0f, 2f, -0.1f);

		string imei = SystemInfo.deviceUniqueIdentifier;
		network = FindObjectOfType<NetworkManager> ();
		AnimationPlayer = FindObjectOfType<ProfileAnimationPlayer> ();
		UI = FindObjectOfType<ProfileUIManager> ();
		audio = FindObjectOfType<AudioManager> ();
		sound = FindObjectOfType<SoundManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		shakeDetector = FindObjectOfType<ShakeDetector> ();

		//If necessary objects are not found in the scene, create it. 
		if (audio == null)
			audio = ((GameObject)Instantiate (g_audio, Vector3.zero, Quaternion.identity)).GetComponent<AudioManager> ();
		if (sound == null)
			sound = ((GameObject)Instantiate (g_sound, Vector3.zero, Quaternion.identity)).GetComponent<SoundManager> ();
		if (pokeDamaManager == null)
			pokeDamaManager = ((GameObject)Instantiate (g_PokeDamaManager, Vector3.zero, Quaternion.identity)).GetComponent<PokeDamaManager> ();

		audio.PlayMenuMusic ();

		//Debug purposes;
		network.RequestData(imei);
	}
	
	// Update is called once per frame
	void Update () {
        if (ShakeDetector.isShaked())
        {
            //Debug.Log("Update isShaked() is TRUE");
            Exercise();
        }
        //Debug.Log("Update isShaked() is false");
	}

	public void Pet() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just pet " + myPokeDama.name + "!", 3);
		myPokeDama.friendliness += 2;
		myPokeDama.recalculateStat ();
		safeToLoad = false;
		StartCoroutine (AnimationPlayer.PlayOnFriendliness (myPokeDama.friendliness));
		UI.UIUpdate ();
	}

	public void Exercise() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just exercised " + myPokeDama.name + "!", 3);
		myPokeDama.strength += 2;
		myPokeDama.recalculateStat ();
		safeToLoad = false;
		StartCoroutine (AnimationPlayer.PlayOnFriendliness (myPokeDama.friendliness));
		UI.UIUpdate ();
	}

	public void Feed() {
		UI.EraseAllMessage ();
		UI.SystemMessage ("You just fed " + myPokeDama.name + "!", 3);
		myPokeDama.damage (-1000);
		myPokeDama.recalculateStat ();
		safeToLoad = false;
		StartCoroutine (AnimationPlayer.PlayOnHeal (((float) myPokeDama.health) / myPokeDama.maxHealth, myPokeDama.health));
		UI.UIUpdate ();
	}

	public void Save() {
		myPokeDama.recalculateStat ();
		network.RequestSave (myPokeDama);
	}

	IEnumerator AutoSave(float seconds) {
		while (true) {
			yield return new WaitForSeconds (seconds);
			Save ();
		}
	}

	public void StopAutoSave() {
		StopCoroutine ("AutoSave");
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

				pokeDamaManager.SaveMyPokeDama (inkachu);
				myPokeDama = pokeDamaManager.GetMyPokeDama ();
				pokeDamaManager.DisplayMyPokeDama (spawnPos);

				AnimationPlayer.enabled = true;
				UI.enabled = true;
				StartCoroutine (AutoSave (5f));
				//SceneManager.LoadScene ("PokeDamaScene");
			} else {
				Debug.Log ("Failed to find your PokeDama...");
				Debug.Log ("Creating new PokeDama...");
				//SceneManager.LoadScene ("CreateScene");
			}
		}
	}
}
