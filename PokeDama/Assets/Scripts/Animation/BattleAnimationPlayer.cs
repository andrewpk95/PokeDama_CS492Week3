using UnityEngine;
using System.Collections;

public class BattleAnimationPlayer : MonoBehaviour {

	public static bool mutex = false;

	PokeDamaManager pokeDamaManager;
	AudioManager audio;
	SoundManager sound;

	public GameObject g_playerHealthBar;
	public GameObject g_opponentHealthBar;

	public GameObject g_playerHPText;
	public GameObject g_opponentHPText;

	public GameObject g_110V;
	public GameObject g_lightning;
	public GameObject g_soju;
	public GameObject g_zzz;
	public GameObject g_spit;
	public GameObject HealParticle;

	UIProgressBar playerHealthBar;
	UIProgressBar opponentHealthBar;
	UILabel playerHPText;
	UILabel opponentHPText;

	public GameObject myPokeDama;
	public GameObject opPokeDama;

	// Use this for initialization
	void Start () {
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		audio = FindObjectOfType<AudioManager> ();
		sound = FindObjectOfType<SoundManager> ();

		playerHealthBar = g_playerHealthBar.GetComponent<UIProgressBar> ();
		opponentHealthBar = g_opponentHealthBar.GetComponent<UIProgressBar> ();
		playerHPText = g_playerHPText.GetComponent<UILabel> ();
		opponentHPText = g_opponentHPText.GetComponent<UILabel> ();

		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		playerHealthBar.value = ((float) pk.health) / pk.maxHealth;
		pk = pokeDamaManager.GetOpPokeDama ();
		opponentHealthBar.value = ((float) pk.health) / pk.maxHealth;

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
		Debug.Log ("Player Kick Animation Start");
		//Retract Animation
		float angleStep = 5f;
		for (int i = 0; i < 30; i++) {
			angleStep = Mathf.Lerp (angleStep, 0f, 0.1f);
			myPokeDama.transform.Rotate(Vector3.forward, angleStep);

			yield return new WaitForEndOfFrame ();
		}
		//Kick Animation
		float kickspeed = 10f;
		for (int i = 0; i < 10; i++) {
			myPokeDama.transform.position += kickspeed * (opPokeDama.transform.position - myPokeDama.transform.position) * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		StartCoroutine (sound.PlayOnHit ());
		StartCoroutine (Rotate (opPokeDama, 2, 1500f));
		//Return Animation
		for (int i = 0; i < 30; i++) {
			myPokeDama.transform.position = Vector3.Lerp (myPokeDama.transform.position, BattleGameManager.playerPos, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		myPokeDama.transform.position = BattleGameManager.playerPos;
		myPokeDama.transform.rotation = Quaternion.identity;
		Debug.Log ("Player Kick Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentKick() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Kick Animation Start");
		//Retract Animation
		float angleStep = 5f;
		for (int i = 0; i < 30; i++) {
			angleStep = Mathf.Lerp (angleStep, 0f, 0.1f);
			opPokeDama.transform.Rotate(Vector3.forward, angleStep);

			yield return new WaitForEndOfFrame ();
		}
		//Kick Animation
		float kickspeed = 10f;
		for (int i = 0; i < 10; i++) {
			opPokeDama.transform.position += kickspeed * (myPokeDama.transform.position - opPokeDama.transform.position) * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		StartCoroutine (sound.PlayOnHit ());
		StartCoroutine (RotateLeft (myPokeDama, 2, 1500f));
		//Return Animation
		for (int i = 0; i < 30; i++) {
			opPokeDama.transform.position = Vector3.Lerp (opPokeDama.transform.position, BattleGameManager.opponentPos, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		opPokeDama.transform.position = BattleGameManager.opponentPos;
		opPokeDama.transform.rotation = Quaternion.identity;
		Debug.Log ("Opponent Kick Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayer110V() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player 110V Animation Start");
		//Plug Animation
		GameObject plug = (GameObject) Instantiate(g_110V, BattleGameManager.playerPos, Quaternion.identity);
		for (int i = 0; i < 30; i++) {
			plug.transform.position = Vector3.Lerp (plug.transform.position, BattleGameManager.centerPos, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//Lightning Animation
		GameObject lightning = (GameObject) Instantiate(g_lightning, BattleGameManager.centerPos, Quaternion.identity);
		lightning.transform.Rotate (new Vector3 (0, 0, -60));
		for (int i = 0; i < 60; i++) {
			yield return new WaitForEndOfFrame ();
		}
		//Destroy Effects
		Destroy (plug);
		Destroy (lightning);
		Debug.Log ("Player 110V Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponent110V() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent 110V Animation Start");
		//Plug Animation
		GameObject plug = (GameObject) Instantiate(g_110V, BattleGameManager.opponentPos, Quaternion.identity);
		for (int i = 0; i < 30; i++) {
			plug.transform.position = Vector3.Lerp (plug.transform.position, BattleGameManager.centerPos, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//Lightning Animation
		GameObject lightning = (GameObject) Instantiate(g_lightning, BattleGameManager.centerPos, Quaternion.identity);
		lightning.transform.Rotate (new Vector3 (0, 0, -60));
		for (int i = 0; i < 60; i++) {
			yield return new WaitForEndOfFrame ();
		}
		//Destroy Effects
		Destroy (plug);
		Destroy (lightning);
		Debug.Log ("Opponent 110V Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerThrow() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Throw Animation Start");
		//Soju Spawn Animation
		GameObject soju = (GameObject) Instantiate(g_soju, BattleGameManager.playerPos, Quaternion.identity);
		Vector3 moveTo = new Vector3 (0, 1, 0);
		moveTo += BattleGameManager.playerPos;
		for (int i = 0; i < 30; i++) {
			soju.transform.position = Vector3.Lerp (soju.transform.position, moveTo, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//Soju Throw Animation
		StartCoroutine(sound.PlayOnThrow());
		StartCoroutine(Rotate(soju, 2, 1440f));
		float throwSpeed = 10f;
		for (int i = 0; i < 15; i++) {
			soju.transform.position += throwSpeed * (opPokeDama.transform.position - myPokeDama.transform.position) * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		StartCoroutine (sound.PlayOnHit ());
		yield return StartCoroutine (Rotate (opPokeDama, 2, 1440f));
		//Destroy Effects
		Destroy (soju);
		Debug.Log ("Player Throw Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentThrow() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Throw Animation Start");
		//Soju Spawn Animation
		GameObject soju = (GameObject) Instantiate(g_soju, BattleGameManager.opponentPos, Quaternion.identity);
		Vector3 moveTo = new Vector3 (0, 1, 0);
		moveTo += BattleGameManager.opponentPos;
		for (int i = 0; i < 30; i++) {
			soju.transform.position = Vector3.Lerp (soju.transform.position, moveTo, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//Soju Throw Animation
		StartCoroutine(sound.PlayOnThrow());
		StartCoroutine(RotateLeft(soju, 2, 1440f));
		float throwSpeed = 10f;
		for (int i = 0; i < 15; i++) {
			soju.transform.position += throwSpeed * (myPokeDama.transform.position - opPokeDama.transform.position) * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		StartCoroutine (sound.PlayOnHit ());
		yield return StartCoroutine (RotateLeft (myPokeDama, 2, 1440f));
		//Destroy Effects
		Destroy (soju);
		Debug.Log ("Opponent Throw Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerSpit() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Spit Animation Start");
		GameObject spit = (GameObject)Instantiate (g_spit);
		Vector3 vibration = new Vector3 (0.2f, 0);
		int dir = 1;
		for (int i = 0; i < 60; i++) {
			if (i % 5 == 0) {
				opPokeDama.transform.Translate (vibration * dir);
				dir *= -1;
			}
			if (i % 10 == 0) {
				StartCoroutine (sound.PlayOnThrow ());
			}
			yield return new WaitForEndOfFrame ();
		}
		//Destroy Effects
		Destroy (spit);
		opPokeDama.transform.position = BattleGameManager.opponentPos;
		Debug.Log ("Player Spit Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentSpit() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Spit Animation Start");
		GameObject spit = (GameObject)Instantiate (g_spit);
		Vector3 vibration = new Vector3 (0.2f, 0);
		int dir = 1;
		for (int i = 0; i < 60; i++) {
			if (i % 5 == 0) {
				myPokeDama.transform.Translate (vibration * dir);
				dir *= -1;
			}
			if (i % 10 == 0) {
				StartCoroutine (sound.PlayOnThrow ());
			}
			yield return new WaitForEndOfFrame ();
		}
		//Destroy Effects
		Destroy (spit);
		myPokeDama.transform.position = BattleGameManager.playerPos;
		Debug.Log ("Opponent Spit Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerSleep() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Sleep Animation Start");
		//ZZZ Spawn Animation
		StartCoroutine(sound.PlayOnSleep());
		GameObject zzz = (GameObject) Instantiate (g_zzz, BattleGameManager.playerPos, Quaternion.identity);
		Vector3 moveTo = new Vector3 (0, 1, 0);
		moveTo += BattleGameManager.playerPos;
		for (int i = 0; i < 30; i++) {
			zzz.transform.position = Vector3.Lerp (zzz.transform.position, moveTo, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//ZZZ Move Animation
		for (int i = 0; i < 20; i++) {
			if (i % 5 == 0) {
				zzz.transform.Rotate (Vector3.up, 180f);
			}
			yield return new WaitForEndOfFrame ();
		}
		//PokeDama Jump Animation
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 10; j++) {
				myPokeDama.transform.position = Vector3.Lerp (myPokeDama.transform.position, moveTo, 0.1f);
				yield return new WaitForEndOfFrame ();
			}
			for (int j = 0; j < 10; j++) {
				myPokeDama.transform.position = Vector3.Lerp (myPokeDama.transform.position, BattleGameManager.playerPos, 0.1f);
				yield return new WaitForEndOfFrame ();
			}
		}
		//Destroy Effects
		Destroy (zzz);
		Debug.Log ("Player Sleep Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentSleep() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Sleep Animation Start");
		//ZZZ Spawn Animation
		StartCoroutine(sound.PlayOnSleep());
		GameObject zzz = (GameObject) Instantiate (g_zzz, BattleGameManager.opponentPos, Quaternion.identity);
		Vector3 moveTo = new Vector3 (0, 1, 0);
		moveTo += BattleGameManager.opponentPos;
		for (int i = 0; i < 30; i++) {
			zzz.transform.position = Vector3.Lerp (zzz.transform.position, moveTo, 0.1f);
			yield return new WaitForEndOfFrame ();
		}
		//ZZZ Move Animation
		for (int i = 0; i < 20; i++) {
			if (i % 5 == 0) {
				zzz.transform.Rotate (Vector3.up, 180f);
			}
			yield return new WaitForEndOfFrame ();
		}
		//PokeDama Jump Animation
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 10; j++) {
				opPokeDama.transform.position = Vector3.Lerp (opPokeDama.transform.position, moveTo, 0.1f);
				yield return new WaitForEndOfFrame ();
			}
			for (int j = 0; j < 10; j++) {
				opPokeDama.transform.position = Vector3.Lerp (opPokeDama.transform.position, BattleGameManager.opponentPos, 0.1f);
				yield return new WaitForEndOfFrame ();
			}
		}
		//Destroy Effects
		Destroy (zzz);
		Debug.Log ("Player Sleep Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerRun() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Run Animation Start");
		float runSpeed = 15f;
		for (int i = 0; i < 20; i++) {
			myPokeDama.transform.Translate (Vector3.left * runSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Player Run Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerDamaged(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Damaged Animation Start");
		//Blinking Animation
		StartCoroutine(sound.PlayOnDamaged());
		for (int i = 0; i < 2; i++) {
			myPokeDama.SetActive (false);
			for (int j = 0; j < 5; j++) {
				yield return new WaitForEndOfFrame ();
			}
			myPokeDama.SetActive (true);
			for (int j = 0; j < 5; j++) {
				yield return new WaitForEndOfFrame ();
			}
		}
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		int healthText = 0;
		while (playerHealthBar.value >= value) {
			playerHealthBar.value -= 0.01f;
			healthText = (int) (playerHealthBar.value * pk.maxHealth);
			if (playerHealthBar.value >= 1 || playerHealthBar.value <= 0) { //Fail-Safe
				break;
			}
			playerHPText.text = healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		playerHPText.text = health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Player Damaged Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentDamaged(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Damaged Animation Start");
		//Blinking Animation
		StartCoroutine(sound.PlayOnDamaged());
		for (int i = 0; i < 2; i++) {
			opPokeDama.SetActive (false);
			for (int j = 0; j < 5; j++) {
				yield return new WaitForEndOfFrame ();
			}
			opPokeDama.SetActive (true);
			for (int j = 0; j < 5; j++) {
				yield return new WaitForEndOfFrame ();
			}
		}
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetOpPokeDama ();
		int healthText = 0;
		while (opponentHealthBar.value >= value) {
			opponentHealthBar.value -= 0.01f;
			healthText = (int) (opponentHealthBar.value * pk.maxHealth);
			if (opponentHealthBar.value >= 1 || opponentHealthBar.value <= 0) {
				break;
			}
			opponentHPText.text = healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		opponentHPText.text = health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Opponent Damaged Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerHeal(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Heal Animation Start");
		StartCoroutine (sound.PlayOnHeal ());
		Instantiate (HealParticle, BattleGameManager.playerPos, Quaternion.identity);
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		int healthText = 0;
		while (playerHealthBar.value <= value) {
			playerHealthBar.value += 0.01f;
			if (playerHealthBar.value >= 1 || playerHealthBar.value <= 0) { //Fail-Safe
				break;
			}
			healthText = (int) (playerHealthBar.value * pk.maxHealth);
			playerHPText.text = healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		playerHPText.text = health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Player Heal Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentHeal(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Heal Animation Start");
		StartCoroutine (sound.PlayOnHeal ());
		Instantiate (HealParticle, BattleGameManager.opponentPos, Quaternion.identity);
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetOpPokeDama ();
		int healthText = 0;
		while (opponentHealthBar.value <= value) {
			opponentHealthBar.value += 0.01f;
			if (opponentHealthBar.value >= 1 || opponentHealthBar.value <= 0) { //Fail-Safe
				break;
			}
			healthText = (int) (opponentHealthBar.value * pk.maxHealth);
			opponentHPText.text = healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		opponentHPText.text = health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Opponent Heal Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnPlayerFaint() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Faint Animation Start");
		StartCoroutine (sound.PlayOnFaint ());
		StartCoroutine (Rotate (myPokeDama, 5, 1440f));
		SpriteRenderer renderer = myPokeDama.GetComponent<SpriteRenderer> ();
		for (int i = 0; i < 60; i++) {
			renderer.color = new Color (1f, 1f, 1f, 1f - 0.02f * i);
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Player Faint Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnOpponentFaint() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Opponent Faint Animation Start");
		StartCoroutine (sound.PlayOnFaint ());
		StartCoroutine (Rotate (opPokeDama, 5, 1440f));
		SpriteRenderer renderer = opPokeDama.GetComponent<SpriteRenderer> ();
		for (int i = 0; i < 50; i++) {
			renderer.color = new Color (1f, 1f, 1f, 1f - 0.02f * i);
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log ("Opponent Faint Animation Done");
		mutex = false;
	}

	public IEnumerator PlayVictoryMusic() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Victory Music Start");
		audio.PlayVictoryMusic ();
		Debug.Log ("Victory Music Done");
		mutex = false;
	}

	public IEnumerator PlayDefeatMusic() {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Defeat Music Start");
		audio.PlayDefeatMusic ();
		Debug.Log ("Defeat Music Done");
		mutex = false;
	}

	IEnumerator Rotate(GameObject obj, int count, float speed) {
		float degree = (float)count * 360f;
		while (degree > 0) {
			degree -= speed * Time.deltaTime;
			if (degree < 0) {
				obj.transform.rotation = Quaternion.identity;
				break;
			}
			obj.transform.Rotate (Vector3.forward, speed * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator RotateLeft(GameObject obj, int count, float speed) {
		float degree = (float)count * 360f;
		while (degree > 0) {
			degree -= speed * Time.deltaTime;
			if (degree < 0) {
				obj.transform.rotation = Quaternion.identity;
				break;
			}
			obj.transform.Rotate (Vector3.forward, -speed * Time.deltaTime);
			yield return null;
		}
	}

	public IEnumerator Delay(float seconds) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		yield return new WaitForSeconds (seconds);
		mutex = false;
	}
}
