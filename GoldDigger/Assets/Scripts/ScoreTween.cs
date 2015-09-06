using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTween : MonoBehaviour {

	public Text text;

	public float defaultTime;
	public float timeToDie;

	public Color startColor;
	public Color endColor;

	public float startSize;
	public float endSize;

	void Update () {
		timeToDie -= Time.deltaTime;
		if(timeToDie <= 0) {
			text.enabled = false;
		}

		text.color = Color.Lerp(startColor, endColor, 1 - timeToDie / defaultTime);
		text.fontSize = (int)(startSize + (endSize - startSize) * (1 - timeToDie / defaultTime));
	}

	public void ShowScore(int score) {
		text.text = score.ToString();
		timeToDie = defaultTime;
		text.enabled = true;
	}

	public void ShowMessage(string msg) {
		text.text = msg;
		timeToDie = defaultTime;
		text.enabled = true;
	}
}
