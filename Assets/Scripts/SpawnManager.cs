using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] GameObject[] enemyPrefab;
	[SerializeField] GameObject[] powerupPrefab;
	[SerializeField] Transform Island;
	private int enemyIndex;

 	private float spawnRange = 9.0f;
	private int enemyCount;
	private int waveNumber = 1;

	// Start is called before the first frame update
	void Start()
    {
		int powerUpIndex = Random.Range(0,powerupPrefab.Length);
		spawnEnemyWave(waveNumber);
		Instantiate(powerupPrefab[powerUpIndex],GenerateSpawnPos(),powerupPrefab[powerUpIndex].transform.rotation);
	}

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
		
		if(enemyCount == 0)
		{
			PowerUpSpawn();
		}
    }
	
	void spawnEnemyWave(int enimiesToSpawn)
	{
		for(int i = 0; i < enimiesToSpawn; i++)
		{
			int enemyIndex = Random.Range(0,2); 
			Instantiate(enemyPrefab[enemyIndex],GenerateSpawnPos(),enemyPrefab[enemyIndex].transform.rotation);

			if(i == 5 || i == 10)
            {
				SpawnBoss();
            }
		}
	}
	private Vector3 GenerateSpawnPos()
	{
		float spawnPosX = Random.Range(-spawnRange,spawnRange);
		float spawnPosZ = Random.Range(-spawnRange,spawnRange);
		
		Vector3 randomPos = new Vector3(spawnPosX,0,spawnPosZ);
		
		return randomPos;
	}

	void PowerUpSpawn()
    {
		int powerUpIndex = Random.Range(0, powerupPrefab.Length);
		waveNumber++;
		spawnEnemyWave(waveNumber);
		Instantiate(powerupPrefab[powerUpIndex], GenerateSpawnPos(), powerupPrefab[powerUpIndex].transform.rotation);
	}

	void SpawnBoss()
    {
		Instantiate(enemyPrefab[2], GenerateSpawnPos(), enemyPrefab[2].transform.rotation);
		Island.localScale += new Vector3(1, 1, 1);
	}
}
