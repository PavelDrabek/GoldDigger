using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class MessageGenerator : MonoBehaviour {

	public TextAsset file;
	
	public List<string>[] messages;
	public enum MessageType {Win, Loose, LooseClose, TakeRock, ExitGame, Count };

	void Start () {
		ReadXML();
	}
	
	void Update () {
	
	}

	void ReadXML() {
		int typesCount = (int)MessageType.Count;
		messages = new List<string>[typesCount];
		for(int i = 0; i < typesCount; i++) {
			messages[i] = new List<string>();
		}

		XmlTextReader reader = new XmlTextReader (file.text, XmlNodeType.Element, null);
		while(reader.Read()) {
			string typeName = reader.Name;
			if(string.IsNullOrEmpty(typeName)) {
				continue;
			}

			string message = reader.ReadString(); 

			int mTypeIndex = (int)((MessageType)System.Enum.Parse(typeof(MessageType), typeName));
			if(mTypeIndex >= 0 && mTypeIndex < typesCount) {
				messages[mTypeIndex].Add(message);
//				Debug.Log("Adding message: " + message + " for type " + typeName);
			} else {
				Debug.LogError("Unknown type " + typeName);
			}
		}
	}

	public string GetMessage(MessageType type) {
		int typeIndex = (int)type;
		return messages[typeIndex][Random.Range(0, messages[typeIndex].Count - 1)];
	}
}
