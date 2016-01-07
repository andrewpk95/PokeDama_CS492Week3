using System;

[Serializable]
public class Message {

	public enum MessageType {
		Connect,
		Request,
		Response
	}

	//public JSONObject json;
	public MessageType Type;
	public System.Object[] Arguments;

	public Message(MessageType type, System.Object[] arguments)
	{
		Type = type;
		Arguments = arguments;
	}
}

