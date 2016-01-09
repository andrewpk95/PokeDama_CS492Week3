using UnityEngine;
using System.Collections;

public class PokeDamaManager : MonoBehaviour {

	public bool dontDestroyOnLoad = false;

	PokeDama myPokeDama;
	PokeDama opPokeDama;

	public GameObject myPicture;
	public GameObject opPicture;

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
			myPicture = (GameObject) Instantiate (id_1, position, Quaternion.identity);
			break;
		case 1:
			myPicture = (GameObject) Instantiate (id_2, position, Quaternion.identity);
			break;
		}

	}

	public void DisplayOpPokeDama(Vector3 position) {
		switch (opPokeDama.id) {
		case 0:
			opPicture = (GameObject) Instantiate (id_1, position, Quaternion.identity);
			break;
		case 1:
			opPicture = (GameObject) Instantiate (id_2, position, Quaternion.identity);
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
}
