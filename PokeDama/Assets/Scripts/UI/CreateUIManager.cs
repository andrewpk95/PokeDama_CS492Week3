using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreateUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCreateButtonClick() {
		/*
		 * Do something
		 */
		SceneManager.LoadScene ("MenuScene");
	}
}
