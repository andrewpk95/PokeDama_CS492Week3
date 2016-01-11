using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PokeDamaManager : MonoBehaviour {

	public bool dontDestroyOnLoad = false;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public GameObject myPicture;
	public GameObject opPicture;

	public GameObject id_1;
	public GameObject id_2;

	public GameObject id_1_Battle;
	public GameObject id_2_Battle;

	public GameObject id_1_Opponent;
	public GameObject id_2_Opponent;
	// Use this for initialization
	void Start () {
		//Makes this object stay throughout the entire scene
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisplayMyPokeDama(Vector3 position) {
		switch (myPokeDama.id) {
		case 1:
			if (SceneManager.GetActiveScene ().name == "BattleScene") {
				myPicture = (GameObject)Instantiate (id_1_Battle, position, Quaternion.identity);
			} else {
				myPicture = (GameObject)Instantiate (id_1, position, Quaternion.identity);
			}
			break;
		case 2:
			if (SceneManager.GetActiveScene ().name == "BattleScene") {
				myPicture = (GameObject)Instantiate (id_2_Battle, position, Quaternion.identity);
			} else {
				myPicture = (GameObject)Instantiate (id_2, position, Quaternion.identity);
			}
			break;
		}

	}

	public void DisplayOpPokeDama(Vector3 position) {
		switch (opPokeDama.id) {
		case 1:
			opPicture = (GameObject) Instantiate (id_1_Opponent, position, Quaternion.identity);
			break;
		case 2:
			opPicture = (GameObject) Instantiate (id_2_Opponent, position, Quaternion.identity);
			break;
		}
	}

	public void SaveMyPokeDama(PokeDama pokeDama) {
		myPokeDama = pokeDama;
		Debug.Log ("Saved as: " + JsonUtility.ToJson (myPokeDama));
	}

	public void SaveOpPokeDama(PokeDama pokeDama) {
		opPokeDama = pokeDama;
		Debug.Log ("Saved as: " + JsonUtility.ToJson (opPokeDama));
	}

	public PokeDama GetMyPokeDama() {
		return myPokeDama;
	}

	public PokeDama GetOpPokeDama() {
		return opPokeDama;
	}

	public bool isMyPokeDamaLoaded() {
		return myPokeDama != null;
	}

	public bool isOpPokeDamaLoaded() {
		return opPokeDama != null;
	}
}
