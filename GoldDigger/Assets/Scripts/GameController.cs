﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private static GameController instance;
	public static GameController Instance { get { return instance; } }

	public Debris debrisPrefab;

	public MapGenerator mapGenerator;
	public ScoreManager scoreManager;
	public CameraShake cameraShake;
	public Map map;
	public Gun gun;

	public GameObject nextLevelScreen;
	public GameObject gameOverScreen;
	public GameObject menuScreen;
	public GameObject bDayObject;
	public GameObject bDayScreen;

	public Text scoreText;
	public Text scoreMisText;
	public Text levelText;
	public Text timeText;
	public Text dynamiteText;

	private bool paused;
	private float speedUp = 1;
	public float TimeFlow { get { return paused ? 0 : Time.deltaTime * speedUp; } }
	public bool IsPause { get { return paused; } }

	private float timeLeft;

	public int actualLevel = 0;
	public int ActualLevel { get { return actualLevel; } }

	private bool isMainButtonPressed;

	// TODO: Hudba
	// TODO: Zebricek s historii skore
	// TODO: Dynamit do urciteho okruhu???

	void Start () {
		instance = this;
		StartNewGame();
		Pause();
	}

	void OnEnable() {
		instance = this;
	}
	
	void Update () {
		if(!menuScreen.activeSelf && !nextLevelScreen.activeSelf && !gameOverScreen.activeSelf) {
			timeLeft -= TimeFlow;
			timeText.text = ((int)(timeLeft + 0.99f)).ToString();
			
			if(timeLeft <= 0) {
				Pause();
				if(scoreManager.ScoreNeed > 0) {
					gameOverScreen.SetActive(true);
				} else {
					nextLevelScreen.SetActive(true);
				}
			}
		}
	}

	public void StartNewGame() {
		if (actualLevel > 10) {
			actualLevel = 9;
		} else {
			actualLevel = 0;
		}
		StartNewLevel();
		scoreManager.ResetScore();
		gun.ResetDynamites();
	}

	public void StartNewLevel() {
		actualLevel++;
		timeLeft = 60;
		if(actualLevel == 19) {
			ShowBDayLevel();
			Pause();
		} else {
			mapGenerator.NewLevel(actualLevel);
			scoreManager.SetNeedScoreByLevel(actualLevel);
		}
		levelText.text = "Level: " + actualLevel.ToString();
	}

	public void OnMainButtonPressed(bool down) {
		if(gun.IsOn) {
			SpeedUp(down);
		} else {
			if(isMainButtonPressed && !down) {
				gun.StartHook();
			}
			isMainButtonPressed = true;
		}

		if(!down) {
			isMainButtonPressed = false;
		}
	}

	public void SpeedUp(bool value) {
		speedUp = value ? 3 : 1;
	}

	public void Pause() {
		paused = true;
		gun.animator.enabled = false;
	}

	public void Continue() {
		paused = false;
		gun.animator.enabled = true;
	}

	public void BlowsAll(Gem.GemType type) {
		List<Gem> gems = map.Gems;
		for(int i = gems.Count - 1; i >= 0; i--) {
			if(gems[i].gemType == type) {
				Material mat = gems[i].GetComponent<MeshRenderer>().material;
				int count = (int)(20 * gems[i].Size);
				for (int d = 0; d < count; d++) {
					Debris debris = Instantiate(debrisPrefab, gems[i].transform.position, Random.rotation) as Debris;
					debris.Set(mat, Random.insideUnitSphere * gems[i].Size);
				}

				DestroyImmediate(gems[i].gameObject);
				gems.RemoveAt(i);
			}
		}

	}

	public void ShowBDayLevel() {
		bDayObject.SetActive(true);
		bDayScreen.SetActive(true);
	}

	public void QuitApplication() {
		Application.Quit();
	}
}
