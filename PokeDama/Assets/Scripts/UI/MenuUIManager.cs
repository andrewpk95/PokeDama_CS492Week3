﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnProfileButtonClick() {
		Debug.Log ("Moving to Profile Scene...");
		SceneManager.LoadScene ("PokeDamaScene");
	}
}