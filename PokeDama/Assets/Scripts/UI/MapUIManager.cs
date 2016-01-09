using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}

	public void OnReturnButtonClick() {
		SceneManager.LoadScene ("PokeDamaScene");
	}

	void OnEscapeKeyPress() {
		Debug.Log ("Moving to Profile Scene...");
		SceneManager.LoadScene ("PokeDamaScene");
	}
}
