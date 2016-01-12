using UnityEngine;
using System.Collections;

public class ProfileAnimationPlayer : MonoBehaviour {

	public static bool mutex = false;

	PokeDamaManager pokeDamaManager;
	SoundManager sound;

	public GameObject g_playerHealthBar;
	public GameObject g_HPLabel;
	public GameObject g_friendlinessLabel;
	public GameObject g_strengthLabel;

	public GameObject healingParticle;
	public GameObject heartParticle;

	UIProgressBar playerHealthBar;
	UILabel HPText;
	UILabel friendText;
	UILabel strengthText;

	public GameObject myPokeDama;
	Animator anim;
	int oldFriendliness;

	// Use this for initialization
	void Start () {
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();
		sound = FindObjectOfType<SoundManager> ();

		playerHealthBar = g_playerHealthBar.GetComponent<UIProgressBar> ();
		HPText = g_HPLabel.GetComponent<UILabel> ();
		friendText = g_friendlinessLabel.GetComponent<UILabel> ();
		strengthText = g_strengthLabel.GetComponent<UILabel> ();

		myPokeDama = pokeDamaManager.myPicture;
		anim = myPokeDama.GetComponent<Animator> ();

		//Initialize UI
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		playerHealthBar.value = (float) pk.health / pk.maxHealth;
		oldFriendliness = pk.friendliness;
		HPText.text = pk.health.ToString () + " / " + pk.maxHealth.ToString ();
		friendText.text = "Friendliness: " + oldFriendliness.ToString ();
		strengthText.text = "Strength: " + pk.strength.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator PlayOnFriendliness(int friendliness) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Player Friendliness Animation Start");
		//Animate PokeDama
		anim.SetTrigger("Pet");
		//Animate Particle
		StartCoroutine(sound.PlayOnPet());
		Instantiate (heartParticle, ProfileGameManager.spawnPos, Quaternion.identity);
		//Animate Friendliness Increase
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		while (oldFriendliness < friendliness) {
			oldFriendliness++;
			friendText.text = "Friendliness: " + oldFriendliness.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		friendText.text = "Friendliness: " + friendliness.ToString ();
		strengthText.text = "Strength: " + pk.strength.ToString ();
		HPText.text = pk.health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Player Friendliness Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnHeal(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Heal Animation Start");
		//Animate PokeDama
		anim.SetTrigger("Feed");
		//Animate Particle
		StartCoroutine(sound.PlayOnHeal());
		Instantiate (healingParticle, ProfileGameManager.spawnPos, Quaternion.identity);
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		int healthText = 0;
		while (playerHealthBar.value <= value) {
			playerHealthBar.value += 0.01f;
			if (playerHealthBar.value >= 1 || playerHealthBar.value <= 0) { //Fail-Safe
				break;
			}
			healthText = (int) (playerHealthBar.value * pk.maxHealth);
			HPText.text = healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		HPText.text = health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Heal Animation Done");
		mutex = false;
	}
}
