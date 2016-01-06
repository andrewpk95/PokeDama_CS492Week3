using UnityEngine;
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
		Application.LoadLevel ("PokeDamaScene");
	}

	void OnEscapeKeyPress() {
		Debug.Log ("Moving to Profile Scene...");
		Application.LoadLevel ("PokeDamaScene");
	}
}
