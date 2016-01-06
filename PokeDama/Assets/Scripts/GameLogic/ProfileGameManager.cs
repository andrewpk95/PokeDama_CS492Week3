using UnityEngine;
using System.Collections;

public class ProfileGameManager : MonoBehaviour {

	int friendliness;
	// Use this for initialization
	void Start () {
		friendliness = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pet() {
		friendliness++;
		Debug.Log (friendliness);
	}
}
