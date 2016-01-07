using UnityEngine;
using System.Collections;

public class BattleUIManager : MonoBehaviour {
	int PlayerStat = 100;
	int EnemyStat = 100;
	Random r = new Random();
	int playset = r.Next(0,4);


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

	public void PlayerAction(int num){
		switch (num) {
		case 0:
			Kick ();
			break;
		case 1:
			Throw ();
			break;
		case 2:
			ACT110V ();
			break;
		case 3:
			Sleep ();
			break;
		}
		EnemyAction (playset);
	}


	public void Kick(int Stat){
		Instantiate (inkachu, new Vector3 (0, 0, 0), Quaternion.identity);
		EnemyStat -= 5;
	}
	public void Throw(){
		EnemyStat -= 10;
	}
	public void ACT110V(){
		EnemyStat -= 20;
	}
	public void Sleep(){
		EnemyStat -= 0;
	}
	

	public void EnemyAction(int num){
		switch (num) {
		case 0:
			Kick ();
			break;
		case 1:
			Throw ();
			break;
		case 2:
			ACT110V ();
			break;
		case 3:
			Sleep ();
			break;
		}
	}

}
