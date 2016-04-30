using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class SpawnSet
{
	public int spawnPointIndex;
	public float waitTime;
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
	public Spawner spawner;
	int index = -1;
	public float delay;
	bool wavesOver = false;
	public GameObject win;
	void Start()
	{
		waveDone += NextWave;
		StartCoroutine(StartWaves());

	}

	IEnumerator StartWaves()
	{
		yield return new WaitForSeconds(delay);
		NextWave();
		
		yield return null;
	}

	void NextWave()
	{
		index++;
		if (index >= waves.Count)
		{
			wavesOver = true;
			win.SetActive(true);
		}
		else
		{
			StartCoroutine(LaunchWave());
		}
	}

	IEnumerator LaunchWave()
	{
		foreach (SpawnSet s in waves[index].spawnSets)
		{
			StartCoroutine(LaunchSet(s));
		}
		yield return new WaitForSeconds(waves[index].maxTime);
		if (index != waves.Count -1)
		{
			waveDone();
		}
		
	}

	IEnumerator LaunchSet(SpawnSet s)
	{
		yield return new WaitForSeconds(s.waitTime);
		switch (s.unitType)
		{
			case units.SIMPLE_GROUND_UNIT:
				spawner.SpawnSimpleGroundUnit(SpawnPoints[s.spawnPointIndex].position);
				break;
		}
		// Spawn given unit
	}
	
}
