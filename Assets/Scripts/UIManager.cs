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

	public GameObject pauseMenu;
	
	void Start () {
		healthCount = 0;
		goldCount = 0;
		currentWave = 1;
		maxWave = 10;
		UpdateHealthText ();
		UpdateGoldText ();
		UpdateWaveText ();
	}

	void Update () {
		UpdateHealthText ();
		UpdateGoldText ();
		UpdateWaveText ();
		ScanForKeyStroke();
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

}