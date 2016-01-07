using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour, GameManager {

	NetworkManager network;
	int friendliness;
	PokeDama pokeDama;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		friendliness = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pet() {
		friendliness++;
		Debug.Log (friendliness);
	}

	public void handleResponse(string data) {

	}
}
