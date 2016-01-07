using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour {

	NetworkManager network;
	int friendliness;
	PokeDama pokeDama;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		friendliness = 0;
		pokeDama = new PokeDama (1111, 1);
		string jsonString = JsonUtility.ToJson (pokeDama);
		JSONObject json = new JSONObject (jsonString);
		Debug.Log(json.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pet() {
		friendliness++;
		Debug.Log (friendliness);
	}
}
