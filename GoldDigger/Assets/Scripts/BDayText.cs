using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BDayText : MonoBehaviour {

	public Transform myTransform;
	public Text text;
	public float timeFreeze = 0.3f;

	void Start() {
		StartCoroutine(Blinking());
	}

	IEnumerator Blinking() {
		while(true) {
			myTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10, 10));
			text.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
			yield return new WaitForSeconds(timeFreeze);
		}
	}
}
