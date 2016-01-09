using UnityEngine;
using System.Collections;

public class PokeDamaManager : MonoBehaviour {

	public bool dontDestroyOnLoad = false;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public GameObject id_1;
	public GameObject id_2;
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
		case 0:
			Instantiate (id_1, position, Quaternion.identity);
			break;
		case 1:
			Instantiate (id_2, position, Quaternion.identity);
			break;
		}

	}

	public void DisplayOpPokeDama(Vector3 position) {
		switch (opPokeDama.id) {
		case 0:
			Instantiate (id_1, position, Quaternion.identity);
			break;
		case 1:
			Instantiate (id_2, position, Quaternion.identity);
			break;
		}
	}

	public void SaveMyPokeDama(PokeDama pokeDama) {
		myPokeDama = pokeDama;
		Debug.Log ("Saved as: " + JsonUtility.ToJson (myPokeDama));
	}

	public void SaveOpPokeDama(PokeDama pokeDama) {
		opPokeDama = pokeDama;
		Debug.Log ("Saved as: " + JsonUtility.ToJson (myPokeDama));
	}

	public PokeDama GetMyPokeDama() {
		return myPokeDama;
	}

	public PokeDama GetOpPokeDama() {
		return opPokeDama;
	}
}
