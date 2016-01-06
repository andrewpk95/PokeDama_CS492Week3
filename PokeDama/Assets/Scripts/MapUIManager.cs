using UnityEngine;
using System.Collections;

public class MapUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnReturnButtonClick() {
		Application.LoadLevel ("PokeDamaScene");
	}
}
