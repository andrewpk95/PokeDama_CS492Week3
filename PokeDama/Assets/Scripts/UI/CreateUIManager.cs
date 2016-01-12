using UnityEngine;
using System.Collections;

public class CreateUIManager : MonoBehaviour {
	
	NetworkManager network;
	SoundManager sound;
	public GameObject inputBox;
	UIInput uiInput;
	string text = "";
	int ID = 0;


	// Use this for initialization
	void Start () {
		network = FindObjectOfType<NetworkManager> ();
		sound = FindObjectOfType<SoundManager> ();
		uiInput = inputBox.GetComponent<UIInput> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void getName(){
		text = uiInput.label.text;
		print (text);
		StartCoroutine (sound.PlayOnTouch ());
	}


	public void OnCreateButtonClick() {
		if (text == "")
			return;
		if (ID == 0)
			return;
		print (text);
		print (ID);
		string imei = SystemInfo.deviceUniqueIdentifier;
		PokeDama yourPokeDama = new PokeDama (imei, ID, text);
		network.RequestCreation(yourPokeDama);
		StartCoroutine (sound.PlayOnTouch ());
	}

	public void chooseInka(){
		ID = 1;
		print ("you choose Inkachu");
		StartCoroutine (sound.PlayOnTouch ());
	}

	public void chooseZara(){
		ID = 2;
		print ("you choose Zaraboogi");
		StartCoroutine (sound.PlayOnTouch ());
	}

}
