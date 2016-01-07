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

	public PokeDama(string imei, int ID) {
		IMEI = imei;
		id = ID;
		maxHealth = ID * 2;
		health = maxHealth;
	}

}

