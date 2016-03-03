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

}