using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	BattleGameManager gameManager;
	BattleAnimationPlayer anim;
	SoundManager sound;
	PokeDamaManager pokeDamaManager;

	//UI GameObjects
	public GameObject g_label;
	public GameObject g_mask;
	public GameObject g_clickMask;
	public GameObject g_playerNameLabel;
	public GameObject g_opponentNameLabel;
	public GameObject g_playerLevelLabel;
	public GameObject g_opponentLevelLabel;
	public GameObject g_playerHPLabel;
	public GameObject g_opponentHPLabel;

	public GameObject KickButton;
	public GameObject LightningButton;
	public GameObject ThrowButton;
	public GameObject SpitButton;
	public GameObject SleepButton;
	public GameObject RunButton;

	//UI Objects to manipulate
	UILabel textBox;
	UILabel playerNameText;
	UILabel opponentNameText;
	UILabel playerLevelText;
	UILabel opponentLevelText;
	UILabel playerHPText;
	UILabel opponentHPText;
	UISprite mask;
	UISprite clickMask;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<BattleGameManager> ();
		anim = FindObjectOfType<BattleAnimationPlayer> ();
		sound = FindObjectOfType<SoundManager> ();
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		//Loading PokeDama information to UI
		textBox = g_label.GetComponent<UILabel> ();
		playerNameText = g_playerNameLabel.GetComponent<UILabel> ();
		opponentNameText = g_opponentNameLabel.GetComponent<UILabel> ();
		playerLevelText = g_playerLevelLabel.GetComponent<UILabel> ();
		opponentLevelText = g_opponentLevelLabel.GetComponent<UILabel> ();
		playerHPText = g_playerHPLabel.GetComponent<UILabel> ();
		opponentHPText = g_opponentHPLabel.GetComponent<UILabel> ();
		mask = g_mask.GetComponent<UISprite> ();
		clickMask = g_clickMask.GetComponent<UISprite> ();
		StartCoroutine (LoadText ());

		//Loading Buttons
		StartCoroutine(LoadButton());
	}

	IEnumerator LoadText() {
		while (!pokeDamaManager.isMyPokeDamaLoaded () || !pokeDamaManager.isOpPokeDamaLoaded()) {
			yield return null;
		}
		PokeDama myPokeDama = pokeDamaManager.GetMyPokeDama ();
		PokeDama opPokeDama = pokeDamaManager.GetOpPokeDama ();
		playerNameText.text = myPokeDama.name.ToString();
		opponentNameText.text = opPokeDama.name.ToString();
		playerLevelText.text += myPokeDama.level.ToString();
		opponentLevelText.text += opPokeDama.level.ToString();
		playerHPText.text = myPokeDama.health.ToString() + "/" + myPokeDama.maxHealth.ToString();
		opponentHPText.text = opPokeDama.health.ToString() + "/" + opPokeDama.maxHealth.ToString();

		textBox.text = "What will " + myPokeDama.name + " do?";
	}

	IEnumerator LoadButton() {
		while (!pokeDamaManager.isMyPokeDamaLoaded () || !pokeDamaManager.isOpPokeDamaLoaded()) {
			yield return null;
		}
		if (pokeDamaManager.GetMyPokeDama ().id == 1) {
			LightningButton.SetActive (true);
			ThrowButton.SetActive (true);
			SleepButton.SetActive (true);
		} else if (pokeDamaManager.GetMyPokeDama ().id == 2) {
			KickButton.SetActive (true);
			SpitButton.SetActive (true);
			SleepButton.SetActive (true);
		}
		RunButton.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}

	public void OnKickButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Kick command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Kick ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void On110VButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed 110V command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Lightning ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnThrowButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Throw command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Throw ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnSpitButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Spit command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Spit ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnSleepButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Sleep command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Sleep ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnRunButtonClick() {
		if (gameManager.isPlayerTurn) {
			Debug.Log ("You pressed Run command!");
			StartCoroutine (sound.PlayOnTouch ());
			gameManager.Run ();
		} else {
			Debug.Log ("It's not your turn yet!");
		}
	}

	public void OnMaskClick() {
		clickMask.depth = -20;
		StartCoroutine (sound.PlayOnTouch ());
	}

	void OnEscapeKeyPress() {
		if (gameManager.gameOver) {
			Application.Quit ();
		} else {
			Debug.Log ("You forfeit battle!");
			gameManager.onForfeit ();
		}
	}

	public IEnumerator SystemMessage(string text) {
		while (anim.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("System Message Start");
		anim.mutex = true;
		textBox.text = text;
		anim.mutex = false;
		Debug.Log ("System Message Done");
	}

	public IEnumerator Mask() {
		Debug.Log ("Mask");
		while (anim.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Mask Start");
		anim.mutex = true;
		mask.depth = 10;
		anim.mutex = false;
		Debug.Log ("Mask End");
	}

	public IEnumerator unMask() {
		while (anim.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Unmask Start");
		anim.mutex = true;
		mask.depth = -10;
		anim.mutex = false;
		Debug.Log ("Unmask End");
	}
		
	public IEnumerator clickableMask() {
		while (anim.mutex) {
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Click Mask Start");
		anim.mutex = true;
		clickMask.depth = 20;
		while (clickMask.depth > 0) {
			yield return new WaitForEndOfFrame ();
		}
		anim.mutex = false;
		Debug.Log ("Click Mask End");
	}
}
