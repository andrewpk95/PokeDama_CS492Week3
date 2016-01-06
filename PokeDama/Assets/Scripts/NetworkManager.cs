using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
/*
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
*/

public class NetworkManager : MonoBehaviour {

	private SocketIOComponent socket;

	void Start() {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();



		if (socket.IsConnected) {
			Debug.Log ("Connected to Server!");
			StartCoroutine("BeepBoop");
		} else {
			Debug.Log ("Not Connected...");
		}

	}

	private IEnumerator BeepBoop()
	{
		int i = 0;
		while (true) {
			// wait 1 seconds and continue
			yield return new WaitForSeconds (1);
			i++;
			Dictionary<string, string> data = new Dictionary<string, string> ();
			data ["node"] = "node";
			data ["age"] = i.ToString();

			socket.Emit ("new message", new JSONObject (data));
			Debug.Log ("Beep");

			// wait 3 seconds and continue
			yield return new WaitForSeconds (3);
		}
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
