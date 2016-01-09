using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}

	public void OnRunButtonClick() {
		Debug.Log ("Moving to Map Scene...");
		SceneManager.LoadScene ("MapScene");
	}

	void OnEscapeKeyPress() {
		Debug.Log ("Moving to Map Scene...");
		SceneManager.LoadScene ("MapScene");
	}
}
