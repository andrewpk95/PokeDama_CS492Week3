using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool dontDestroyOnLoad;

	//Music Prefabs
	public GameObject MenuMusic;
	public GameObject BattleMusic;
	public GameObject VictoryMusic;
	public GameObject DefeatMusic;

	public GameObject currentMusic;

	// Use this for initialization
	void Start () {
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayMenuMusic() {
		if (currentMusic != null) {
			if (currentMusic.name != "MenuMusic(Clone)") {
				Destroy (currentMusic);
				currentMusic = (GameObject)Instantiate (MenuMusic, Vector3.zero, Quaternion.identity);
			}
		} else {
			currentMusic = (GameObject)Instantiate (MenuMusic, Vector3.zero, Quaternion.identity);
		}
	}

	public void PlayBattleMusic() {
		if (currentMusic != null) {
			if (currentMusic.name != "BattleMusic(Clone)") {
				Destroy (currentMusic);
				currentMusic = (GameObject)Instantiate (BattleMusic, Vector3.zero, Quaternion.identity);
			}
		} else {
			currentMusic = (GameObject)Instantiate (BattleMusic, Vector3.zero, Quaternion.identity);
		}
	}

	public void PlayVictoryMusic() {
		if (currentMusic != null) {
			if (currentMusic.name != "VictoryMusic(Clone)") {
				Destroy (currentMusic);
				currentMusic = (GameObject)Instantiate (VictoryMusic, Vector3.zero, Quaternion.identity);
			}
		} else {
			currentMusic = (GameObject)Instantiate (VictoryMusic, Vector3.zero, Quaternion.identity);
		}
	}

	public void PlayDefeatMusic() {
		if (currentMusic != null) {
			if (currentMusic.name != "DefeatMusic(Clone)") {
				Destroy (currentMusic);
				currentMusic = (GameObject)Instantiate (DefeatMusic, Vector3.zero, Quaternion.identity);
			}
		} else {
			currentMusic = (GameObject)Instantiate (DefeatMusic, Vector3.zero, Quaternion.identity);
		}
	}

	public void Stop() {
		if (currentMusic != null) {
			Destroy (currentMusic);
		}
	}
}
