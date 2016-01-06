using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	void Start() {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
	}

	/*
	NetworkClient client;

	// Use this for initialization
	void Start () {
		SetupClient ();
	}

	public void SetupClient()
	{
		client = new NetworkClient();
		client.RegisterHandler (MsgType.Connect, OnConnected);
		client.Connect("143.248.140.92", 1336);
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		var message = netMsg.ReadMessage<StringMessage> ();
		Debug.Log ("Received messege: " + message.value);

		//StartCoroutine (Listen (netMsg));
	}

	IEnumerator Listen(NetworkMessage netMsg) {
		while (true) {
			var message = netMsg.ReadMessage<StringMessage> ();
			Debug.Log ("Received messege: " + message.value);
			yield return null;
		}
	}
	*/
}
