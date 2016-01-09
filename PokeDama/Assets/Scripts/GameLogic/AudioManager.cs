using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public bool dontDestroyOnLoad;
	// Use this for initialization
	void Start () {
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
