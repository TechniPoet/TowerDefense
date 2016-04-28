using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	
	public int healthCount;
	public int goldCount;
	public int currentWave;
	public int maxWave;

	public Text healthText;
	public Text goldText;
	public Text waveText;

	public WaveManager waveManager;

	public GameObject pauseMenu;
	
	void Start () {
		healthCount = 20;
		goldCount = 100;
		currentWave = 0;
		maxWave = waveManager.waves.Count;
		UpdateHealthText ();
		UpdateGoldText ();
		UpdateWaveText();
		waveManager.waveDone += UpdateWaveText;
		Unit.UnitDied += AddGold;
		deadZone.LoseHP += RemoveLife;
	}

	void Update () {
		
		ScanForKeyStroke();
	}

	void AddGold(int amt)
	{
		goldCount += amt;
		UpdateGoldText();
	}

	void RemoveLife(int amt)
	{
		healthCount -= amt;
		UpdateHealthText();
	}

	void UpdateHealthText ()
	{
		healthText.text = healthCount.ToString ();
	}

	void UpdateGoldText ()
	{
		goldText.text = goldCount.ToString ();
	}

	void UpdateWaveText ()
	{
		currentWave++;
		waveText.text = "WAVE   " + currentWave.ToString () + " / " + maxWave.ToString ();
	}

	void ScanForKeyStroke()
	{
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown ("p")) {
			TogglePauseMenu ();
		}
	}

	public void TogglePauseMenu ()
	{
		if (pauseMenu.activeSelf) {	// resume game
			pauseMenu.SetActive (false);
			Time.timeScale = 1.0f;
		}
		else {	// pause game
			pauseMenu.SetActive (true);
			Time.timeScale = 0.0f;
		}
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}

}