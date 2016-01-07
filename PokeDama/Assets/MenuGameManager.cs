using UnityEngine;
using System.Collections;

public class MenuGameManager : MonoBehaviour {

	NetworkManager network;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();

		string imei = SystemInfo.deviceUniqueIdentifier;
		network.RequestData (imei);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
