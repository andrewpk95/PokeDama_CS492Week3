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

	GameManager gameManager;

	void Awake() {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();

		socket.On ("new message", NewMessage);
		socket.On ("ConnectionTest", NetTest);
		socket.On ("Response", NetResponse);
		socket.Connect ();

		if (socket.IsConnected) {
			Debug.Log ("Connected to Server!");
			StartCoroutine("ConnectionTest");
		} else {
			Debug.Log ("Not Connected...");
		}

	}

	public void NewMessage(SocketIOEvent socketEvent) {
		Debug.Log ("Response from server! " + socketEvent.data);
	}

	public void NetTest(SocketIOEvent socketEvent) {
		string data = socketEvent.data.ToString ();
		Debug.Log ("Response from server: " + data);
		StopCoroutine ("ConnectionTest");
	}

	public void NetResponse(SocketIOEvent socketEvent) {
		string data = socketEvent.data.ToString ();
		Debug.Log ("Response from server: " + data);
		gameManager.handleResponse (data);
	}

	/*
	private IEnumerator SendToServer(string ev, JSONObject json) {
		socket.Emit (ev, json);
		yield return null;
	}
	*/

	private IEnumerator ConnectionTest()
	{
		while (true) {
			Dictionary<string, string> data = new Dictionary<string, string> ();
			data ["node"] = "ConnectionTest";
			data ["message"] = "abcd";
			socket.Emit ("ConnectionTest", new JSONObject (data));
			Debug.Log ("ConnectionTest abcd");
			yield return new WaitForSeconds (1);
			yield return null;
		}
	}

	public void RequestSave(PokeDama pokedama) {
		string jsonString = JsonUtility.ToJson (pokedama);
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data ["RequestType"] = "Save";
		data ["PokeDama"] = jsonString;
		socket.Emit ("Request", new JSONObject (data));
	}

	public void RequestData(string IMEI) {
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data ["RequestType"] = "FindByIMEI";
		data ["IMEI"] = IMEI;
		socket.Emit ("Request", new JSONObject (data));
	}

	public void RequestData(GoogleMapLocation location) {

	}

	public void RequestCreation(PokeDama pokedama) {
		string jsonString = JsonUtility.ToJson (pokedama);
		Dictionary<string, string> data = new Dictionary<string, string> ();
		data ["RequestType"] = "Create";
		data ["PokeDama"] = jsonString;
		socket.Emit ("Request", new JSONObject (data));
	}
}
