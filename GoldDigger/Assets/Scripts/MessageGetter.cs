using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageGetter : MonoBehaviour {

	public MessageGenerator generator;
	public Text message;

	public MessageGenerator.MessageType type;

	void OnEnable() {
		message.text = generator.GetMessage(type);
	}
}
