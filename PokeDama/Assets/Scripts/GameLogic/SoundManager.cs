using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public bool dontDestroyOnLoad;

	//Sound Prefabs
	public GameObject DamageSound;
	public GameObject FaintSound;
	public GameObject HealSound;
	public GameObject HitSound;
	public GameObject PetSound;
	public GameObject SleepSound;
	public GameObject ThrowSound;
	public GameObject TouchDialogueSound;

	// Use this for initialization
	void Start () {
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator PlayOnDamaged() {
		GameObject sound = (GameObject) Instantiate (DamageSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnFaint() {
		GameObject sound = (GameObject) Instantiate (FaintSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnHeal() {
		GameObject sound = (GameObject) Instantiate (HealSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnHit() {
		GameObject sound = (GameObject) Instantiate (HitSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnPet() {
		GameObject sound = (GameObject) Instantiate (PetSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnSleep() {
		GameObject sound = (GameObject) Instantiate (SleepSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnThrow() {
		GameObject sound = (GameObject) Instantiate (ThrowSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}

	public IEnumerator PlayOnTouch() {
		GameObject sound = (GameObject) Instantiate (TouchDialogueSound, Vector3.zero, Quaternion.identity);
		while (sound.GetComponent<AudioSource> ().isPlaying) {
			yield return null;
		}
		Destroy (sound);
	}
}
