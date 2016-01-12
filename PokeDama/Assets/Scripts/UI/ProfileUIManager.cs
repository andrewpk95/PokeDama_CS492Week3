using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProfileUIManager : MonoBehaviour {

	ProfileGameManager gameManager;
	AudioManager audio;
	PokeDamaManager pokeDamaManager;

	//UI GameObjects
	public GameObject g_Label;
	public GameObject g_nameLabel;
	public GameObject g_lvLabel;

	public GameObject MapButton;
	public GameObject PetButton;
	public GameObject FeedButton;

	//UI Objects to manipulate
	UILabel textBox;
	UILabel nameText;
	UILabel lvText;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<ProfileGameManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		audio = FindObjectOfType<AudioManager> ();

		//Loading PokeDama information to UI
		textBox = g_Label.GetComponent<UILabel> ();
		nameText = g_nameLabel.GetComponent<UILabel> ();
		lvText = g_lvLabel.GetComponent<UILabel> ();
		StartCoroutine (LoadText ());

		//Loading Buttons
		StartCoroutine (LoadButton ());
	}

	IEnumerator LoadText() {
		while (!pokeDamaManager.isMyPokeDamaLoaded ()) {
			yield return null;
		}
		UIUpdate ();
	}

	IEnumerator LoadButton() {
		while (!pokeDamaManager.isMyPokeDamaLoaded ()) {
			yield return null;
		}
		MapButton.SetActive (true);
		PetButton.SetActive (true);
		FeedButton.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}

	public void UIUpdate() {
		PokeDama myPokeDama = pokeDamaManager.GetMyPokeDama ();
		nameText.text = "Name: " + myPokeDama.name;
		lvText.text = "Lv. " + myPokeDama.level.ToString();
	}

	public void OnDebugBattleClick() {
		audio.Stop ();
		StartCoroutine (LoadScene ("BattleScene"));
	}

	public void OnPetButtonClick() {
		Debug.Log ("You just pet your PokeDama!");
		gameManager.Pet ();
		UIUpdate ();
	}

	public void OnMapButtonClick() {
		Debug.Log ("You will move to map scene...");
		audio.Stop ();
		StartCoroutine (LoadScene ("MapScene"));;
	}

	public void OnFeedButtonClick() {
		Debug.Log ("You just fed your PokeDama!");
		gameManager.Feed ();
		UIUpdate ();
	}

	void OnEscapeKeyPress() {
		Debug.Log ("Quitting...");
		StartCoroutine (QuitApp ());
	}

	public void SystemMessage(string text, float seconds) {
		StartCoroutine (DisplayMessage (text, seconds));
	}

	public IEnumerator DisplayMessage(string text, float seconds) {
		textBox.text = text;
		yield return new WaitForSeconds (seconds);
		textBox.text = "";
	}

	public void EraseAllMessage() {
		StopAllCoroutines ();
	}

	IEnumerator LoadScene(string scene) {
		gameManager.Save ();
		while (!gameManager.safeToLoad) {
			yield return new WaitForEndOfFrame ();
		}
		gameManager.StopAutoSave ();
		SceneManager.LoadScene (scene);
	}

	IEnumerator QuitApp() {
		gameManager.Save ();
		while (!gameManager.safeToLoad) {
			yield return new WaitForEndOfFrame ();
		}
		gameManager.StopAutoSave ();
		Application.Quit();
	}
}
