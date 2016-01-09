using UnityEngine;
using System.Collections;

public class BattleAnimationPlayer : MonoBehaviour {

	public static bool mutex = false;

	PokeDamaManager pokeDamaManager;

	public GameObject g_playerHealthBar;
	public GameObject g_opponentHealthBar;

	UIProgressBar playerHealthBar;
	UIProgressBar opponentHealthBar;

	public GameObject myPokeDama;
	public GameObject opPokeDama;

	// Use this for initialization
	void Start () {
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		playerHealthBar = g_playerHealthBar.GetComponent<UIProgressBar> ();
		opponentHealthBar = g_opponentHealthBar.GetComponent<UIProgressBar> ();

		myPokeDama = pokeDamaManager.myPicture;
		opPokeDama = pokeDamaManager.opPicture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator PlayOnPlayerKick() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		//Retract Animation
		for (int i = 0; i < 30; i++) {
			float angleStep = Mathf.Lerp (0f, 5f, 0.5f);
			myPokeDama.transform.Rotate(Vector3.forward, angleStep);

			yield return new WaitForEndOfFrame ();
		}
		//Kick Animation
		for (int i = 0; i < 10; i++) {
			float kickspeed = 10f;
			myPokeDama.transform.Translate (Time.deltaTime * kickspeed * (opPokeDama.transform.position - myPokeDama.transform.position));
			yield return new WaitForEndOfFrame ();
		}
		myPokeDama.transform.position = BattleGameManager.playerPos;
		myPokeDama.transform.rotation = Quaternion.identity;
		mutex = false;
	}

	public IEnumerator PlayOnOpponentKick() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;

		mutex = false;
	}

	public IEnumerator PlayOnPlayerThrow() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;

		mutex = false;
	}

	public IEnumerator PlayOnOpponentThrow() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;

		mutex = false;
	}

	public IEnumerator PlayOnPlayerSleep() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;

		mutex = false;
	}

	public IEnumerator PlayOnOpponentSleep() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;

		mutex = false;
	}

	public IEnumerator PlayOnPlayerDamaged(float value) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		//Blinking Animation
		for (int i = 0; i < 2; i++) {
			myPokeDama.SetActive (false);
			for (int j = 0; j < 10; j++) {
				yield return new WaitForEndOfFrame ();
			}
			myPokeDama.SetActive (true);
			for (int j = 0; j < 10; j++) {
				yield return new WaitForEndOfFrame ();
			}
		}
		//Animate Healthbar change
		while (playerHealthBar.value >= value) {
			playerHealthBar.value -= 0.01f;
			if (playerHealthBar.value > 1 || playerHealthBar.value < 0) { //Fail-Safe
				break;
			}
			yield return new WaitForEndOfFrame ();
		}
		mutex = false;
	}

	public IEnumerator PlayOnOpponentDamaged(float value) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		//Blinking Animation
		for (int i = 0; i < 2; i++) {
			opPokeDama.SetActive (false);
			for (int j = 0; j < 10; j++) {
				yield return new WaitForEndOfFrame ();
			}
			opPokeDama.SetActive (true);
			for (int j = 0; j < 10; j++) {
				yield return new WaitForEndOfFrame ();
			}
		}
		//Animate Healthbar change
		while (opponentHealthBar.value >= value) {
			opponentHealthBar.value -= 0.01f;
			if (opponentHealthBar.value > 1 || opponentHealthBar.value < 0) { //Fail-Safe
				break;
			}
			yield return new WaitForEndOfFrame ();
		}
		mutex = false;
	}

	public IEnumerator PlayOnPlayerHeal(float value) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		//Animate Healthbar change
		while (playerHealthBar.value <= value) {
			playerHealthBar.value += 0.01f;
			if (playerHealthBar.value > 1 || playerHealthBar.value < 0) { //Fail-Safe
				break;
			}
			yield return new WaitForEndOfFrame ();
		}
		mutex = false;
	}

	public IEnumerator PlayOnOpponentHeal(float value) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		//Animate Healthbar change
		while (opponentHealthBar.value <= value) {
			opponentHealthBar.value += 0.01f;
			if (opponentHealthBar.value > 1 || opponentHealthBar.value < 0) { //Fail-Safe
				break;
			}
			yield return new WaitForEndOfFrame ();
		}
		mutex = false;
	}
}
