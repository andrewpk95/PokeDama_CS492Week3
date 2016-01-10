using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool dontDestroyOnLoad;

	AudioSource audio;
	// Use this for initialization
	void Start () {
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (transform.gameObject);
		}
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
