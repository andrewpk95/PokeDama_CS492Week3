using UnityEngine;
using System.Collections;

public class BattleUIManager : MonoBehaviour {

	public GameObject inkachu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			OnEscapeKeyPress();
		}
	}
	void OnEscapeKeyPress() {
		Debug.Log ("Moving to Map Scene...");
		Application.LoadLevel ("MapScene");
	}






	public void OnRunButtonClick() {
		Debug.Log ("Moving to Map Scene...");
		Application.LoadLevel ("MapScene");
	}

	public void Kick(){
		Instantiate (inkachu, new Vector3 (0, 0, 0), Quaternion.identity);

	}
	public void Throw(){

	}
	public void ACT110V(){

	}
	public void Sleep(){

	}


}
