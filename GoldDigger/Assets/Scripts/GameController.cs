using UnityEngine;
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

	private int actualLevel;
	public int ActualLevel { get { return actualLevel; } }

	private bool isMainButtonPressed;

	// TODO: 19 level bude mit dort
	// TODO: Postupem casu zvedat nutne skore pro splneni levelu 
	// TODO: Hlasky po sebrani sutru, dynamitu, prohre, vyhre ...
	// TODO: Obrazovka s ovladanim a vo co go (ze to je pro Vercu, ...)
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
		actualLevel = 0;
		StartNewLevel();
		scoreManager.ResetScore();
		gun.ResetDynamites();
	}

	public void StartNewLevel() {
		actualLevel++;
		levelText.text = "Level: " + actualLevel.ToString();
		mapGenerator.NewLevel();
		timeLeft = 60;
		scoreManager.SetNeedScore(200);
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

	public void QuitApplication() {
		Application.Quit();
	}
}
