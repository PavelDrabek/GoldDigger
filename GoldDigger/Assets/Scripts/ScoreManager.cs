using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private static ScoreManager instance;
	public static ScoreManager Instance { get { return instance; } }

	public Text scoreText;
	public Text scoreNeedText;
	public ScoreTween tween;

	private int score;
	private int scoreNeed;
	public int Score { get { return score; } }
	public int ScoreNeed { get { return scoreNeed; } }

	void Awake() {
		instance = this;
	}

	public void AddScore(int value) {
		score += value;
		scoreText.text = "Skóre: " + score.ToString();
		tween.ShowScore(value);

		scoreNeed -= value;
		if(scoreNeed < 0) {
			scoreNeed = 0;
		}
		scoreNeedText.text = "Zbývá: " + scoreNeed.ToString();
	}

	public void ShowMessage(string message) {
		tween.ShowMessage(message);
	}

	public void SetNeedScore(int need) {
		scoreNeed = need;
		scoreNeedText.text = "Zbývá: " + scoreNeed.ToString();
	}

	public void ResetScore() {
		score = 0;
		scoreText.text = "Skóre: " + score.ToString();
	}
}
