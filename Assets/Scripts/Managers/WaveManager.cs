using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class SpawnSet
{
	public int spawnPointIndex;
	public float waitTime;
	public int numberOfUnits;
	public units unitType;
}

[System.Serializable]
public class Wave
{
	public float maxTime;
	public List<SpawnSet> spawnSets;
}


public class WaveManager : MonoBehaviour
{
	public delegate void done();
	public event done waveDone;

	public List<Wave> waves;
	public List<Transform> SpawnPoints;
	int index = 0;
	public float delay;
	bool wavesOver = false;
	
	void Start()
	{
		waveDone += NextWave;
		StartCoroutine(StartWaves());

	}

	IEnumerator StartWaves()
	{
		yield return new WaitForSeconds(delay);
		float waveStart;
		waveStart = Time.time;
		
		yield return null;
	}

	void NextWave()
	{
		index++;
		if (index >= waves.Count)
		{
			wavesOver = true;
		}
		else
		{
			StartCoroutine(LaunchWave());
		}
	}

	IEnumerator LaunchWave()
	{

		yield return null;
	}
	
}
