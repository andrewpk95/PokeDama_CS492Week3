using UnityEngine;
using System.Collections;

public class ProfileUIManager : MonoBehaviour {

	ProfileGameManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<ProfileGameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPetButtonClick() {
		Debug.Log ("You just pet your PokeDama!");
		gameManager.Pet ();
	}

	public void OnMapButtonClick() {
		Debug.Log ("You will move to map scene...");
		Application.LoadLevel ("MapScene");
	}
}
