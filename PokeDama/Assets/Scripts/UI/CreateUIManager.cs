using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreateUIManager : MonoBehaviour {
	
	NetworkManager network = FindObjectOfType<NetworkManager> ();
	public GameObject inputBox;
	UIInput uiInput;
	string text = "";
	int ID = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void getName(){
		uiInput = inputBox.GetComponent<UIInput> ();
		text = uiInput.label.text;
		print (text);

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
		SceneManager.LoadScene ("MenuScene");
	}

	public void chooseInka(){
		ID = 1;
		print ("you choose Inkachu");
	}

	public void chooseZara(){
		ID = 2;
		print ("you choose Zaraboogi");
	}

}
