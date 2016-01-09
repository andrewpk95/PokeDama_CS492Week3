using System;

[Serializable]
public class PokeDama {

	//Whose PokeDama is this?
	public string IMEI; //Unique ID according to devices
	public GoogleMapLocation location; //Location of the PokeDama

	//Identifiers
	public int id; //ID of the PokeDama. (ex. Inkachu's ID = 1)
	public String name = "PokeDamaDefault"; //Name that the user gives

	//Attributes
	public int level = 1;
	public int exp = 0;
	public int maxHealth = 10000;
	public int health = 10000;
	public int friendliness = 0;

	public PokeDama(string imei, int ID){
		IMEI = imei;
		id = ID;
		calculateStat ();
		health = maxHealth;
	}

	public PokeDama(string imei, int ID, string n){
		name = n;
		IMEI = imei;
		id = ID;
		calculateStat ();
		health = maxHealth;
	}

	public PokeDama(string imei, int ID, int LV) {
		level = LV;
		IMEI = imei;
		id = ID;
		calculateStat ();
		health = maxHealth;
	}

	public void damage(int damage) {
		health -= damage;
		if (health <= 0) { //Health reached below 0
			health = 0;
		} else if (health > maxHealth) { //Health went above max health
			health = maxHealth;
		}
	}
		
	public void calculateStat() {
		//Recalculate max health
		maxHealth = 10000 + friendliness * 2;

		//Recalculate level
		if (level <= 0) {
			level = 1;
		}
	}

	public void recalculateStat() {
		calculateStat ();
	}
}

