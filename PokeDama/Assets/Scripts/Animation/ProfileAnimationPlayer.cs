using UnityEngine;
using System.Collections;

public class ProfileAnimationPlayer : MonoBehaviour {

	public static bool mutex = false;

	PokeDamaManager pokeDamaManager;

	public GameObject g_playerHealthBar;
	public GameObject g_HPLabel;
	public GameObject g_friendlinessLabel;

	UIProgressBar playerHealthBar;
	UILabel HPText;
	UILabel friendText;

	public GameObject myPokeDama;
	int oldFriendliness;

	// Use this for initialization
	void Start () {
		pokeDamaManager = FindObjectOfType<PokeDamaManager> ();

		playerHealthBar = g_playerHealthBar.GetComponent<UIProgressBar> ();
		HPText = g_HPLabel.GetComponent<UILabel> ();
		friendText = g_friendlinessLabel.GetComponent<UILabel> ();

		myPokeDama = pokeDamaManager.myPicture;

		//Initialize UI
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		playerHealthBar.value = (float) pk.health / pk.maxHealth;
		oldFriendliness = pk.friendliness;
		HPText.text = "HP: " +  pk.health.ToString () + " / " + pk.maxHealth.ToString ();
		friendText.text = "Friendliness: " + oldFriendliness.ToString ();
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
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		while (oldFriendliness < friendliness) {
			oldFriendliness++;
			friendText.text = "Friendliness: " + oldFriendliness.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		friendText.text = "Friendliness: " + friendliness.ToString ();
		HPText.text = "HP: " +  pk.health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Player Friendliness Animation Done");
		mutex = false;
	}

	public IEnumerator PlayOnHeal(float value, int health) {
		while (mutex) {
			yield return new WaitForEndOfFrame ();
		}
		mutex = true;
		Debug.Log ("Heal Animation Start");
		//Animate Healthbar change
		PokeDama pk = pokeDamaManager.GetMyPokeDama ();
		int healthText = 0;
		while (playerHealthBar.value <= value) {
			playerHealthBar.value += 0.01f;
			if (playerHealthBar.value >= 1 || playerHealthBar.value <= 0) { //Fail-Safe
				break;
			}
			healthText = (int) (playerHealthBar.value * pk.maxHealth);
			HPText.text = "HP: " +  healthText.ToString () + " / " + pk.maxHealth.ToString ();
			yield return new WaitForEndOfFrame ();
		}
		HPText.text = "HP: " + health.ToString () + " / " + pk.maxHealth.ToString ();
		Debug.Log ("Heal Animation Done");
		mutex = false;
	}
}
