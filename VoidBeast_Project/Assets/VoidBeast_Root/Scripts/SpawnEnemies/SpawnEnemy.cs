using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [Header("Referencias")]
    [SerializeField] TMP_Text enemyText;

    [Header("Opciones de oleadas")]
    [SerializeField] List<WaveType> allWaveTypes;

    [Header("Spawners")]
    [SerializeField] List<Transform> allSpawners;

    [SerializeField]private int enemyRemain;
    private bool spawning;
    [SerializeField] private bool finishedNight = false;
    private Coroutine spawnCoroutine;
    void Update()
    {

        if (DayNightSystem.Instance.isNight && !spawning)
        {
            StartNight();
        }
        if (DayNightSystem.Instance.isNight)
        {
            int enemyCount = EnemyManager.instance.enemies.Count;
            enemyText.text = enemyCount.ToString();

            if (finishedNight && enemyCount == 0)
            {
                DayNightSystem.Instance.ToDay();
                finishedNight = false;
                enemyRemain = 0;
            }
        }

    }
    private void StartNight()
    {
        spawning = true;
        finishedNight = false;

        int night = DayNightSystem.Instance.nightNumber;
        enemyRemain = GetEnemyCountForNight(night);

        WaveType wave = GetRandomWaveForNight(night);

        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        spawnCoroutine = StartCoroutine(SpawnWave(wave));
    }

    private IEnumerator SpawnWave(WaveType wave)
    {
        finishedNight = true;
        int night = DayNightSystem.Instance.nightNumber;
        int enemiesToSpawn = GetEnemyCountForNight(night);

        while (enemiesToSpawn > 0)
        {
            int weight = SpawnEnemyFromWave(wave);

            enemiesToSpawn -= weight;

            yield return new WaitForSeconds(0.02f);
        }
    }

    private int SpawnEnemyFromWave(WaveType wave)
    {
        Transform spawner = GetSpawnerForWave(wave);

        float roll = Random.value;
        float cumulative = 0f;

        foreach (var enemy in wave.enemies)
        {
            cumulative += enemy.chance;
            if (roll <= cumulative)
            {
                Instantiate(enemy.enemyPrefab, spawner.position, Quaternion.identity);

                return enemy.weight;
            }
        }

        return 1; 
    }
    private Transform GetSpawnerForWave(WaveType wave)
    {
        int maxSpawners = Mathf.Min(wave.maxSpawnersUsed, allSpawners.Count);

        List<Transform> tempList = new List<Transform>(allSpawners);
        Shuffle(tempList);

        return tempList[Random.Range(0, maxSpawners)];
    }
    private WaveType GetRandomWaveForNight(int night)
    {
        List<WaveType> validWaves = new List<WaveType>();

        foreach (var wave in allWaveTypes)
        {
            if (night >= wave.minNight && night <= wave.maxNight)
                validWaves.Add(wave);
        }

        return validWaves[Random.Range(0, validWaves.Count)];
    }
    public int GetEnemyCountForNight(int night)
    {
        int baseEnemies = 6;
        float growth = 1.2f;

        return Mathf.RoundToInt(baseEnemies + night * growth);
    }
    private void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
}

