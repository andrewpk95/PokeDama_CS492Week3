using System;

[Serializable]
public class PokeDama {

	//Whose PokeDama is this?
	public string IMEI;
	public GoogleMapLocation location;

	//Identifiers
	public int id;
	public String name;

	//Attributes
	public int maxHealth;
	public int health;
	public int friendliness;

	public PokeDama(string imei, int ID){
		IMEI = imei;
		id = ID;
		maxHealth = ID * 2;
		health = maxHealth;
	}

	public PokeDama(string imei, int ID, string n){
		IMEI = imei;
		id = ID;
		name = n;
		//maxHealth = 100 + friendliness * 0.02;
		health = maxHealth;
	}
}

